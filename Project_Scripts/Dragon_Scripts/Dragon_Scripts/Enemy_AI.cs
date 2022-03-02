using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    Dragon dragon;
    Enemy_Pattern enemy_Pattern;
    private void Start()
    {
        enemy_Pattern = this.transform.GetComponent<Enemy_Pattern>();
        dragon = this.transform.GetComponent<Dragon>();
        dragon.ChangeState(Dragon.EnemyState.PATROL);
    }

    private void Update()
    {
        if (dragon.IsState(Dragon.EnemyState.PATROL))
        {
            float distance = (dragon.target.position - dragon.transform.position).sqrMagnitude;
            if (distance <= dragon.SightRange * dragon.SightRange)
            {
                dragon.ChangeState(Dragon.EnemyState.CHASE);
            }           
        }
        else if (dragon.IsState(Dragon.EnemyState.CHASE))
        {
            float distance = (dragon.target.position - dragon.transform.position).sqrMagnitude;
            if (distance >= dragon.SightRange * dragon.SightRange)
            {
                dragon.ChangeState(Dragon.EnemyState.PATROL);
            }
        }
    }
    public void AnimationEndCheck()
    {
        if (dragon.AnimationName && dragon.AnimationProgress >= 0.9f)
        {
            if (dragon.isFly == true)
            {
                dragon.ChangeState(Dragon.EnemyState.READY);
            }
            else
            {
                dragon.ChangeState(Dragon.EnemyState.CHASE);
            }

            dragon.Attacking = false;
        }
    }
}
