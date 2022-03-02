using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_FlyFast : EState<Dragon>
{
    public void OnEnter(Dragon dragon)
    {
        dragon.StartCoroutine(MeteorCoroutine(dragon));
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
    IEnumerator MeteorCoroutine(Dragon dragon)
    {
        dragon.a_id = "FlyFast";
        dragon.dragonAnimator.SetTrigger("FlyFast");
        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.1f);

        dragon.BreatheColision.SetActive(true);
        GameObject Breathe = ObjectPoolingManager.Instance.GetObject("Breathe", dragon.BreathePos);
        GameObject Flame = ObjectPoolingManager.Instance.GetObject("Flame", dragon.FlamePos);
        GameObject Meteor = ObjectPoolingManager.Instance.GetObject("MeteorShower", dragon.MeteorPos);

        yield return new WaitUntil(() => dragon.AnimationName && dragon.AnimationProgress >= 0.9f);

        ObjectPoolingManager.Instance.ReturnObject("Breathe", Breathe);
        ObjectPoolingManager.Instance.ReturnObject("Flame", Flame);
        dragon.BreatheColision.SetActive(false);

        yield return new WaitForSeconds(dragon.SkillReturnTime);
        ObjectPoolingManager.Instance.ReturnObject("MeteorShower", Meteor);
    }
}
