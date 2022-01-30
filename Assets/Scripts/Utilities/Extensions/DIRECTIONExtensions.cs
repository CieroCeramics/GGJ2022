using System;
using UnityEngine;

namespace Utilities.Extensions
{
    public static class DIRECTIONExtensions
    {
        public static Vector2Int DirectionAsDirectionVector(this RiverStartTile.DIRECTION direction)
        {
            switch (direction)
            {
                case RiverStartTile.DIRECTION.LEFT:
                    return Vector2Int.left;
                case RiverStartTile.DIRECTION.RIGHT:
                    return Vector2Int.right;
                case RiverStartTile.DIRECTION.FORWARD:
                    return Vector2Int.up;
                case RiverStartTile.DIRECTION.BACKWARD:
                    return Vector2Int.down;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    
        public static Vector3 DirectionAsDirectionVector3(this RiverStartTile.DIRECTION direction)
        {
            switch (direction)
            {
                case RiverStartTile.DIRECTION.LEFT:
                    return Vector3.left;
                case RiverStartTile.DIRECTION.RIGHT:
                    return Vector3.right;
                case RiverStartTile.DIRECTION.FORWARD:
                    return Vector3.forward;
                case RiverStartTile.DIRECTION.BACKWARD:
                    return Vector3.back;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}