using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Extensions
{
    public static class PuzzleTileDictionaryExtensions
    {
        public static Dictionary<Vector2Int, Tile> GetAllTilesInDirectionFrom(this Dictionary<Vector2Int, Tile> tiles, Vector2Int fromCoordinate, RiverStartTile.DIRECTION direction)
        {
            if (tiles == null)
                return default;
            
            if (tiles.ContainsKey(fromCoordinate) == false)
                return default;

            var dirVector = direction.DirectionAsDirectionVector();
            var coordinate = fromCoordinate + dirVector;

            var outList = new Dictionary<Vector2Int, Tile>();
            while (true)
            {
                if (tiles.TryGetValue(coordinate, out var tile) == false)
                    break;
                
                outList.Add(coordinate, tile);

                coordinate += dirVector;
            }

            return outList.Count == 0 ? default : outList;
        }
    }
}