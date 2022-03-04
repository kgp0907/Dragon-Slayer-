using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDodge : IState<Player>
{
    public void OnEnter(Player player)
    {
        player.p_takedamage.GodMode = true;
        if (player.Lockon)
        {
            player.a_id = "Slide";
            player.PlayerAnimator.SetTrigger("Slide");
        }

        else
        {
            player.a_id = "Dumb";
            player.PlayerAnimator.SetTrigger("Dumb");
        }         
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
       player.inputmanager.AnimationEndCheck();    
    }
    
    
}
