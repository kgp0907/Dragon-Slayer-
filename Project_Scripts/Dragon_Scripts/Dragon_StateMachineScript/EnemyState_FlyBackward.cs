using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyState_FlyBackward : EState<Dragon>
{
    public void OnEnter(Dragon dragon)
    {
        dragon.StartCoroutine(FlyBackwardCoroutine(dragon));
 
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
        dragon.transform.position -= dragon.transform.forward * dragon.FlyMoveSpeed * Time.deltaTime;
    }
    IEnumerator FlyBackwardCoroutine(Dragon dragon)
    {
        dragon.isFly = true;
        dragon.navMeshAgent.enabled = false;
        dragon.a_id = "FlyBackward";
        dragon.dragonAnimator.SetTrigger("FlyBackward");

        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.25f);
        GameObject SmokeEffect = ObjectPoolingManager.Instance.GetObject_Noparent("Smoke", dragon.SmokePos);
        dragon.BreatheColision.SetActive(true);
        ShakeCamera.instance.OnShakeCamera(0.3f, 0.3f);
        GameObject Breathe = ObjectPoolingManager.Instance.GetObject("Breathe", dragon.BreathePos);
        GameObject Flame = ObjectPoolingManager.Instance.GetObject("Flame", dragon.FlamePos);

        dragon.StartCoroutine(FlyCoroutine(dragon));

        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.9f || dragon.dead);
        ObjectPoolingManager.Instance.ReturnObject("Smoke", SmokeEffect);
        ObjectPoolingManager.Instance.ReturnObject("Breathe", Breathe);
        ObjectPoolingManager.Instance.ReturnObject("Flame", Flame);
        dragon.BreatheColision.SetActive(false);
    }
    IEnumerator FlyCoroutine(Dragon dragon)
    {
        while (dragon.transform.position.y <= dragon.FlyHeight)
        {
            Vector3 DragonPosition = dragon.gameObject.transform.position;
            DragonPosition.y += (dragon.FlyHeight*0.5f) * Time.deltaTime;
            dragon.gameObject.transform.position = DragonPosition;
            yield return null;
        }
    }
  
}
