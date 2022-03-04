using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Patrol : EState<Dragon>
{
    private int patrolIndex = 0;
    public void OnEnter(Dragon dragon)
    {
        patrolIndex = 0;
        dragon.navMeshAgent.autoBraking = false;
        dragon.StartMove();
        GotoNextPoint(dragon);
    }
    public void OnExit(Dragon dragon)
    {
        dragon.StopMove();
    }
    public void OnFixedUpdate(Dragon dragon)
    {

    }
    public void OnUpdate(Dragon dragon)
    {
        if (dragon.ArrivePoint==false &&
            dragon.RemainDistance <= dragon.AttackRange)
        {
            GotoNextPoint(dragon);
        }        
    }
    public void GotoNextPoint(Dragon dragon)
    {    
        if (dragon.WayPoints.Length == 0)
            return;
   
        dragon.navMeshAgent.destination = dragon.WayPoints[patrolIndex].position;

        patrolIndex = (patrolIndex + 1) % dragon.WayPoints.Length;
    }
}
