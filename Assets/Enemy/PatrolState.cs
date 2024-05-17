using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    bool _isMoving;
    Vector3 _destination;
    public void EnterState(Enemy enemy)
    {
        _isMoving = false;
        enemy.Animator.SetTrigger("PatrolState");
    }
    public void UpdateState(Enemy enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) < enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.ChaseState);
        }
        if (!_isMoving)
        {
            _isMoving = true;
            // Range adalah Exclusive, 0 sd 6. 7 tidak termasuk
            int index = UnityEngine.Random.Range(0, enemy.Waypoint.Count);
            _destination = enemy.Waypoint[index].position;
            enemy.NavMeshAgent.destination = _destination;
        }
        else
        {
            if (Vector3.Distance(_destination, enemy.transform.position) <= 0.1)
            {
                _isMoving = false;
            }
        }
    }
    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop Patrol");
    }
}
