using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyState_TakeOff : EState<Dragon>
{
    public void OnEnter(Dragon dragon)
    {
        dragon.StartCoroutine(TakeOffCoroutine(dragon));
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

    IEnumerator TakeOffCoroutine(Dragon dragon) {

        dragon.a_id = "TakeOff";
        dragon.isFly = true; 
        dragon.navMeshAgent.enabled = false;
        dragon.dragonAnimator.SetTrigger(dragon.a_id);
        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.25f);  
        GameObject SmokeEffect = ObjectPoolingManager.Instance.GetObject_Noparent("Smoke", dragon.SmokePos);
        ShakeCamera.instance.OnShakeCamera(0.3f, 0.3f);
        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.9f);
        ObjectPoolingManager.Instance.ReturnObject("Smoke", SmokeEffect);
    }
}
