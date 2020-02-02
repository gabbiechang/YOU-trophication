using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public bool selected;
    GameMaster gm;

    public int tileSpeed;
    public bool hasMoved;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    private void OnMouseDown()
    {
        if (selected == true)
        {
            selected = false;
            gm.selectedUnit = null; 
        }
        else
        {
            if (gm.selectedUnit != null)
            {
                gm.selectedUnit.selected = false;
            }

            selected = true;
            gm.selectedUnit = this;

            GetWalkableTiles();
        }
    }

    void GetWalkableTiles()
    { // Looks for the tiles the unit can walk on
        if (hasMoved == true)
        {
            return;
        }

        WaterTile[] tiles = FindObjectsOfType<WaterTile>();
        foreach (WaterTile tile in tiles)
        {
            if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= tileSpeed)
            { // how far he can move
                if (tile.IsClear() == true)
                { // is the tile clear from any obstacles
                    tile.Highlight();
                }

            }
        }
    }

}

