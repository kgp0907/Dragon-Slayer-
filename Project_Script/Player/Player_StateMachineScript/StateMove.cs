using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : IState<Player>
{
    public void OnEnter(Player player)
    {
    
    }

    public void OnExit(Player player)
    {
       
    }

    public void OnFixedUpdate(Player player)
    {
        player.inputmanager.InputMovement();
        player.characterController.Move(player.inputmanager.velocity * Time.deltaTime);
        player.inputmanager.Rotation();
    }

    public void OnUpdate(Player player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.ChangeState(Player.eState.NORMALATK1);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            player.ChangeState(Player.eState.DODGE);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.ChangeState(Player.eState.CHARGEATK);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            player.ChangeState(Player.eState.SPATK);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            player.ChangeState(Player.eState.BUFF);
        }
   
        if (Input.GetKeyDown(KeyCode.O))
        {
            player.ChangeState(Player.eState.DEAD);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (player.Lockon)
            {
                player.Lockon = false;
                player.PlayerAnimator.SetBool("LockOnMode", false);
            }
            else
            {
                player.Lockon = true;
                player.PlayerAnimator.SetBool("LockOnMode", true);
            }
        }
    }

}

