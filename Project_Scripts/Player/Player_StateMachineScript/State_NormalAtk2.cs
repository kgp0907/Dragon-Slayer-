using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_NormalAtk2 : IState<Player>
{
    public void OnEnter(Player player)
    {
        player.StartCoroutine(NormalAtk2Coroutine(player));
    }

    public void OnExit(Player player)
    {

    }

    public void OnFixedUpdate(Player player)
    {
        player.inputmanager.InputMovement();
        player.inputmanager.Rotation();
    }

    public void OnUpdate(Player player)
    {
        player.inputmanager.ComboAtkCheck(Player.eState.NORMALATK3);
    }

    IEnumerator NormalAtk2Coroutine(Player player)
    {
        player.a_id = "NormalAtk2";
        player.PlayerAnimator.SetTrigger("NormalAtk2");
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.3f);
        GameObject Slash = ObjectPoolingManager.Instance.GetObject("Slash", player.WeaponPos[1]);
        player.AtkColision.SetActive(true);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.33f);
        player.AtkColision.SetActive(false);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.7f);
        ObjectPoolingManager.Instance.ReturnObject("Slash", Slash);
    }
}
