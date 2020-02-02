using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public bool selected;
    GameMaster gm;

    public int tileSpeed;
    public bool hasMoved;

    public float moveSpeed;

    public int playerNumber;

    public int attackRange;
    List<Unit> enemiesInRange = new List<Unit>();
    public bool hasAttacked;

    public GameObject weaponIcon;

    public int health;
    public int attackDamage;
    public int defenseDamage;
    public int armor;

    public DamageIcon damageIcon;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    private void OnMouseDown()
    {
        ResetWeaponIcon();
        if (selected == true)
        {
            selected = false;
            gm.selectedUnit = null;
            gm.ResetTiles();
        }
        else
        {
            if (playerNumber == gm.playerTurn)
            {
                if (gm.selectedUnit != null)
                {
                    gm.selectedUnit.selected = false;
                }

                selected = true;
                gm.selectedUnit = this;

                gm.ResetTiles();
                GetEnemies();
                GetWalkableTiles();
            }
        }

        Collider2D col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.15f);
        Unit unit = col.GetComponent<Unit>(); // double check that what we clicked on is a unit
        if (gm.selectedUnit != null)
        {
            if (gm.selectedUnit.enemiesInRange.Contains(unit) && !gm.selectedUnit.hasAttacked)
            { // does the currently selected unit have in his list the enemy we just clicked on
                gm.selectedUnit.Attack(unit);

            }
        }
    }

    void Attack(Unit enemy)
    {
        hasAttacked = true;

        int enemyDamege = attackDamage - enemy.armor;
        int myDamage = enemy.defenseDamage - armor;

        if (enemyDamege >= 1)
        {
             
            enemy.health -= enemyDamege;
        }

        if (myDamage >= 1)
        {
            DamageIcon instance = Instantiate(damageIcon, transform.position, Quaternion.identity);
            instance.Setup(myDamage);
            health -= myDamage;
        }

        if(enemy.health<=0)
        {
            Destroy(enemy.gameObject);
            GetWalkableTiles();
        }

        if (health <= 0)
        {
            gm.ResetTiles();
            Destroy(this.gameObject);
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


    void GetEnemies()
    {

        enemiesInRange.Clear();

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            if (Mathf.Abs(transform.position.x - unit.transform.position.x) + Mathf.Abs(transform.position.y - unit.transform.position.y) <= attackRange) // check is the enemy is near enough to attack
            {
                if (unit.playerNumber != gm.playerTurn && !hasAttacked)
                { // make sure you don't attack your allies
                    enemiesInRange.Add(unit);
                    unit.weaponIcon.SetActive(true);
                }

            }
        }
    }


    public void ResetWeaponIcon()
    {
        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unit.weaponIcon.SetActive(false);
        }
    }


    public void Move(Vector2 tilePos)
    {
        gm.ResetTiles();
        StartCoroutine(StartMovement(tilePos));
    }

    IEnumerator StartMovement(Vector2 tilePos)
    {
        while(transform.position.x != tilePos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(tilePos.x,transform.position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }
        while (transform.position.y != tilePos.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, tilePos.y), moveSpeed * Time.deltaTime);
            yield return null;
        }
        hasMoved = true;
        ResetWeaponIcon();
        GetEnemies();
    }
}

