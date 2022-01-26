using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractWithTile
{
    void EnterTile(Tile tile);

    void ExitTile(Tile tile);
}
