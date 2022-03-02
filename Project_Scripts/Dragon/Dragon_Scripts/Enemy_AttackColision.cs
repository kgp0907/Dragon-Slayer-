using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_AttackColision : MonoBehaviour
{
    public static float EnemyDamage = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.GetComponent<Player_TakeDamage>().PlayerTakeDamage(EnemyDamage);
    }
}
