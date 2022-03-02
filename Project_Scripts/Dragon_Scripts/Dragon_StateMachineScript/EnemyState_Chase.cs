using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyState_Chase : EState<Dragon>
{
    public void OnEnter(Dragon dragon)
    {
        dragon.StartMove();
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
        float Distance = (dragon.target.position - dragon.transform.position).magnitude;

        dragon.dragonAnimator.SetFloat("MoveSpeed", dragon.navMeshAgent.velocity.magnitude);

        if (Distance <= dragon.AttackRange &&
                        dragon.Attacking == false)
        {
            dragon.ChangeState(Dragon.EnemyState.READY);      
        }
        else
        {
            dragon.navMeshAgent.SetDestination(dragon.target.transform.position);
        }
    }
}
