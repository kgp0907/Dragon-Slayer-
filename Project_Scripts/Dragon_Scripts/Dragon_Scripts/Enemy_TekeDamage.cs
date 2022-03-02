using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TekeDamage : MonoBehaviour
{
    Dragon dragon;
    public float eHp_Current=200;
    public float EffectReturnTime=2f;
    public void Awake()
    {
        dragon = Dragon.FindObjectOfType<Dragon>();
    }
    public void TakeDamage(float damage)
    {
       eHp_Current -= damage;
       StartCoroutine(AttackEffect());
    }
    private void Update()
    {
        if (eHp_Current <= 0f && !dragon.dead)
        {
            dragon.ChangeState(Dragon.EnemyState.DEAD);
        }
        else if(eHp_Current <= 50f && !dragon.Stunned)
        {
            dragon.Stunned = true;
            dragon.ChangeState(Dragon.EnemyState.STUN);
        }
    }

    public IEnumerator AttackEffect()
    {
        GameObject explosion = ObjectPoolingManager.Instance.GetObject_Noparent("Explosion", dragon.HitEffect);
        yield return new WaitForSeconds(EffectReturnTime);
        ObjectPoolingManager.Instance.ReturnObject("Explosion", explosion);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadAtk"))
        {          
            eHp_Current = 10;
        }
    }

}

