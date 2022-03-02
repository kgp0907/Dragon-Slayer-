using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Stun : EState<Dragon>
{

    private Enemy_Pattern Enemy_pattern;
    Animator enemyani;
    public void OnEnter(Dragon dragon)
    {
        StunCoroutine(dragon);
    }

    public void OnExit(Dragon dragon)
    {

    }

    public void OnFixedUpdate(Dragon dragon)
    {
        dragon.enemy_ai.AnimationEndCheck();
    }

    public void OnUpdate(Dragon dragon)
    {


    }

    public void StunCoroutine(Dragon dragon)
    {
       
        dragon.a_id = "Stun";
        enemyani = dragon.GetComponent<Animator>();
        Enemy_pattern = dragon.transform.GetComponent<Enemy_Pattern>();
        dragon.StopAllCoroutines();
        enemyani.SetTrigger("Stunned");       
    }
}
