using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day20
{
    public class JurassicJigsaw : IDay
    {
        public int DayNumber => 20;
        public string ValidatedPart1 => "8425574315321";
        public string ValidatedPart2 => string.Empty;

        private readonly List<Tile> _tiles;

        private JurassicJigsaw(List<Tile> tiles)
        {
            _tiles = tiles;
        }

        public static JurassicJigsaw LoadFromFile(string filename)
        {
            var lines = File.ReadAllLines(filename).ToList();
            lines.Add(string.Empty);

            var tiles = new List<Tile>();
            var tilelines = new List<string>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    var tile = new Tile(tilelines);
                    tiles.Add(tile);
                    tilelines = new List<string>();
                }
                else
                {
                    tilelines.Add(line);
                }
            }

            return new JurassicJigsaw(tiles);
        }

        public string Part1()
        {
            var lookups = _tiles
                .SelectMany(t => t.GetLookups());

            var groups = lookups.GroupBy(l => l.Edge);

            foreach (var group in groups)
            {
                foreach (var tile1 in group)
                {
                    foreach (var tile2 in group)
                    {
                        if (tile1.Tile.TileId != tile2.Tile.TileId)
                        {
                            tile1.Tile.AddNeighbour(
                                new Neighbour
                                {
                                    NeighbourTile = tile2.Tile,
                                    ThisEdge = tile1.EdgeType,
                                    ThatEdge = tile2.EdgeType
                                });
                        }
                    }
                }
            }

            foreach (var tile in _tiles)
            {
                // Console.WriteLine($"Tile {tile.TileId}: {tile.Neighbours.Count} neighbours");
            }

            var corners = _tiles.Where(t => t.Neighbours.Count == 4);

            long result = 1;
            foreach (var corner in corners)
            {
                result *= corner.TileId;
            }

            return result.ToString();
        }

        public string Part2()
        {
            return string.Empty;
        }

        private class TileLookup
        {
            public string Edge { get; set; }
            public string EdgeType { get; set; }
            public Tile Tile { get; set; }
        }

        private class Neighbour
        {
            public string ThisEdge { get; set; }
            public string ThatEdge { get; set; }
            public Tile NeighbourTile { get; set; }
        }

        private class Tile
        {
            public string Left { get; set; }
            public string Right { get; set; }
            public string Top { get; set; }
            public string Bottom { get; set; }

            public string LeftReverse { get; set; }
            public string RightReverse { get; set; }
            public string TopReverse { get; set; }
            public string BottomReverse { get; set; }

            public int TileId { get; private set; }
            public List<Neighbour> Neighbours { get; set; } = new List<Neighbour>();
            public Tile(List<string> lines)
            {
                TileId = int.Parse(lines[0]
                    .Replace("Tile ", string.Empty)
                    .Replace(":", string.Empty));

                var dataLines = lines.Skip(1).ToList();

                Top = dataLines.First();
                Bottom = dataLines.Last();
                Left = string.Join("", dataLines.Select(l => l.First()));
                Right = string.Join("", dataLines.Select(l => l.Last()));

                TopReverse = string.Join("", Top.Reverse());
                BottomReverse = string.Join("", Bottom.Reverse());
                LeftReverse = string.Join("", Left.Reverse());
                RightReverse = string.Join("", Right.Reverse());
            }

            public void AddNeighbour(Neighbour neighbour)
            {
                Neighbours.Add(neighbour);
            }

            public List<TileLookup> GetLookups()
            {
                return new List<TileLookup>
                {
                    new TileLookup { Edge = Top, EdgeType = "Top", Tile = this },
                    new TileLookup { Edge = Bottom, EdgeType = "Bottom", Tile = this },
                    new TileLookup { Edge = Left, EdgeType = "Left", Tile = this },
                    new TileLookup { Edge = Right, EdgeType = "Right", Tile = this },
                    new TileLookup { Edge = TopReverse, EdgeType = "TopReverse", Tile = this },
                    new TileLookup { Edge = BottomReverse, EdgeType = "BottomReverse", Tile = this },
                    new TileLookup { Edge = LeftReverse, EdgeType = "LeftReverse", Tile = this },
                    new TileLookup { Edge = RightReverse, EdgeType = "RightReverse", Tile = this },
                };
            }
        }
    }
}
