using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using PacSharpApp.AI;
using PacSharpApp.Graphics;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    class Maze
    {
        private readonly TileCollection tiles;

        private Maze(TileCollection tiles, List<Rectangle> walls, (List<Point> pellets, List<Point> powerPellets) pelletInfo, Point playerSpawn, IDictionary<GhostAIType, Point> ghostSpawns)
        {
            this.tiles = tiles;
            Walls = walls;
            Pellets = pelletInfo.pellets;
            PowerPellets = pelletInfo.powerPellets;
            PlayerSpawn = playerSpawn;
            GhostSpawns = new ReadOnlyDictionary<GhostAIType, Point>(ghostSpawns);
        }

        internal IReadOnlyCollection<Rectangle> Walls { get; }
        internal IReadOnlyCollection<Point> Pellets { get; }
        internal IReadOnlyCollection<Point> PowerPellets { get; }
        internal Point PlayerSpawn { get; }
        internal IReadOnlyDictionary<GhostAIType, Point> GhostSpawns { get; }

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
                ReadGhostSpawns(mazeXml));
        }

        private static IDictionary<GhostAIType, Point> ReadGhostSpawns(XDocument mazeXml)
        {
            var ghostSpawns = new Dictionary<GhostAIType, Point>();
            var spawnInfo = 
                mazeXml.Root.Descendants(ObjectElementName)
                .Where(obj => obj.Attribute(TypeAttribute).Value == GhostObjectType)
                .Select(ghost =>
                (
                    (GhostAIType)Enum.Parse(
                        typeof(GhostAIType),
                        ghost.Attribute(NameAttribute).Value),
                    new Point(
                        int.Parse(ghost.Attribute(XAttribute).Value),
                        int.Parse(ghost.Attribute(YAttribute).Value))
                ));
            foreach ((GhostAIType type, Point location) in spawnInfo)
                ghostSpawns.Add(type, location);
            return ghostSpawns;
        }

        private static Point ReadPlayerSpawn(XDocument mazeXml)
        {
            return mazeXml.Root.Descendants(ObjectElementName)
                .Where(obj => obj.Attribute(TypeAttribute).Value == PlayerObjectType)
                .Select(player => new Point(int.Parse(player.Attribute(XAttribute).Value), int.Parse(player.Attribute(YAttribute).Value)))
                .First();
        }

        private static (List<Point> pellets, List<Point> powerPellets) ReadPellets(XDocument mazeXml, int mapWidth, int mapHeight)
        {
            int[] pelletData =
                mazeXml.Root.Descendants(LayerElementName)
                .Where(layer => layer.Attribute(NameAttribute).Value == PelletLayerName)
                .First()
                .Element(DataElementName).Value
                .Split(',')
                .Select(tile => int.Parse(tile))
                .ToArray();

            List<Point> pellets = new List<Point>();
            List<Point> powerPellets = new List<Point>();

            for (int r = 0; r < mapHeight; ++r)
                for (int c = 0; c < mapWidth; ++c)
                {
                    int tileID = pelletData[r * mapWidth + c];
                    if (tileID == PelletTileGraphicsID)
                        pellets.Add(new Point(c * GraphicsConstants.TileWidth, r * GraphicsConstants.TileWidth));
                    else if (tileID == PowerPelletTileGraphicsID)
                        powerPellets.Add(new Point(c * GraphicsConstants.TileWidth, r * GraphicsConstants.TileWidth));
                }

            return (pellets, powerPellets);
        }

        private static List<Rectangle> ReadWalls(XDocument mazeXml)
        {
            return new List<Rectangle>(
                mazeXml.Root.Descendants(ObjectElementName)
                .Where(obj => obj.Attribute(TypeAttribute).Value == WallObjectType)
                .Select(wall => new Rectangle(
                    int.Parse(wall.Attribute(XAttribute).Value),
                    int.Parse(wall.Attribute(YAttribute).Value),
                    int.Parse(wall.Attribute(WidthAttribute).Value),
                    int.Parse(wall.Attribute(HeightAttribute).Value)
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
                    tiles.SetTile(r, c, GraphicsUtils.IDFromLocation(tileID / 16, tileID % 16), PaletteID.Maze);
                }

            return tiles;
        }
        #endregion
    }
}
