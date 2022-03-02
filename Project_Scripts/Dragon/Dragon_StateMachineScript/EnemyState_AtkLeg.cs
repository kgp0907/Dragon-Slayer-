using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_AtkLeg : EState<Dragon>
{
    public void OnEnter(Dragon dragon)
    {  
        dragon.StartCoroutine(LegAtkCoroutine(dragon));
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
    IEnumerator LegAtkCoroutine(Dragon dragon)
    {
        dragon.a_id = "LegAtk";
        dragon.dragonAnimator.SetTrigger("LegAtk");

        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.2f);

        dragon.BreatheColision.SetActive(true);
        GameObject Breathe = ObjectPoolingManager.Instance.GetObject("Breathe", dragon.BreathePos);
        GameObject Flame = ObjectPoolingManager.Instance.GetObject("Flame", dragon.FlamePos);

        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.65f || dragon.dead);

        ShakeCamera.instance.OnShakeCamera(0.2f, 0.2f);
        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.7f || dragon.dead);
        ShakeCamera.instance.OnShakeCamera(0.2f, 0.2f);

        ObjectPoolingManager.Instance.ReturnObject("Breathe", Breathe);
        ObjectPoolingManager.Instance.ReturnObject("Flame", Flame);
        dragon.BreatheColision.SetActive(false);
    

    }
}
