using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyState_FlyBreatheAtk : EState<Dragon>
{
    public void OnEnter(Dragon dragon)
    {
        dragon.StartCoroutine(FlyBreathCoroutine(dragon));
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
        if(dragon.AnimationName && dragon.AnimationProgress >= 0.3f)
        {
            dragon.transform.position += dragon.transform.forward * dragon.FlyMoveSpeed * Time.deltaTime * 3;
        }
   
    }
    IEnumerator FlyBreathCoroutine(Dragon dragon)
    {
        dragon.a_id = "FlyBreatheAtk";
        yield return new WaitForSeconds(0.2f);
        dragon.dragonAnimator.SetTrigger("FlyBreatheAtk");

        yield return new WaitUntil(() => dragon.AnimationName &&
                               dragon.AnimationProgress >= 0.4f);
        ShakeCamera.instance.OnShakeCamera(0.2f, 0.2f);

        dragon.BreatheColision.SetActive(true);
        GameObject Breathe = ObjectPoolingManager.Instance.GetObject("Breathe", dragon.BreathePos);
        GameObject Flame = ObjectPoolingManager.Instance.GetObject("Flame", dragon.FlamePos);


        yield return new WaitUntil(() => dragon.AnimationName &&
                           dragon.AnimationProgress >= 0.9f || dragon.dead);
        ShakeCamera.instance.OnShakeCamera(0.2f, 0.2f);
        ObjectPoolingManager.Instance.ReturnObject("Breathe", Breathe);
        ObjectPoolingManager.Instance.ReturnObject("Flame", Flame);
        dragon.BreatheColision.SetActive(false);
    }
}
