using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : MonoBehaviour {

    private SpriteRenderer rend;
    //array of type Sprite
    public Sprite[] tileGrapics;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

}
