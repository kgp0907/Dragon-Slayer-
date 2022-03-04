using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackColision : MonoBehaviour
{
    Player player;
    public ShakeCamera shakeCamera;
  
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {      
            ShakeCamera.instance.OnShakeCamera(0.2f, 0.2f);
            other.GetComponent<Enemy_TekeDamage>().TakeDamage(Player.PlayerDamage);
        }
    }
    
}
