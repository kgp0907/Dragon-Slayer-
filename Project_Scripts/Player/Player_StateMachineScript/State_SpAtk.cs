using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_SpAtk : IState<Player>
{
    public void OnEnter(Player player)
    {
        player.StartCoroutine(SpAtk1Coroutine(player));
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

    }

    IEnumerator SpAtk1Coroutine(Player player)
    {
        player.a_id = "SpAtk";
        player.PlayerAnimator.SetTrigger("SpAtk");
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.15f);
        Time.timeScale = 0.4f;
      

        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.35f);
        player.AtkColision.SetActive(true);
        GameObject Smash = ObjectPoolingManager.Instance.GetObject("Smash", player.WeaponPos[3]);
    
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.4f);
        player.AtkColision.SetActive(false);
        Time.timeScale = 1f;
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.9f);
        ObjectPoolingManager.Instance.ReturnObject("Smash", Smash);
        player.ChangeState(Player.eState.MOVE);
    }
}
