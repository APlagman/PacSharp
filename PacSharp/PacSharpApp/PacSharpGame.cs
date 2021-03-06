﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using PacSharpApp.AI;
using PacSharpApp.Graphics;
using PacSharpApp.Graphics.Animation;
using PacSharpApp.Objects;
using PacSharpApp.Properties;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// 
/// Sources used to replicate game behavior:
/// https://www.lomont.org/Software/Games/PacMan/PacmanEmulation.pdf
/// http://gameinternals.com/post/2072558330/understanding-pac-man-ghost-behavior
/// https://www.gamasutra.com/view/feature/132330/the_pacman_dossier.php
/// </summary>
namespace PacSharpApp
{
    /// <summary>
    /// Handles game logic
    /// </summary>
    sealed class PacSharpGame : Game
    {
        #region Constants
        private const int StartingLives = 2;
        private const string ReadyText = "READY!";
        private const string OneUpText = "1UP";
        private const string HighscoreText = "HIGHSCORE";
        private const string TwoUpText = "2UP";
        private const string CreditText = "CREDIT";
        private const int BaseGhostScore = 200;
        private const int LevelNumberToStopGhostsTurningBlue = 20;
        private const double MinimumFruitAppearanceDurationInSeconds = 9;
        private const int OneUpThreshold = 10000;
        private const int PowerPelletMovementDelayFrames = 3;
        private const int PelletMovementDelayFrames = 1;
        private const int FirstFruitThreshold = 170;
        private const int SecondFruitThreshold = 70;
        private const int LastOrangeLevel = 4;
        private const int LastAppleLevel = 6;
        private const int LastMelonLevel = 8;
        private const int LastGalaxianLevel = 10;
        private const int LastBellLevel = 12;
        private const int LastStrawberryLevel = 2;
        private const int MaxFruitsToDraw = 6;
        private const int UpperBonusFruitRow = 34;
        private const int RightmostBonusFruitLeftColumn = 26;
        private const int HighscoresFirstInitialColumn = 23;
        private static readonly TimeSpan EatGhostPauseDuration = TimeSpan.FromSeconds(1);
        private static readonly TimeSpan VictoryPauseDuration = TimeSpan.FromMilliseconds(300);
        private static readonly TimeSpan MainMenuAnimationsDisabledDuration = TimeSpan.FromMilliseconds(500);
        private static readonly TimeSpan LevelStartDelay = TimeSpan.FromSeconds(2);
        private static readonly TimeSpan RespawnPlayerDelay = TimeSpan.FromMilliseconds(300);
        private static readonly TimeSpan HighscoreScreenDelay = TimeSpan.FromSeconds(1);
        private static readonly TimeSpan EatFruitDisplayScoreDuration = TimeSpan.FromSeconds(2);
        #endregion

        #region Fields
        private Highscores currentHighscores;
        private int creditsRemaining = 0;
        private int livesRemaining;
        private int LivesRemaining { get => livesRemaining; set { livesRemaining = value; UpdateLives(); } }
        private int globalPelletCounter = 0;
        private bool globalPelletCounterEnabled = false;

        private int ghostsEaten = 0;
        private int levelNumber = 0;
        private int displayedHighScore = 0;
        private List<(TimeSpan delay, Action action)> actionQueue = new List<(TimeSpan, Action)>();

        private Maze level;
        private ICollection<PelletObject> pellets = new List<PelletObject>();
        private ICollection<PowerPelletObject> powerPellets = new List<PowerPelletObject>();
        private ICollection<GameObject> lives = new List<GameObject>();
        private ICollection<GhostObject> ghosts = new List<GhostObject>();
        private PacmanObject player;
        private IReadOnlyCollection<RectangleF> walls;
        private FruitObject fruit;

        private TimeSpan ghostModeTimer = TimeSpan.MaxValue;
        private int ghostphase;
        private TimeSpan fruitDespawnTimer = TimeSpan.MaxValue;
        private GameObject ghostScoreObj;
        private TimeSpan pausePlayerOnEatingGhostTimer = TimeSpan.MaxValue;
        private ICollection<GameObject> ghostObjectsEaten = new List<GameObject>();

        private bool victoryAlreadyReached = false;
        private TimeSpan pelletTimer = TimeSpan.MaxValue;
        private bool playerHasLostLifeThisLevel = false;

        private bool enteringHighscoreInitials = false;
        private int highscoreInitialToChangePosition = 0;
        private int highscoreToChangeIndex = 0;
        #endregion

        internal PacSharpGame(IGameUI owner, Control gameArea)
            : base(owner, gameArea)
        {
            LoadHighscores();
        }

        #region Properties
        private int DisplayedHighScore
        {
            get => displayedHighScore;
            set { displayedHighScore = value; UpdateHighScore(); }
        }
        private protected override bool PreventUpdate => Paused;
        private bool VictoryConditionReached => Score > 0 && pellets.Count == 0 && powerPellets.Count == 0;
        private int GhostScore => BaseGhostScore << ghostsEaten;
        private protected override bool UseFixedTimeStepForUpdates => true;
        private protected override bool UseFixedTimeStepForAnimations => true;
        private bool GhostsShouldTurnBlue => levelNumber < LevelNumberToStopGhostsTurningBlue;
        private GhostObject GhostToRelease => ghosts?.Where(ghost => ghost.IsHome).OrderBy(ghost => (ghost.Behavior as GhostAIBehavior).ReleasePriority).FirstOrDefault();
        private TimeSpan PelletTimerInterval => (levelNumber >= 4 ? TimeSpan.FromSeconds(4) : TimeSpan.FromSeconds(3));
        private int GhostPhase
        {
            get => ghostphase; set { ghostphase = value;  PlaySirens(); }
        }
        #endregion

        #region Input
        private protected override void HandleInput()
        {
            if (InputHandler.HeldKeys.Contains(Keys.R) && InputHandler.HeldKeys.Contains(Keys.ControlKey))
            {
                ScheduleReset();
                return;
            }
            switch (State)
            {
                case GameState.Menu:
                    HandleMenuInput();
                    break;
                case GameState.Playing:
                    HandlePlayingInput();
                    break;
                case GameState.Highscores:
                    HandleHighscoreInput();
                    break;
                default:
                    break;
            }
        }

        private void HandleHighscoreInput()
        {
            if (enteringHighscoreInitials)
            {
                foreach (var key in InputHandler.PressedKeys)
                {
                    if ((int)key >= (int)Keys.A && (int)key <= (int)Keys.Z)
                    {
                        char[] initials = currentHighscores.ToDisplay[highscoreToChangeIndex].initials.ToCharArray();
                        initials[highscoreInitialToChangePosition] = (char)key;
                        string newInitials = new string(initials);
                        ++highscoreInitialToChangePosition;
                        highscoreInitialToChangePosition %= 3;
                        currentHighscores.Update(highscoreToChangeIndex, newInitials);
                        for (int t = 0; t < 3; ++t)
                            Tiles.ClearTile(4 + highscoreToChangeIndex * 2, HighscoresFirstInitialColumn + t);
                        GraphicsHandler.CommitTiles(Tiles);
                        Tiles.DrawText(4 + highscoreToChangeIndex * 2, HighscoresFirstInitialColumn, newInitials);
                        break;
                    }
                }
                if (InputHandler.PressedKeys.Contains(Keys.Enter))
                {
                    enteringHighscoreInitials = false;
                    Tiles.DrawText(4 + highscoreToChangeIndex * 2, HighscoresFirstInitialColumn, currentHighscores.ToDisplay[highscoreToChangeIndex].initials, HighscoreEntryPalette(highscoreToChangeIndex));
                    for (int c = 0; c < GraphicsConstants.GridWidth; ++c)
                        Tiles.ClearTile(30, c);
                    SaveHighScores();
                }
            }
        }

        private void HandlePlayingInput()
        {
            if (InputHandler.PressedKeys.Contains(Keys.P))
                Paused = !Paused;
            if (!Paused)
                player?.HandleInput(InputHandler);
        }

        private void HandleMenuInput()
        {
            if (InputHandler.HeldKeys.Contains(Keys.Enter))
            {
                StartGame();
                return;
            }
            if (InputHandler.HeldKeys.Contains(Keys.H) && InputHandler.HeldKeys.Contains(Keys.ControlKey))
            {
                State = GameState.Highscores;
                ShowHighscoreScreen();
            }
        }
        #endregion

        #region Update Logic
        private protected override void UpdateImpl(TimeSpan elapsedTime)
        {
            UpdateActionQueue(elapsedTime);
            UpdateObjects(elapsedTime);
            switch (State)
            {
                case GameState.Menu:
                    break;
                case GameState.Highscores:
                    break;
                case GameState.Cutscene:
                    break;
                case GameState.Playing:
                    if (Paused)
                        break;
                    HandleCollisions();
                    if (VictoryConditionReached && !victoryAlreadyReached)
                    {
                        GraphicsHandler.PreventAnimatedSpriteUpdates = true;
                        player.PreventMovement = true;
                        foreach (var ghost in ghosts)
                            ghost.PreventMovement = true;
                        victoryAlreadyReached = true;
                        ++levelNumber;
                        StopSirens();
                        actionQueue.Add((VictoryPauseDuration, () => StartNextLevel(TimeSpan.MaxValue)));
                    }
                    if (ghostObjectsEaten.Count == 0)
                    {
                        UpdateGhostModeTimer(elapsedTime);
                        UpdatePelletTimer(elapsedTime);
                        UpdateFruitTimer(elapsedTime);
                    }
                    UpdateSounds();
                    UpdateGhostEatenPauseTimer(elapsedTime);
                    CheckCruiseElroyState();
                    break;
                default:
                    break;
            }
        }

        private void UpdateSounds()
        {
            int frightenedCount = ghosts.Count(ghost => ghost.IsFrightened);
            if (frightenedCount == 0)
            {
                SoundHandler.Stop("Content/Sound/large pellet loop.wav");
                PlaySirens();
            }

            if (ghosts.Count(ghost => ghost.IsRespawning) == 0)
            {
                SoundHandler.Stop("Content/Sound/ghost eat 2.wav");
                if (frightenedCount > 0)
                    SoundHandler.Play("Content/Sound/large pellet loop.wav", true);
            }
        }

        private void CheckCruiseElroyState()
        {
            if (pellets.Count > CruiseElroyThreshold)
                return;
            foreach (var ghost in ghosts)
            {
                if (ghost.Behavior is BlinkyAIBehavior && (ghosts.Count(g => g.IsHome) == 0 || playerHasLostLifeThisLevel == false))
                    ghost.CruiseElroyMode = (pellets.Count <= CruiseElroyThreshold2) ? 2 : 1;
            }
        }

        private void UpdateFruitTimer(TimeSpan elapsedTime)
        {
            if (fruitDespawnTimer == TimeSpan.MaxValue)
                return;
            if (fruitDespawnTimer <= elapsedTime)
            {
                Despawn(fruit);
                fruit = null;
            }
            else
                fruitDespawnTimer -= elapsedTime;
        }

        private void UpdateGhostEatenPauseTimer(TimeSpan elapsedTime)
        {
            if (pausePlayerOnEatingGhostTimer == TimeSpan.MaxValue)
                return;
            if (pausePlayerOnEatingGhostTimer <= elapsedTime)
            {
                AfterGhostEatenPause();
            }
            else
                pausePlayerOnEatingGhostTimer -= elapsedTime;
        }

        private void UpdatePelletTimer(TimeSpan elapsedTime)
        {
            if (pelletTimer == TimeSpan.MaxValue)
                return;
            if (pelletTimer <= elapsedTime)
            {
                if (GhostToRelease != null)
                    GhostToRelease.ExitingGhostHouse = true;
                pelletTimer = PelletTimerInterval;
            }
            else
                pelletTimer -= elapsedTime;
        }

        private void UpdateGhostModeTimer(TimeSpan elapsedTime)
        {
            if (ghostModeTimer == TimeSpan.MaxValue)
                return;
            if (GhostPhase < 7 && ghostModeTimer <= elapsedTime)
            {
                ++GhostPhase;
                ghostModeTimer = GhostModePhaseDuration();
                foreach (var ghost in ghosts)
                {
                    ghost.ShouldScatter = (GhostPhase % 2 == 0);
                }
            }
            else
                ghostModeTimer -= elapsedTime;
        }

        private void UpdateActionQueue(TimeSpan elapsedTime)
        {
            var finished = new List<(TimeSpan delay, Action action)>(
                actionQueue
                .FindAll(delayedAction => delayedAction.delay <= elapsedTime));
            foreach (var (delay, action) in finished)
                action();
            actionQueue = new List<(TimeSpan delay, Action action)>(
                actionQueue
                .FindAll(delayedAction => delayedAction.delay > elapsedTime)
                .Select(delayedAction => (delayedAction.delay - elapsedTime, delayedAction.action)));
        }

        private void UpdateObjects(TimeSpan elapsedTime)
        {
            foreach (var obj in GameObjects)
                obj.Value.Update(elapsedTime);
            if (State == GameState.Playing)
            {
                if (!Paused)
                    player?.Update(elapsedTime);
                foreach (var ghost in ghosts)
                    ghost.PelletCounterEnabled = false;
                if (globalPelletCounterEnabled == false && GhostToRelease != null)
                    GhostToRelease.PelletCounterEnabled = true;
                foreach (var ghost in ghosts)
                    ghost.Update(elapsedTime);
                foreach (var pellet in pellets)
                    pellet.Update(elapsedTime);
                foreach (var pellet in powerPellets)
                    pellet.Update(elapsedTime);
            }
        }
        #endregion

        #region Collision
        private void HandleCollisions()
        {
            if (player == null)
                return;
            CheckWarping();
            PushOutOfWalls(player);
            CheckIfEatingPellets(player);
            CheckIfTouchingGhosts(player);
            CheckIfEatingFruit(player);
        }

        private void CheckIfEatingFruit(PacmanObject eater)
        {
            if (fruit != null && eater.MouthBounds.IntersectsWith(fruit.Bounds))
            {
                HandleEatingFruit(eater);
            }
        }

        private void HandleEatingFruit(PacmanObject eater)
        {
            fruitDespawnTimer = TimeSpan.MaxValue;
            Score += fruit.Score;
            Despawn(fruit);
            Vector2 fruitPos = fruit.Position;
            int fruitLeftCol = (int)(fruitPos.X) / GraphicsConstants.TileWidth - 1;
            int fruitRow = (int)(fruitPos.Y - GraphicsConstants.TileWidth / 2) / GraphicsConstants.TileWidth;
            fruit.DrawPoints(Tiles, fruitRow, fruitLeftCol);
            actionQueue.Add((EatFruitDisplayScoreDuration, () => RemoveDisplayedFruitScore(fruitLeftCol, fruitRow)));
            fruit = null;
            SoundHandler.Play("Content/Sound/fruit.wav", false);
        }

        private void CheckWarping()
        {
            CheckPlayerWarping();
            CheckGhostWarping();
        }

        private void CheckGhostWarping()
        {
            foreach (var ghost in ghosts)
            {
                if (ShouldBeginWarping(ghost))
                    ghost.BeginWarping();
                if (ShouldEndWarping(ghost))
                    ghost.EndWarping();
                WarpIfOffScreen(ghost);
            }
        }

        private void CheckPlayerWarping()
        {
            if (ShouldBeginWarping(player))
                player.BeginWarping();
            if (ShouldEndWarping(player))
                player.ReturnToMovementState();
            WarpIfOffScreen(player);
        }

        private void CheckIfTouchingGhosts(PacmanObject obj)
        {
            if (victoryAlreadyReached || (!obj.IsMoving && !obj.IsWarping))
                return;
            foreach (var touchedGhost in ghosts.Where(ghost => ghost.TilePosition.Equals(obj.TilePosition)))
            {
                if (touchedGhost.IsFrightened && GhostsShouldTurnBlue)
                    HandleGhostEaten(obj, touchedGhost);
                else if (!touchedGhost.IsRespawning)
                {
                    HandlePlayerDeath(obj);
                    break;
                }
            }
        }

        private void HandlePlayerDeath(PacmanObject obj)
        {
            playerHasLostLifeThisLevel = true;
            if (LivesRemaining > 0)
            {
                obj.BeginRespawning(BeginRespawningPlayer);
                --LivesRemaining;
                foreach (var ghost in ghosts)
                {
                    ghost.PelletCounterEnabled = false;
                    ghost.CruiseElroyMode = 0;
                }
                globalPelletCounterEnabled = true;
                globalPelletCounter = 0;
            }
            else
            {
                obj.BeginDeath(() =>
                {
                    player = null;
                    actionQueue.Add((HighscoreScreenDelay, () =>
                    {
                        State = GameState.Highscores;
                        ShowHighscoreScreen();
                        Score = 0;
                    }
                    ));
                });
            }
            obj.PreventMovement = true;
            foreach (var ghost in ghosts)
                Despawn(ghost);
            ghosts.Clear();
            ghostObjectsEaten.Clear();
            Despawn(fruit);
            fruit = null;
            StopSirens();
            SoundHandler.Play("Content/Sound/death 1.wav", false);
        }

        private void HandleGhostEaten(PacmanObject eater, GhostObject eaten)
        {
            Score += GhostScore;
            ghostScoreObj = new ScoreObject(GraphicsHandler, GhostScore)
            {
                Position = eater.Position
            };
            eaten.BeginRespawning();
            GraphicsHandler.Hide(eater);
            GraphicsHandler.Hide(eaten);
            DisableMovement();
            ++ghostsEaten;

            pausePlayerOnEatingGhostTimer = EatGhostPauseDuration;
            ghostObjectsEaten.Add(eaten);
            foreach (var ghost in ghosts.Where(ghost => ghost.IsFrightened))
                ghost.PauseTimers = true;
            SoundHandler.Play("Content/Sound/ghost eat 7.wav", false);
            SoundHandler.Stop("Content/Sound/large pellet loop.wav");
        }

        private void AfterGhostEatenPause()
        {
            Despawn(ghostScoreObj);
            if (player != null)
                GraphicsHandler.Show(player);
            foreach (var ghost in ghostObjectsEaten)
                GraphicsHandler.Show(ghost);
            ghostObjectsEaten.Clear();
            EnableMovement();
            foreach (var ghost in ghosts.Where(ghost => ghost.IsFrightened))
                ghost.PauseTimers = false;
            pausePlayerOnEatingGhostTimer = TimeSpan.MaxValue;
            SoundHandler.Play("Content/Sound/ghost eat 2.wav", true);
        }

        private void CheckIfEatingPellets(PacmanObject obj)
        {
            HandleEatingNormalPellets(obj);
            HandleEatingPowerPellets(obj);
        }

        private void HandleEatingPowerPellets(PacmanObject obj)
        {
            List<PowerPelletObject> eaten =
                powerPellets.Where(pellet => obj.MouthBounds.IntersectsWith(pellet.EdibleBounds))
                .ToList();
            foreach (var pellet in eaten)
            {
                Score += PowerPelletObject.Worth;
                BeginPowerPelletEffects();
                Despawn(pellet);
                powerPellets.Remove(pellet);
            }
            if (eaten.Count > 0)
            {
                obj.FramesToDelayMotion = PowerPelletMovementDelayFrames;
                SoundHandler.Play("Content/Sound/munch A+B.wav", false);
                if (ghosts.Count(ghost => ghost.IsRespawning) == 0)
                    SoundHandler.Play("Content/Sound/large pellet loop.wav", true);
                StopSirens();
            }
        }

        private void StopSirens()
        {
            SoundHandler.Stop("Content/Sound/siren fast 2.wav");
            SoundHandler.Stop("Content/Sound/siren slow 3.wav");
            SoundHandler.Stop("Content/Sound/siren medium 3.wav");
        }

        private void HandleEatingNormalPellets(PacmanObject obj)
        {
            int prevPelletCount = pellets.Count;
            List<PelletObject> eaten =
                pellets.Where(pellet => obj.MouthBounds.IntersectsWith(pellet.EdibleBounds))
                .ToList();
            foreach (var pellet in eaten)
            {
                Score += PelletObject.Worth;
                Despawn(pellet);
                pellets.Remove(pellet);
            }
            if (pellets.Count <= FirstFruitThreshold && prevPelletCount > FirstFruitThreshold ||
                pellets.Count <= SecondFruitThreshold && prevPelletCount > SecondFruitThreshold)
                SpawnFruit();
            if (eaten.Count > 0)
            {
                foreach (var ghost in ghosts)
                {
                    if (ghost.IsHome && ghost.PelletCounterEnabled)
                        ++ghost.PelletCounter;
                }
                if (globalPelletCounterEnabled)
                    UpdateGlobalPelletCounter();
                pelletTimer = PelletTimerInterval;
                obj.FramesToDelayMotion = PelletMovementDelayFrames;
                SoundHandler.Play("Content/Sound/munch A+B.wav", false);
            }
        }

        private void UpdateGlobalPelletCounter()
        {
            ++globalPelletCounter;
            if (GhostToRelease != null)
                GhostToRelease.ExitingGhostHouse = (GhostToRelease.Behavior as GhostAIBehavior).GlobalPelletReleaseReached(globalPelletCounter);
        }

        private bool ShouldBeginWarping(PacmanObject obj)
            => level.WarpTunnelStarts.Contains(obj.TilePosition)
            && obj.IsMoving
            && obj.IsFacingMazeEdge;

        private bool ShouldEndWarping(PacmanObject obj)
            => level.WarpTunnelStarts.Contains(obj.TilePosition)
            && obj.IsWarping
            && !obj.TilePosition.Equals(obj.WarpStartPosition);

        private bool ShouldBeginWarping(GhostObject obj)
            => level.WarpTunnelStarts.Contains(obj.TilePosition)
            && !obj.IsWarping && !obj.IsRespawning
            && obj.IsFacingMazeEdge;

        private bool ShouldEndWarping(GhostObject obj)
            => level.WarpTunnelStarts.Contains(obj.TilePosition)
            && obj.IsWarping
            && !obj.TilePosition.Equals(obj.WarpStartPosition);

        private bool OutsideGameArea(GameObject obj)
        {
            return obj.RightSideRightOf(GraphicsHandler.GameArea.Right)
                || obj.LeftSideLeftOf(GraphicsHandler.GameArea.Left)
                || obj.TopAbove(GraphicsHandler.GameArea.Top)
                || obj.BottomBelow(GraphicsHandler.GameArea.Bottom);
        }

        private void PushOutOfWalls(GameObject obj) => PushOutOfWalls(obj, walls);

        internal static void PushOutOfWalls(GameObject obj, IReadOnlyCollection<RectangleF> walls)
        {
            foreach (RectangleF wall in walls.Where(wall => obj.Bounds.IntersectsWith(wall)))
            {
                if (obj.CollidingFromAbove(wall))
                    obj.Position.Y -= (obj.Bottom - wall.Top);
                if (obj.CollidingFromBelow(wall))
                    obj.Position.Y -= (obj.Top - wall.Bottom);
                if (obj.CollidingFromLeft(wall))
                    obj.Position.X -= (obj.Right - wall.Left);
                if (obj.CollidingFromRight(wall))
                    obj.Position.X -= (obj.Left - wall.Right);
            }
        }
        #endregion

        #region Fruit
        private void SpawnFruit()
        {
            fruit = new FruitObject(GraphicsHandler, GetFruitFromLevelNumber())
            {
                Position = level.FruitSpawn + new Vector2(GraphicsConstants.TileWidth / 2, GraphicsConstants.TileWidth / 2)
            };
            fruitDespawnTimer = TimeSpan.FromSeconds(new Random().NextDouble() + MinimumFruitAppearanceDurationInSeconds);
        }

        private GraphicsID GetFruitFromLevelNumber() => GetFruitFromLevelNumber(levelNumber);

        private static GraphicsID GetFruitFromLevelNumber(int levelNumber)
        {
            if (levelNumber == 0)
                return GraphicsID.SpriteCherry;
            else if (levelNumber < LastStrawberryLevel)
                return GraphicsID.SpriteStrawberry;
            else if (levelNumber < LastOrangeLevel)
                return GraphicsID.SpriteOrange;
            else if (levelNumber < LastAppleLevel)
                return GraphicsID.SpriteApple;
            else if (levelNumber < LastMelonLevel)
                return GraphicsID.SpriteMelon;
            else if (levelNumber < LastGalaxianLevel)
                return GraphicsID.SpriteGalaxian;
            else if (levelNumber < LastBellLevel)
                return GraphicsID.SpriteBell;
            else
                return GraphicsID.SpriteKey;
        }
        #endregion

        #region Gameplay Effects
        private void TurnPlayerOnCollision(GameObject obj, bool collided)
        {
            if (collided && obj is PacmanObject)
            {
                var pacman = obj as PacmanObject;
                pacman.PerformTurn(
                    Enum.GetValues(typeof(Direction)).Cast<Direction?>()
                    .Where(dir => dir != pacman.Orientation && dir != pacman.Orientation.GetOpposite() && player.CanTurnTo(walls, pacman.DirectionVelocity(dir.Value))).FirstOrDefault());
            }
        }

        private void WarpIfOffScreen(GameObject obj)
        {
            if (obj.Right < GraphicsHandler.GameArea.Left)
            {
                obj.Position = new Vector2(GraphicsHandler.GameArea.Right + obj.Size.Width / 2, obj.Position.Y);
            }
            if (obj.Left > GraphicsHandler.GameArea.Right)
            {
                obj.Position = new Vector2(GraphicsHandler.GameArea.Left - obj.Size.Width / 2, obj.Position.Y);
            }
            if (obj.Top > GraphicsHandler.GameArea.Bottom)
            {
                obj.Position = new Vector2(obj.Position.X, GraphicsHandler.GameArea.Top - obj.Size.Width / 2);
            }
            if (obj.Bottom < GraphicsHandler.GameArea.Top)
            {
                obj.Position = new Vector2(obj.Position.X, GraphicsHandler.GameArea.Bottom + obj.Size.Width / 2);
            }
        }

        private void StartNextLevel(TimeSpan extraDelay)
        {
            pelletTimer = TimeSpan.MaxValue;
            ghostModeTimer = TimeSpan.MaxValue;
            fruitDespawnTimer = TimeSpan.MaxValue;
            pausePlayerOnEatingGhostTimer = TimeSpan.MaxValue;

            Paused = false;

            Despawn(fruit);
            fruit = null;
            if (ghosts != null)
            {
                foreach (var ghost in ghosts)
                    Despawn(ghost);
                ghosts.Clear();
            }
            Despawn(player);
            player = null;
            Despawn(ghostScoreObj);
            ghostScoreObj = null;
            ghostObjectsEaten?.Clear();

            victoryAlreadyReached = false;
            globalPelletCounter = 0;
            globalPelletCounterEnabled = false;
            GraphicsHandler.PreventAnimatedSpriteUpdates = true;

            InitLevel();
            player.PerformTurn(Direction.Right);
            DelayStart(extraDelay);

            pelletTimer = PelletTimerInterval;
            DrawFruits();
        }

        private void DrawFruits()
        {
            int toDraw = Math.Min(MaxFruitsToDraw, levelNumber + 1);
            int temp = levelNumber - (toDraw - 1);
            while (toDraw > 0)
            {
                Tiles.DrawFruit(UpperBonusFruitRow, RightmostBonusFruitLeftColumn - (toDraw - 1) * 2, GetFruitFromLevelNumber(temp));
                ++temp;
                --toDraw;
            }
        }

        private void BeginRespawningPlayer()
        {
            Despawn(player);
            player = null;
            foreach (var ghost in ghosts)
                Despawn(ghost);
            ghosts.Clear();
            Despawn(ghostScoreObj);
            ghostScoreObj = null;
            ghostObjectsEaten.Clear();

            ghostModeTimer = TimeSpan.MaxValue;
            pelletTimer = TimeSpan.MaxValue;
            pausePlayerOnEatingGhostTimer = TimeSpan.MaxValue;

            actionQueue.Add((RespawnPlayerDelay, RespawnPlayer));
        }

        private void RespawnPlayer()
        {
            GhostPhase = 0;

            SpawnPlayer(level);
            SpawnGhosts(level);
            DisableMovement();
            DelayStart();
        }

        private void BeginPowerPelletEffects()
        {
            foreach (var ghost in ghosts)
                if (!ghost.IsRespawning && !ghost.IsHome)
                    ghost.BecomeFrightened(GhostsShouldTurnBlue);
            ghostsEaten = 0;
        }

        private int CruiseElroyThreshold
        {
            get
            {
                if (levelNumber == 0)
                    return 20;
                else if (levelNumber == 1)
                    return 30;
                else if (levelNumber < 5)
                    return 40;
                else if (levelNumber < 8)
                    return 50;
                else if (levelNumber < 11)
                    return 60;
                else if (levelNumber < 14)
                    return 80;
                else if (levelNumber < 18)
                    return 100;
                else
                    return 120;
            }
        }
        private int CruiseElroyThreshold2
        {
            get
            {
                if (levelNumber == 0)
                    return 10;
                else if (levelNumber == 1)
                    return 15;
                else if (levelNumber < 5)
                    return 20;
                else if (levelNumber < 8)
                    return 25;
                else if (levelNumber < 11)
                    return 30;
                else if (levelNumber < 14)
                    return 40;
                else if (levelNumber < 18)
                    return 50;
                else
                    return 60;
            }
        }

        private void DisableMovement()
        {
            player.PreventMovement = true;
            foreach (var ghost in ghosts)
                ghost.PreventMovement = true;
        }

        private void EnableMovement()
        {
            if (player != null)
                player.PreventMovement = false;
            foreach (var ghost in ghosts)
                ghost.PreventMovement = false;
        }

        private TimeSpan GhostModePhaseDuration()
        {
            if (levelNumber < 2)
            {
                if (GhostPhase == 0 || GhostPhase == 2)
                    return TimeSpan.FromSeconds(7);
                else if (GhostPhase == 4 || GhostPhase == 6)
                    return TimeSpan.FromSeconds(5);
                else
                    return TimeSpan.FromSeconds(20);
            }
            else
            {
                if (GhostPhase == 0 || GhostPhase == 2)
                    return TimeSpan.FromSeconds((levelNumber < 5) ? 7 : 5);
                else if (GhostPhase == 1 || GhostPhase == 3)
                    return TimeSpan.FromSeconds(20);
                else if (GhostPhase == 4)
                    return TimeSpan.FromSeconds(5);
                else if (GhostPhase == 5)
                    return TimeSpan.FromSeconds((levelNumber < 5) ? 1033 : 1037);
                else
                    return TimeSpan.FromSeconds(1.0d / 60);
            }
            throw new Exception("Unhandled ghost mode / level state.");
        }

        private void PlaySirens()
        {
            if (victoryAlreadyReached || player == null || player.PreventMovement == true || (!player.IsMoving || player.IsWarping) && ghosts.Count(ghost => ghost.IsFrightened) > 0)
                return;
            if (GhostPhase < 2)
            {
                SoundHandler.Stop("Content/Sound/siren fast 2.wav");
                SoundHandler.Stop("Content/Sound/siren medium 3.wav");
                SoundHandler.Play("Content/Sound/siren slow 3.wav", true);
            }
            else if (GhostPhase < 4)
            {
                SoundHandler.Stop("Content/Sound/siren fast 2.wav");
                SoundHandler.Stop("Content/Sound/siren slow 3.wav");
                SoundHandler.Play("Content/Sound/siren medium 3.wav", true);
            }
            else
            {
                SoundHandler.Stop("Content/Sound/siren medium 3.wav");
                SoundHandler.Stop("Content/Sound/siren slow 3.wav");
                SoundHandler.Play("Content/Sound/siren fast 2.wav", true);
            }
        }
        #endregion

        #region Game State
        private protected override void OnGameStateChanged()
        {
            base.OnGameStateChanged();
            SoundHandler.Dispose();
            if (State == GameState.Menu)
            {
                Score = 0;
                Animation = new MainMenuAnimation(GraphicsHandler, () => BeginMainMenuChase());
                AddCredit();
            }
            if (State == GameState.Menu || State == GameState.Playing)
            {
                AddTopScreenInfo();
            }
        }

        private protected override void ResetImpl()
        {
            Score = 0;
            State = GameState.Menu;
            Paused = false;
            fruit = null;
            ghosts.Clear();
            player = null;
            pellets.Clear();
            powerPellets.Clear();
            actionQueue.Clear();
            walls = null;
        }

        private void BeginMainMenuChase()
        {
            GameObjects["EatenPellet"] = new PowerPelletObject(GraphicsHandler)
            {
                Position = Vector2FromTilePosition(4.75, 20)
            };
            actionQueue.Add((MainMenuAnimationsDisabledDuration, () => { GraphicsHandler.PreventAnimatedSpriteUpdates = false; }));
            GameObjects["PacMan"] = new PacmanObject(GraphicsHandler, null)
            {
                Position = Vector2FromTilePosition(30, 19)
            };
            (GameObjects["PacMan"] as PacmanObject).PerformTurn(Direction.Left);
            GameObjects["PacMan"].Behavior = new MenuPacmanAIBehavior(GameObjects);
        }
        #endregion

        #region Tile Updates
        private void AddTopScreenInfo()
        {
            Tiles.DrawText(0, 3, OneUpText);
            Tiles.DrawText(0, 9, HighscoreText);
            Tiles.DrawText(0, 21, TwoUpText);
        }

        private void AddCredit()
        {
            Tiles.DrawText(35, 2, $"{CreditText}  {creditsRemaining}");
        }

        private protected override void UpdateScore(int value)
        {
            if (Score < OneUpThreshold && value >= OneUpThreshold)
            {
                ++LivesRemaining;
                SoundHandler.Play("Content/Sound/extra man.wav", false);
            }

            base.UpdateScore(value);
            if (value > DisplayedHighScore)
                DisplayedHighScore = value;
            if (State != GameState.Cutscene && State != GameState.Highscores)
                Tiles.DrawInteger(1, 6, Score);
        }

        private void UpdateHighScore()
        {
            Tiles.DrawInteger(1, 16, displayedHighScore);
        }

        private void UpdateLives()
        {
            foreach (var life in lives)
                Despawn(life);
            lives.Clear();
            for (int i = 0; i < LivesRemaining; ++i)
            {
                lives.Add(new GameObject(GraphicsConstants.SpriteSize)
                {
                    Position = Vector2FromTilePosition(1 + 2 * i, 35)
                });
                GraphicsHandler.SetStaticSprite(lives.Last(), GraphicsID.SpritePacmanMiddleRight, PaletteID.Pacman);
            }
        }

        private void RemoveDisplayedFruitScore(int fruitLeftCol, int fruitRow)
        {
            Tiles.DrawRange((fruitRow, fruitLeftCol), (fruitRow, fruitLeftCol + 3), GraphicsID.TileEmpty, PaletteID.Empty);
        }
        #endregion

        #region Game Start
        private void StartGame()
        {
            levelNumber = 0;
            State = GameState.Playing;
            Score = 0;
            SoundHandler.Disabled = true;
            LivesRemaining = StartingLives;
            StartNextLevel(TimeSpan.FromSeconds(1.3));
            UpdateHighScore();
            SoundHandler.Disabled = false;
            SoundHandler.Play("Content/Sound/intro.wav", false);
        }

        private void DelayStart() => DelayStart(TimeSpan.MaxValue);

        private void DelayStart(TimeSpan extraDelay)
        {
            Tiles.DrawText(20, 11, ReadyText, PaletteID.Pacman);
            TimeSpan delay = LevelStartDelay;
            if (extraDelay != TimeSpan.MaxValue)
                delay += extraDelay;
            actionQueue.Add((delay, EnableLevelPlay));
        }

        private void EnableLevelPlay()
        {
            pelletTimer = PelletTimerInterval;
            ghostModeTimer = GhostModePhaseDuration();
            Paused = false;
            player.PreventMovement = false;
            foreach (var ghost in ghosts)
                ghost.PreventMovement = false;
            GraphicsHandler.PreventAnimatedSpriteUpdates = false;
            Tiles.DrawRange((20, 11), (20, 16), GraphicsID.TileEmpty, PaletteID.Empty);
            player.PerformTurn(Direction.Right);
            GhostPhase = 0;
            ghostModeTimer = GhostModePhaseDuration();
        }
        #endregion

        #region Level Loading
        private void InitLevel()
        {
            level = Maze.Load(Resources.OriginalMaze);

            CreateLevelObjects(level);
            DisableMovement();
            DrawLevel(level);
        }

        private void DrawLevel(Maze level)
        {
            Tiles.DrawRange((4, 0), (35, 27), GraphicsID.TileEmpty, PaletteID.Empty);
            GraphicsHandler.CommitTiles(Tiles);
            level.Draw(Tiles);
        }

        private void CreateLevelObjects(Maze level)
        {
            // Must create walls before spawning player due to turn conditions
            CreateWalls(level);
            CreatePellets(level);
            CreatePowerPellets(level);
            SpawnPlayer(level);
            SpawnGhosts(level);
        }

        private void CreateWalls(Maze level)
        {
            walls = level.Walls;
        }

        private void SpawnGhosts(Maze level)
        {
            ghosts = new HashSet<GhostObject>
                (level.GhostSpawns.Select(spawn => new GhostObject(GraphicsHandler, spawn.Key, player, level)
                {
                    Position = new Vector2(spawn.Value.X + GraphicsConstants.TileWidth / 2, spawn.Value.Y + GraphicsConstants.TileWidth / 2),
                    ExitingGhostHouse = (spawn.Key == GhostType.Blinky),
                    LevelNumber = levelNumber
                }));
            if (ghosts.Count(ghost => ghost.Behavior is BlinkyAIBehavior) > 0)
                foreach (var ghost in ghosts.Where(ghost => ghost.Behavior is InkyAIBehavior))
                    (ghost.Behavior as InkyAIBehavior).Reference = ghosts.Where(g => g.Behavior is BlinkyAIBehavior).FirstOrDefault();
        }

        private void CreatePowerPellets(Maze level)
        {
            powerPellets = new List<PowerPelletObject>
                (level.PowerPellets.Select(position => new PowerPelletObject(GraphicsHandler)
                {
                    Position = new Vector2(position.X + GraphicsConstants.TileWidth / 2, position.Y + GraphicsConstants.TileWidth / 2)
                }));
        }

        private void CreatePellets(Maze level)
        {
            pellets = new List<PelletObject>
                (level.Pellets.Select(position => new PelletObject(GraphicsHandler)
                {
                    Position = new Vector2(position.X + GraphicsConstants.TileWidth / 2, position.Y + GraphicsConstants.TileWidth / 2)
                }));
        }

        private void SpawnPlayer(Maze level)
        {
            player = new PacmanObject(GraphicsHandler, walls)
            {
                Position = new Vector2(level.PlayerSpawn.X + GraphicsConstants.TileWidth / 2, level.PlayerSpawn.Y + GraphicsConstants.TileWidth / 2)
            };
        }
        #endregion

        #region Highscores
        private void LoadHighscores()
        {
            try
            {
                var serializer = new BinaryFormatter();
                using (var highscoreFile = File.OpenRead(@".\Highscores"))
                    currentHighscores = serializer.Deserialize(highscoreFile) as Highscores;
            }
            catch (SerializationException)
            {
                currentHighscores = new Highscores();
            }
            catch (IOException)
            {
                currentHighscores = new Highscores();
            }
            DisplayedHighScore = currentHighscores.Highscore;
        }

        private void SaveHighScores()
        {
            try
            {
                var serializer = new BinaryFormatter();
                using (var highscoreFile = File.OpenWrite(@".\Highscores"))
                    serializer.Serialize(highscoreFile, currentHighscores);
            }
            catch (IOException) { }
        }

        private PaletteID HighscoreEntryPalette(int i)
        {
            if (i % 5 == 0)
                return PaletteID.Blinky;
            else if (i % 5 == 1)
                return PaletteID.Pinky;
            else if (i % 5 == 2)
                return PaletteID.Inky;
            else if (i % 5 == 3)
                return PaletteID.Clyde;
            else
                return PaletteID.Pacman;
        }

        private void ShowHighscoreScreen()
        {
            Tiles.Clear();
            if (Score > currentHighscores.Minimum)
            {
                highscoreToChangeIndex = currentHighscores.AddScore(Score, "---");
                enteringHighscoreInitials = true;
                highscoreInitialToChangePosition = 0;
                Tiles.DrawText(30, 5, "PRESS ENTER TO SAVE");
                Tiles.DrawText(31, 5, "  CTRL-R TO RESET  ");
            }
            GraphicsHandler.CommitTiles(Tiles);
            Tiles.DrawText(1, 9, "HIGHSCORES", PaletteID.Pacman);
            for (int i = 0; i < currentHighscores.ToDisplay.Count; ++i)
            {
                Tiles.DrawInteger(4 + 2 * i, 3, i + 1, HighscoreEntryPalette(i));
                Tiles.DrawInteger(4 + 2 * i, 17, currentHighscores.ToDisplay[i].score, HighscoreEntryPalette(i));
                Tiles.DrawText(4 + 2 * i, HighscoresFirstInitialColumn, currentHighscores.ToDisplay[i].initials, HighscoreEntryPalette(i));
            }
        }
        #endregion
    }
}
