using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHit : IState<Player>
{
    public void OnEnter(Player player)
    {
        player.p_takedamage.GodMode = true;
        if (player.SmashHit)
        {
            player.a_id = "Falldown";
            player.PlayerAnimator.SetTrigger("TakeDamage_Knockback");
        }
        else
        {
            player.a_id = "Hit";
            player.PlayerAnimator.SetTrigger("TakeDamage");
        }
    }

    public void OnExit(Player player)
    {
        player.SmashHit = false;
        player.Hit = false;
        player.p_takedamage.GodMode = false;
    }

    public void OnFixedUpdate(Player player)
    {
       
    }

    public void OnUpdate(Player player)
    {
       player.inputmanager.AnimationEndCheck();
    }
}
