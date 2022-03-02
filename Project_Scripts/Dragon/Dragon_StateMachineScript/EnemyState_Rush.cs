using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Rush : EState<Dragon>
{
    bool rush;
    public void OnEnter(Dragon dragon)
    {
        dragon.StartCoroutine(RushAtkColisionActive(dragon));
    }

    public void OnExit(Dragon dragon)
    {
        rush = false;
    }

    public void OnFixedUpdate(Dragon dragon)
    {
        //dragon.enemy_ai.AnimationEndCheck();
    }

    public void OnUpdate(Dragon dragon)
    {
        if(rush)
        dragon.transform.position += dragon.transform.forward * dragon.RushSpeed * Time.deltaTime;
    }

    IEnumerator RushAtkColisionActive(Dragon dragon)
    {
        rush = true;
        dragon.a_id = "Rush";
        dragon.dragonAnimator.SetTrigger("RushAtk");

        ShakeCamera.instance.OnShakeCamera(0.2f, 0.2f);
        yield return new WaitForSeconds(1f);
        ShakeCamera.instance.OnShakeCamera(0.2f, 0.2f);
        dragon.Attacking = false;
        dragon.ChangeState(Dragon.EnemyState.CHASE);
    }
}
