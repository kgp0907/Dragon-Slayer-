using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_ChargeAtk : IState<Player>
{
    public void OnEnter(Player player)
    {
        player.p_takedamage.GodMode = true;
        player.StartCoroutine(NormalAtk1Coroutine(player));
    }

    public void OnExit(Player player)
    {
        player.p_takedamage.GodMode = false;
    }

    public void OnFixedUpdate(Player player)
    {
        player.inputmanager.InputMovement();
        player.inputmanager.Rotation();
    }

    public void OnUpdate(Player player)
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            player.ChangeState(Player.eState.DODGE);
        }
    }

    IEnumerator NormalAtk1Coroutine(Player player)
    {
        player.a_id = "ChargeAtk";
        player.PlayerAnimator.SetTrigger("ChargeAtk");
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.13f);
        Time.timeScale = 0.4f;
        player.AtkColision.SetActive(true);
        GameObject Slash = ObjectPoolingManager.Instance.GetObject("Slash", player.WeaponPos[4]);

        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.18f);
        player.AtkColision.SetActive(false);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.3f);
        GameObject Slash2 = ObjectPoolingManager.Instance.GetObject("Slash", player.WeaponPos[0]);
        ObjectPoolingManager.Instance.ReturnObject("Slash", Slash);
        player.DeadAtkColision.SetActive(true);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.35f);
        player.DeadAtkColision.SetActive(false);
        Time.timeScale = 1f;
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.5f);
        ObjectPoolingManager.Instance.ReturnObject("Slash", Slash2);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.9f);
        player.ChangeState(Player.eState.MOVE);
    }
}
