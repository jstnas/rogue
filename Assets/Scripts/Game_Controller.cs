using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
    private bool _gameActive = true;
    private bool _playerTurn = true;

    private Grid _grid;
    private Ground _ground;
    

    private void Awake()
    {
        _grid = FindObjectOfType<Grid>();
        _ground = _grid.GetComponent<Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        
            if(_playerTurn)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    _playerTurn = !_playerTurn;
                }

            }
            else
            {
                foreach (Movement enemy in _ground.GetEnemyList())
                {
                   ZombieCharacter zombie =  enemy.GetComponent<ZombieCharacter>();
                   zombie.turn();
                print("check 1");
                }
                _playerTurn = !_playerTurn;
            }
            
        
    }
}
