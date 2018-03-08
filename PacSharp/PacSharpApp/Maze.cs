using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using PacSharpApp.AI;
using PacSharpApp.Graphics;
using PacSharpApp.Utils;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    class Maze
    {
        private readonly TileCollection tiles;

        private Maze(TileCollection tiles,
            List<RectangleF> walls,
            (List<Vector2> pellets, List<Vector2> powerPellets) pelletInfo, 
            Vector2 playerSpawn,
            IDictionary<GhostType, Vector2> ghostSpawns,
            Vector2 fruitSpawn)
        {
            this.tiles = tiles;
            Walls = walls;
            Pellets = pelletInfo.pellets;
            PowerPellets = pelletInfo.powerPellets;
            PlayerSpawn = playerSpawn;
            GhostSpawns = new ReadOnlyDictionary<GhostType, Vector2>(ghostSpawns);
            FruitSpawn = fruitSpawn;
        }

        internal IReadOnlyCollection<RectangleF> Walls { get; }
        internal IReadOnlyCollection<Vector2> Pellets { get; }
        internal IReadOnlyCollection<Vector2> PowerPellets { get; }
        internal Vector2 PlayerSpawn { get; }
        internal IReadOnlyDictionary<GhostType, Vector2> GhostSpawns { get; }
        internal Vector2 FruitSpawn { get; }

        internal void Draw(TileCollection dest)
        {
            for (int r = 0; r < tiles.Height; ++r)
                for (int c = 0; c < tiles.Width; ++c)
                {
                    if ((tiles[r, c]?.GraphicsID ?? GraphicsID.TileEmpty) == GraphicsID.TileEmpty)
                        continue;
                    dest.SetTile(r, c, tiles[r, c].GraphicsID,
                        (tiles[r, c].GraphicsID == GraphicsID.TileMazeGateLeft) ? PaletteID.MazeGate : PaletteID.Maze);
                }
        }

        #region Loading
        private const int PelletTileGraphicsID = 17;
        private const int PowerPelletTileGraphicsID = 21;
        private const string LayerElementName = "layer";
        private const string DataElementName = "data";
        private const string ObjectElementName = "object";
        private const string WidthAttribute = "width";
        private const string HeightAttribute = "height";
        private const string XAttribute = "x";
        private const string YAttribute = "y";
        private const string NameAttribute = "name";
        private const string TypeAttribute = "type";
        private const string WallObjectType = "Wall";
        private const string PlayerObjectType = "Player";
        private const string GhostObjectType = "Ghost";
        private const string PelletLayerName = "Pellets";
        private const string MazeLayerName = "Maze";
        private const string FruitObjectType = "Fruit";

        internal static Maze Load(string xml)
        {
            var mazeXml = XDocument.Parse(xml);
            XElement map = mazeXml.Root;
            (int mapWidth, int mapHeight) =
                (int.Parse(map.Attribute(WidthAttribute).Value), 
                 int.Parse(map.Attribute(HeightAttribute).Value));

            return new Maze(
                ReadTiles(mazeXml, mapWidth, mapHeight),
                ReadWalls(mazeXml),
                ReadPellets(mazeXml, mapWidth, mapHeight),
                ReadPlayerSpawn(mazeXml),
                ReadGhostSpawns(mazeXml),
                ReadFruitSpawn(mazeXml));
        }

        private static Vector2 ReadFruitSpawn(XDocument mazeXml)
        {
            return mazeXml.Root.Descendants(ObjectElementName)
                .Where(obj => obj.Attribute(TypeAttribute).Value == FruitObjectType)
                .Select(fruit => new Vector2(float.Parse(fruit.Attribute(XAttribute).Value), float.Parse(fruit.Attribute(YAttribute).Value)))
                .First();
        }

        private static IDictionary<GhostType, Vector2> ReadGhostSpawns(XDocument mazeXml)
        {
            var ghostSpawns = new Dictionary<GhostType, Vector2>();
            var spawnInfo = 
                mazeXml.Root.Descendants(ObjectElementName)
                .Where(obj => obj.Attribute(TypeAttribute).Value == GhostObjectType)
                .Select(ghost =>
                (
                    (GhostType)Enum.Parse(
                        typeof(GhostType),
                        ghost.Attribute(NameAttribute).Value),
                    new Vector2(
                        float.Parse(ghost.Attribute(XAttribute).Value),
                        float.Parse(ghost.Attribute(YAttribute).Value))
                ));
            foreach ((GhostType type, Vector2 location) in spawnInfo)
                ghostSpawns.Add(type, location);
            return ghostSpawns;
        }

        private static Vector2 ReadPlayerSpawn(XDocument mazeXml)
        {
            return mazeXml.Root.Descendants(ObjectElementName)
                .Where(obj => obj.Attribute(TypeAttribute).Value == PlayerObjectType)
                .Select(player => new Vector2(float.Parse(player.Attribute(XAttribute).Value), float.Parse(player.Attribute(YAttribute).Value)))
                .First();
        }

        private static (List<Vector2> pellets, List<Vector2> powerPellets) ReadPellets(XDocument mazeXml, int mapWidth, int mapHeight)
        {
            int[] pelletData =
                mazeXml.Root.Descendants(LayerElementName)
                .Where(layer => layer.Attribute(NameAttribute).Value == PelletLayerName)
                .First()
                .Element(DataElementName).Value
                .Split(',')
                .Select(tile => int.Parse(tile))
                .ToArray();

            List<Vector2> pellets = new List<Vector2>();
            List<Vector2> powerPellets = new List<Vector2>();

            for (int r = 0; r < mapHeight; ++r)
                for (int c = 0; c < mapWidth; ++c)
                {
                    int tileID = pelletData[r * mapWidth + c];
                    if (tileID == PelletTileGraphicsID)
                        pellets.Add(new Vector2(c * GraphicsConstants.TileWidth, r * GraphicsConstants.TileWidth));
                    else if (tileID == PowerPelletTileGraphicsID)
                        powerPellets.Add(new Vector2(c * GraphicsConstants.TileWidth, r * GraphicsConstants.TileWidth));
                }

            return (pellets, powerPellets);
        }

        private static List<RectangleF> ReadWalls(XDocument mazeXml)
        {
            return new List<RectangleF>(
                mazeXml.Root.Descendants(ObjectElementName)
                .Where(obj => obj.Attribute(TypeAttribute).Value == WallObjectType)
                .Select(wall => new RectangleF(
                    float.Parse(wall.Attribute(XAttribute).Value),
                    float.Parse(wall.Attribute(YAttribute).Value),
                    float.Parse(wall.Attribute(WidthAttribute).Value),
                    float.Parse(wall.Attribute(HeightAttribute).Value)
                    )));
        }

        private static TileCollection ReadTiles(XDocument mazeXml, int mapWidth, int mapHeight)
        {
            int[] tileData =
                mazeXml.Root.Descendants(LayerElementName)
                .Where(layer => layer.Attribute(NameAttribute).Value == MazeLayerName)
                .First()
                .Element(DataElementName).Value
                .Split(',')
                .Select(tile => int.Parse(tile))
                .ToArray();

            var tiles = new TileCollection(mapWidth, mapHeight);

            for (int r = 0; r < mapHeight; ++r)
                for (int c = 0; c < mapWidth; ++c)
                {
                    int tileID = tileData[r * mapWidth + c] - 1; // 0 is no tile
                    if (tileID < 0)
                        continue;
                    tiles.SetTile(r, c, GraphicsUtils.TileIDFromLocation(tileID / 16, tileID % 16), PaletteID.Maze);
                }

            return tiles;
        }
        #endregion
    }
}
