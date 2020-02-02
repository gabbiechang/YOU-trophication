using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : MonoBehaviour {

    private SpriteRenderer rend;
    //array of type Sprite
    public Sprite[] tileGrapics;

    //controls intensity of hovering effect
    public float hoverAmount;

    public LayerMask obstacleLayer;

    public Color highlightedColor;
    public bool isWalkable;
    GameMaster gm;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        int randTile = Random.Range(0, tileGrapics.Length);
        rend.sprite = tileGrapics[randTile];

        gm = FindObjectOfType<GameMaster>();
    }

    //built in function; called automatically when mouse hovers over box collider
    public void OnMouseEnter()
    {
        transform.localScale += Vector3.one * hoverAmount;
    }

    public void OnMouseExit()
    {
        transform.localScale -= Vector3.one * hoverAmount;
    }

    public bool IsClear()
    {
        // Casts invis circle
        Collider2D obstacle = Physics2D.OverlapCircle(transform.position, 0.2f, obstacleLayer);
        // if there's an obstacle on the tile
        if (obstacle != null)
        {
            // tile is not clear
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Highlight()
    {

        rend.color = highlightedColor;
        isWalkable = true;
    }

    public void Reset()
    {
        rend.color = Color.white;
        isWalkable = false;
        isCreatable = false;
    }
}
