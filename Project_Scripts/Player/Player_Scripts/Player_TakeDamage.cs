using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TakeDamage : MonoBehaviour
{
    Player player;
    public Animator playani;
    public float PlayerHp;
    public bool GodMode = false;
    public float BurnDuration = 2f;

    private void Awake()
    {
        player = GetComponent<Player>();    
    }

    public void PlayerTakeDamage(float damage)
    {
        if (GodMode)
            return;
        PlayerHp -= damage;
     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerHp <= 0f)
        {
            player.ChangeState(Player.eState.DEAD);
        }

        else if (other.gameObject.CompareTag("BreatheAtk") && !GodMode)
        {
            StartCoroutine(BurnHitCoroutine());
            player.Hit = true;     
            player.ChangeState(Player.eState.HIT);
        }

        else if (other.gameObject.CompareTag("MeteorAtk") && !GodMode)
        {
            StartCoroutine(BurnHitCoroutine());
            player.SmashHit = true;
            player.ChangeState(Player.eState.HIT);
        }
        else if (other.gameObject.CompareTag("RushAtk") && !GodMode)
        {
            player.SmashHit = true;
            player.ChangeState(Player.eState.HIT);
        }
    }
   IEnumerator BurnHitCoroutine()
    {
        player.BurnHit.Play();
        yield return new WaitForSeconds(BurnDuration);
        player.BurnHit.Stop();
    }

    public void CopyTransformRagdoll(Transform origin,Transform ragdoll)
    {
        for(int i=0; i < origin.childCount; i++)
        {
            if (origin.childCount != 0)
            {
                CopyTransformRagdoll(origin.GetChild(i), ragdoll.GetChild(i));
            }
            ragdoll.GetChild(i).localPosition = origin.GetChild(i).localPosition;
            ragdoll.GetChild(i).localRotation = origin.GetChild(i).localRotation;
        }
    }
}
