using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyState_Land : EState<Dragon>
{
    public void OnEnter(Dragon dragon)
    {   
        dragon.StartCoroutine(LandCoroutine(dragon));
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
   
    IEnumerator LandCoroutine(Dragon dragon)
    {
        dragon.a_id = "Land";
        dragon.dragonAnimator.SetTrigger("Land");

        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.5f);
        GameObject LandEffect = ObjectPoolingManager.Instance.GetObject_Noparent("Ground", dragon.LandPos);
        dragon.isFly = false;
        dragon.navMeshAgent.enabled = true;
        ShakeCamera.instance.OnShakeCamera(0.3f,0.3f);
        yield return new WaitForSeconds(3f);
        ObjectPoolingManager.Instance.ReturnObject("Ground", LandEffect);
    }

}
