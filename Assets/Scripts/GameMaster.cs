using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Unit selectedUnit;

    public void ResetTiles()
    {
        foreach (WaterTile tile in FindObjectsOfType<WaterTile>())
        {
            tile.Reset();
        }
    }
}
