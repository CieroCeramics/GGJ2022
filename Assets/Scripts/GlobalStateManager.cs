using UnityEngine;
using System.Collections;
 using System.Collections.Generic;
public class GlobalStateManager : MonoBehaviour
{
    public List <Tile> puzzle;
    public Dictionary<Vector2Int, Tile> Tiles;

    public void StoreTiles ()
    {
        for (int i = 0; i < puzzle.Count; i ++)
        {

            Tiles.Add(puzzle[i].coordinate, puzzle[i]);
        }
    }

    void Start()
    {
        StoreTiles();
    }
}



// We will need a Level Manager (We will have 1 per puzzle) where each tile will need to be stored in an array or List Tile[], then at runtime will be converted intoDictionary<Vector2Int, Tile>

// I will add Vector2Int coordinate; to the Tile base class, that you can set upon generation 