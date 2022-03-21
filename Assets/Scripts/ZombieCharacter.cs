using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCharacter : Character
{
    

    bool IsPlayerInRange()
    {
        PlayerMovement _Player = _ground.GetPlayer();
        if (Vector3.Distance(_myMovement.GetCellPosition(), _Player.GetCellPosition()) <= Attack_Range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void turn()
    {
        //this currently checks twice however will be changed when
        //decision making is implemented
        if(IsPlayerInRange())
        {
            PlayerMovement _Player = _ground.GetPlayer();
            Attack(_Player.GetCellPosition(), false);
            print("check 2");
        }
        else
        {
            _myMovement.Move();
        }

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
