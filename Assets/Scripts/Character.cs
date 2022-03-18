using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Character: MonoBehaviour
{

    public int Health = 100;
    public int Attack_Range = 1;
    public int Damage = 50;
    private Ground _ground;
    private Grid _grid;
    private Movement _myMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(Health <= 0)
        {
            Destroy(gameObject);
        } 
    }

    private void Awake()
    {
        _grid = FindObjectOfType<Grid>();
        _ground = _grid.GetComponent<Ground>();
        _myMovement = GetComponent<Movement>();
    }

    protected void Attack(Vector3 pos, bool isPlayer)
    {
        if (isPlayer)
        {
            foreach (Movement enemy in _ground.GetEnemyList())
            {

                if (enemy.GetCellPosition() == _grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    print("enemy here");

                    if (Vector3.Distance(enemy.GetCellPosition(), _myMovement.GetCellPosition()) <= Attack_Range)
                    {
                        Character enem = enemy.GetComponent<Character>();
                        enem.Health -= Damage;
                        //print(Vector3.Distance(enemy.GetCellPosition(),_myMovement.GetCellPosition()));
                    }
                }
            }
        }
        else
        {

        }
    }
}
