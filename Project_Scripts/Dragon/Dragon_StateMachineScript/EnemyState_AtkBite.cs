using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_AtkBite : EState<Dragon>
{
    public GameObject BiteColision;

    public void OnEnter(Dragon dragon)
    {
        dragon.StartCoroutine(BiteAtkCoroutine(dragon));
    }

    public void OnExit(Dragon dragon)
    {
        
    }

    public void OnFixedUpdate(Dragon dragon)
    {
   
    }

    public void OnUpdate(Dragon dragon)
    {
        dragon.enemy_ai.AnimationEndCheck();
    }

    IEnumerator BiteAtkCoroutine(Dragon dragon)
    {
        dragon.a_id = "BiteAtk";
        dragon.dragonAnimator.SetTrigger(dragon.a_id);

        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.4f);

        dragon.BiteAtkColision.SetActive(true);

        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.5f);

        dragon.BiteAtkColision.SetActive(false);
    }
    
}
