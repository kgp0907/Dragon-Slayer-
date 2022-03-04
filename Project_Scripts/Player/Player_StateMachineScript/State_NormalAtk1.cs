using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_NormalAtk1 : IState<Player>
{
    public void OnEnter(Player player)
    {      
        player.StartCoroutine(NormalAtk1Coroutine(player));
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
        player.inputmanager.ComboAtkCheck(Player.eState.NORMALATK2);
    }

    IEnumerator NormalAtk1Coroutine(Player player)
    {
        player.a_id = "NormalAtk1";
        player.PlayerAnimator.SetTrigger("NormalAtk1");
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.22f);
        player.AtkColision.SetActive(true);
        GameObject Slash = ObjectPoolingManager.Instance.GetObject("Slash", player.WeaponPos[0]);
       
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.25f);
        player.AtkColision.SetActive(false);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.5f);
        ObjectPoolingManager.Instance.ReturnObject("Slash", Slash);
    }
}
