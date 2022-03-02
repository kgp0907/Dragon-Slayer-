using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_FlyRunLand : EState<Dragon>
{
    public void OnEnter(Dragon dragon)
    {
        dragon.StartCoroutine(RunLandCoroutine(dragon));

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
        //dragon.transform.position +=
        //    dragon.transform.forward * dragon.FlyMoveSpeed * Time.deltaTime;
    }
    IEnumerator RunLandCoroutine(Dragon dragon)
    {
        dragon.navMeshAgent.enabled = false;
        dragon.a_id = "RunLand";
        dragon.dragonAnimator.SetTrigger("RunLand");
       // yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.1f);
        
        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.4f);
       // dragon.LandColision.SetActive(true);
        dragon.isFly = false;
        dragon.navMeshAgent.enabled = true;
        ShakeCamera.instance.OnShakeCamera(0.4f, 0.4f);
        GameObject LandEffect = ObjectPoolingManager.Instance.GetObject("Ground", dragon.LandPos);
        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.45f);
      //  dragon.LandColision.SetActive(false);
        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.55f);
        dragon.StartCoroutine(LightningCoroutine(dragon));
        yield return new WaitForSeconds(1f);
        ObjectPoolingManager.Instance.ReturnObject("Ground", LandEffect);
    }
    IEnumerator LightningCoroutine(Dragon dragon)
    {
        GameObject Lightning = ObjectPoolingManager.Instance.GetObject_Noparent("Lightning", dragon.MeteorPos);
        yield return new WaitForSeconds(10);
        ObjectPoolingManager.Instance.ReturnObject("Lightning", Lightning);
    }
}
