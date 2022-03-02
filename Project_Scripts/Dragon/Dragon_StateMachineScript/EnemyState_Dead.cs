using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Dead : EState<Dragon>
{

    public void OnEnter(Dragon dragon)
    {
        dragon.StartCoroutine(DeadCoroutine(dragon));
    }

    public void OnExit(Dragon dragon)
    {
        
    }

    public void OnFixedUpdate(Dragon dragon)
    {

    }

    public void OnUpdate(Dragon dragon)
    {

    }
    IEnumerator DeadCoroutine(Dragon dragon)
    {
        dragon.a_id = "Dead";
        dragon.dragonAnimator.SetTrigger("Dead");
        dragon.dead = true;
        dragon.enableact = false;
        dragon.BiteAtkColision.SetActive(false);
        dragon.BreatheColision.SetActive(false);
     
        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.7f);
        ShakeCamera.instance.OnShakeCamera(0.5f, 0.5f);
    }
}
