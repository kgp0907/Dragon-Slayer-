using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDead : IState<Player>
{
    public void OnEnter(Player player)
    {
        //player.p_takedamage.CopyTransformRagdoll(player.Character.transform, player.RagDoll.transform);
        player.RagDoll.SetActive(true);
        player.RagDoll.transform.position = player.Character.transform.position;
        player.Character.SetActive(false);
     
        player.RagdollPoint.AddForce(new Vector3(0f, 0f, 30), ForceMode.Impulse);
        player.PlayerAnimator.SetTrigger("Dead");
        player.GetComponent<CharacterController>().enabled = false;
    }

    public void OnExit(Player player)
    {
       
    }

    public void OnFixedUpdate(Player player)
    {

    }

    public void OnUpdate(Player player)
    {

    }
}
