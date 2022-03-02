using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Ready : EState<Dragon>
{

    private Enemy_Pattern Enemy_pattern;
    Animator enemyani;
    public void OnEnter(Dragon dragon)
    {
        dragon.StopMove();
        dragon.StartCoroutine(AtkReadyCoroutine(dragon));
    }

    public void OnExit(Dragon dragon)
    {

    }

    public void OnFixedUpdate(Dragon dragon)
    {
        
    }

    public void OnUpdate(Dragon dragon)
    {

    }
    public IEnumerator AtkReadyCoroutine(Dragon dragon)
    {
        dragon.Attacking = true;

        while (dragon.ActReadyTime >= 0f)
        {
            dragon.ActReadyTime -= Time.deltaTime;
            Rotation(dragon);
            yield return null;
        }
        dragon.Enemy_pattern.NextState(dragon);
        dragon.ActReadyTime = dragon.actReadyTime;
    }
    public void Rotation(Dragon dragon)
    {
        var targetPos = dragon.target.position;
        targetPos.y = dragon.transform.position.y;
        var targetDir = Quaternion.LookRotation(targetPos - dragon.transform.position);
        dragon.transform.rotation = Quaternion.Slerp(dragon.transform.rotation, targetDir,
                                                     dragon.RotationSpeed * Time.deltaTime);
    }
}
