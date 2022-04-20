using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision : MonoBehaviour
{

    [SerializeField] private Entities.Entity target;
    private Entities.EntityManager _entityManager;
    private PathFinding.Path _path;
    
    private void Awake()
    {
        if(_path = GetComponent<PathFinding.Path>())
        {
            Debug.Log("path found");
        }
        else
        {
            Debug.Log("path not found");
            Debug.Log(transform.name);
        }
        _entityManager = FindObjectOfType<Entities.EntityManager>();
        
    }

   
    public void AIDecision(Entities.Entity AIentity)
    {
        //move to player
        var path = _path.GetPath(AIentity.GetCellPosition(), target.GetCellPosition());
        var nextcell = path[0];
        var targetEntity = _entityManager.GetEntity(nextcell);
        List<Entities.Entity> _entityList = _entityManager.GetEntities();
        switch (AIentity.EntityType)
        {
            case Entities.Entity.entityType.Player:
                break;
            case Entities.Entity.entityType.Zombie:

                //move to player
                if (targetEntity != null)
                {
                    if (targetEntity.CompareTag("Player"))
                    {
                        AIentity.Attack(targetEntity);
                        AIentity.EndTurn();
                        return;
                    }
                    else
                    {
                        AIentity.EndTurn();
                        return;
                    }
                }
                break;

            case Entities.Entity.entityType.Healer:
                //heal friendly if friendly is in range and needs healing
                foreach(Entities.Entity entity in _entityList)
                {
                    //check it is not the player or itself
                    if(entity.EntityType != Entities.Entity.entityType.Player && entity != AIentity)
                    {
                        if (Vector3Int.Distance(AIentity.GetCellPosition(), entity.GetCellPosition()) <= 2)
                        {
                            if (entity.NeedsHealing(AIentity))
                            {
                                AIentity.EndTurn();
                                return;
                            }
                        }
                    }

                }
                if (targetEntity != null)
                {
                    if (targetEntity.CompareTag("Player"))
                    {
                        AIentity.Attack(targetEntity);
                        AIentity.EndTurn();
                        return;
                    }
                    else
                    {
                        AIentity.EndTurn();
                        return;
                    }
                }
                break;
            case Entities.Entity.entityType.Range:
                //move within range of player to attack but keep at a distance 
                //if inline and 1/2/3 squares away attack
                if(Vector3Int.Distance(AIentity.GetCellPosition(),target.GetCellPosition()) == 1 || Vector3Int.Distance(AIentity.GetCellPosition(), target.GetCellPosition()) == 2 || Vector3Int.Distance(AIentity.GetCellPosition(), target.GetCellPosition()) == 3)
                {
                    for(int i=0;i<3;i++)
                    {
                        nextcell = path[i];
                        targetEntity = _entityManager.GetEntity(nextcell);

                        if (targetEntity != null)
                        {
                            if (targetEntity.CompareTag("Player"))
                            {
                                AIentity.Attack(targetEntity);
                                AIentity.EndTurn();
                                return;
                            }
                            else
                            {
                                AIentity.EndTurn();
                                return;
                            }
                        }
                    }
                }

                else
                {
                    if (targetEntity != null)
                    {
                        if (targetEntity.CompareTag("Player"))
                        {
                            AIentity.Attack(targetEntity);
                            AIentity.EndTurn();
                            return;
                        }
                        else
                        {
                            AIentity.EndTurn();
                            return;
                        }
                    }
                }
                break;
            case Entities.Entity.entityType.Knight:
                //move to player unless health is 1 then move to healer if one is alive
                break;
            case Entities.Entity.entityType.Tank:
                if (AIentity.GetHealth() > AIentity.GetMaxHealth()/2)
                {
                    if(target.EntityType != Entities.Entity.entityType.Player)
                    {
                        foreach(Entities.Entity entity in _entityList)
                        {
                            if (entity.EntityType == Entities.Entity.entityType.Player)
                            {
                                target = entity;
                            }

                        }
                    }

                    if (targetEntity != null)
                    {
                        if (targetEntity.CompareTag("Player"))
                        {
                            AIentity.Attack(targetEntity);
                            AIentity.EndTurn();
                            return;
                        }
                        else
                        {
                            AIentity.EndTurn();
                            return;
                        }
                    }
                }
                else
                {
                    foreach(Entities.Entity entity in _entityList)
                    {
                        if(entity.EntityType == Entities.Entity.entityType.Healer)
                        {
                            target = entity;
                        }

                    }
                    path = _path.GetPath(AIentity.GetCellPosition(), target.GetCellPosition());
                    nextcell = path[0];
                    targetEntity = _entityManager.GetEntity(nextcell);

                    if (targetEntity != null)
                    {
                        if (targetEntity.CompareTag("Player"))
                        {
                            AIentity.Attack(targetEntity);
                            AIentity.EndTurn();
                            return;
                        }
                        else
                        {
                            AIentity.EndTurn();
                            return;
                        }
                    }

                }
                break;
            default:
                break;
        }
        Debug.Log("decision called 1");
        AIentity.MoveTo(nextcell);


        
    }
}

    

