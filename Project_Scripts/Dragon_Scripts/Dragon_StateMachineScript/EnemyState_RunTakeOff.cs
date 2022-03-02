using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_RunTakeOff : EState<Dragon>
{
    public void OnEnter(Dragon dragon)
    {
        dragon.dragonAnimator.SetTrigger("RunTakeoff");
        dragon.StartCoroutine(RunTakeoffCoroutine(dragon));
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

    IEnumerator RunTakeoffCoroutine(Dragon dragon)
    {
        dragon.a_id = "RunTakeoff";
        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.1f);
        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.5f);
        GameObject SmokeEffect = ObjectPoolingManager.Instance.GetObject_Noparent("Smoke", dragon.SmokePos);
        dragon.isFly = true;
        ShakeCamera.instance.OnShakeCamera(0.3f, 0.3f);
        dragon.navMeshAgent.enabled = false;
        dragon.StartCoroutine(FlyCoroutine(dragon));
        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.9f);
        ObjectPoolingManager.Instance.ReturnObject("Smoke", SmokeEffect);

    }
    //
    IEnumerator FlyCoroutine(Dragon dragon)
    {
       
        while (dragon.transform.position.y < dragon.FlyHeight)
        {
            Vector3 DragonPosition = dragon.gameObject.transform.position;
            DragonPosition.y += (dragon.FlyHeight*0.5f)*Time.deltaTime;
            dragon.gameObject.transform.position= DragonPosition;
            yield return null;
        }
    }
}
