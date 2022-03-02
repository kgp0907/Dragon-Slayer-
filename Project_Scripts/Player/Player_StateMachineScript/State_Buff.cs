using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Buff : IState<Player>
{
    public void OnEnter(Player player)
    {
        player.p_takedamage.GodMode = true;
        player.StartCoroutine(BuffCoroutine(player));
    }

    public void OnExit(Player player)
    {
        player.p_takedamage.GodMode = false;
    }

    public void OnFixedUpdate(Player player)
    {
    }

    public void OnUpdate(Player player)
    {

    }

    IEnumerator BuffCoroutine(Player player)
    {
        player.a_id = "Buff";
        player.PlayerAnimator.SetTrigger("Buff");
        GameObject Buff = ObjectPoolingManager.Instance.GetObject("Buff", player.BuffPos);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.1f);
        player.MagicSword.Play();
        ShakeCamera.instance.OnShakeCamera(0.2f, 0.2f);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.45f);
        ShakeCamera.instance.OnShakeCamera(0.2f, 0.2f);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.9f);
        ObjectPoolingManager.Instance.ReturnObject("Buff", Buff);
        player.ChangeState(Player.eState.MOVE);
    }
}
