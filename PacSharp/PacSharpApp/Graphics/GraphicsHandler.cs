using PacSharpApp.Objects;
using PacSharpApp.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Graphics
{
    /// <summary>
    /// Handles drawing logic before delegating to the actual control
    /// </summary>
    class GraphicsHandler : IDisposable
    {
        private IGameUI ui;
        private IDictionary<GameObject, Sprite> gameObjectMap = new Dictionary<GameObject, Sprite>();
        private Image tileImage;
        private Image screenImage;

        internal GraphicsHandler(IGameUI ui, Control gameArea)
        {
            this.ui = ui;
            GameArea = new GameArea(GraphicsConstants.GridAreaSize, gameArea, OnPaint);
            screenImage = new Bitmap(GraphicsConstants.GridAreaSize.Width, GraphicsConstants.GridAreaSize.Height);
            tileImage = new Bitmap(GraphicsConstants.GridAreaSize.Width, GraphicsConstants.GridAreaSize.Height);
        }

        internal GameArea GameArea { get; }
        internal bool PreventAnimatedSpriteUpdates { get; set; } = false;

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (sender is Control control)
                e.Graphics.DrawImage(screenImage, new Rectangle(control.Location, control.Size));
        }

        internal void Clear()
        {
            gameObjectMap.Clear();
        }

        internal void CommitTiles(TileCollection tiles)
        {
            using (var tileGraphics = System.Drawing.Graphics.FromImage(tileImage))
            {
                for (int row = 0; row < tiles.Height; ++row)
                    for (int col = 0; col < tiles.Width; ++col)
                    {
                        if (!tiles[row, col].Updated)
                            continue;
                        Bitmap source = Resources.Tiles.Clone(GraphicsUtils.GetGraphicSourceRectangle(tiles[row, col].GraphicsID, GraphicsConstants.TileWidth, Resources.Tiles.Width / GraphicsConstants.TileWidth), Resources.Tiles.PixelFormat);
                        GraphicsUtils.SwapColors(source, tiles[row, col].Palette);
                        tileGraphics.DrawImage(source, new Point(col * GraphicsConstants.TileWidth, row * GraphicsConstants.TileWidth));
                        tiles[row, col].Updated = false;
                    }
            }
        }

        internal void UpdateStaticSprite(GameObject obj, GraphicsID id, PaletteID palette)
        {
            UpdateStaticSprite(obj, id, palette, Resources.Sprites, GraphicsConstants.SpriteWidth);
        }

        internal void UpdateStaticSprite(GameObject obj, GraphicsID id, PaletteID palette, Bitmap source, int width)
        {
            gameObjectMap[obj] = new StaticSprite(source, id, width, source.Width / width)
            {
                Palette = palette
            };
        }

        internal void UpdateAnimatedSprite(GameObject obj, AnimatedSprite sprite) => gameObjectMap[obj] = sprite;

        internal void RotateFlip(GameObject gameObject, RotateFlipType rfType) => gameObjectMap[gameObject].RotateFlip(rfType);

        internal void UpdateAnimatedSprites(TimeSpan elapsedTime)
        {
            if (PreventAnimatedSpriteUpdates)
                return;
            foreach (var sprite in gameObjectMap.Values)
                sprite.Update(elapsedTime);
        }

        internal void Draw(GameState state)
        {
            using (var screenGraphics = System.Drawing.Graphics.FromImage(screenImage))
            {
                screenGraphics.Clear(Color.Black);
                screenGraphics.DrawImage(tileImage, Point.Empty);
                foreach (var pair in gameObjectMap.OrderBy(pair => pair.Value.ZIndex))
                {
                    if (pair.Value.Visible)
                    {
                        GameObject obj = pair.Key;
                        Point location = obj.ScreenPosition(GameArea);
                        screenGraphics.DrawImage(pair.Value.Image, location);
                    }
                }
            }
            GameArea.Render(screenImage);
        }

        internal void Register(GameObject obj, Sprite sprite)
        {
            gameObjectMap.Add(obj, sprite);
        }
        
        internal void Close()
        {
            ui.Close();
        }

        internal void OnNewGame()
        {
            ui.OnNewGame();
        }

        public void Dispose()
        {
            tileImage.Dispose();
            screenImage.Dispose();
        }

        internal void Unregister(GameObject obj) => gameObjectMap.Remove(obj);
    }
}
