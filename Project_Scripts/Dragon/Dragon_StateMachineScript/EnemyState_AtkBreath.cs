using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_AtkBreath : EState<Dragon>
{

    public void OnEnter(Dragon dragon)
    {    
        dragon.StartCoroutine(BreathAtkCoroutine(dragon));
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

    IEnumerator BreathAtkCoroutine(Dragon dragon)
    {
        dragon.a_id = "BreatheAtk";
        dragon.dragonAnimator.SetTrigger("BreatheAtk");
        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.15f);
        GameObject Breathe = ObjectPoolingManager.Instance.GetObject("Breathe",dragon.BreathePos);
        GameObject Flame = ObjectPoolingManager.Instance.GetObject("Flame", dragon.FlamePos);

        dragon.BreatheColision.SetActive(true);
        ShakeCamera.instance.OnShakeCamera(0.1f, 0.1f);
        yield return new WaitUntil(() => dragon.AnimationName &&dragon.AnimationProgress >= 0.8f||dragon.dead);
   
        ObjectPoolingManager.Instance.ReturnObject("Breathe", Breathe);
        ObjectPoolingManager.Instance.ReturnObject("Flame", Flame);

        dragon.BreatheColision.SetActive(false);
        ShakeCamera.instance.OnShakeCamera(0.1f, 0.1f);
    }
}
