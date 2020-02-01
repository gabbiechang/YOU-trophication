using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : MonoBehaviour {

    private SpriteRenderer rend;
    //array of type Sprite
    public Sprite[] tileGrapics;

    //controls intensity of hovering effect
    public float hoverAmount;


    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        int randTile = Random.Range(0, tileGrapics.Length);
        rend.sprite = tileGrapics[randTile];
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
}
