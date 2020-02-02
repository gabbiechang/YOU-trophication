using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Unit selectedUnit;
    public int playerTurn = 1;

    public void ResetTiles()
    {
        foreach (WaterTile tile in FindObjectsOfType<WaterTile>())
        {
            tile.Reset();
        }
    }

    private void EndTurn()
    {
        if (playerTurn == 1)
        {
            playerTurn = 2;
        }
        else if (playerTurn == 2)
        {
            playerTurn = 1;
        }
        if (selectedUnit != null)
        {
            selectedUnit.selected = false;
            selectedUnit = null;
        }
        ResetTiles();

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unit.hasMoved = false;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
    }
}
