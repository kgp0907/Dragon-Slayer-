using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_FlyAround : EState<Dragon>
{
    float lightmin = 200;
    float lightminus = 4900;

    public void OnEnter(Dragon dragon)
    {
        dragon.aroundPoint = dragon.target.transform.position; // 위치로 이동
        dragon.aroundPoint.y = dragon.transform.position.y;
        dragon.StartCoroutine(AroundCoroutine(dragon));
        dragon.StartCoroutine(LightCoroutine(dragon));     
    }

    public void OnExit(Dragon dragon)
    {
        
    }

    public void OnFixedUpdate(Dragon dragon)
    {

    }

    public void OnUpdate(Dragon dragon)
    {
        Quaternion rot = Quaternion.LookRotation(dragon.aroundPoint - dragon.transform.position);
        dragon.transform.rotation = Quaternion.Slerp(dragon.transform.rotation, rot, dragon.AroundSpeed * Time.deltaTime);
        dragon.transform.position += dragon.transform.forward * dragon.FlyMoveSpeed * Time.deltaTime;
    }


    IEnumerator AroundCoroutine(Dragon dragon)
    {
        dragon.dragonAnimator.SetTrigger("FlyFast");
        dragon.StartCoroutine(MeteorCouroutine(dragon));
        dragon.StartCoroutine(LightningCoroutine(dragon));
        yield return new WaitForSeconds(1.5f);
        dragon.StartCoroutine(MeteorCouroutine(dragon));
        dragon.StartCoroutine(LightningCoroutine(dragon));
        yield return new WaitForSeconds(1.5f);
        dragon.dragonAnimator.SetTrigger("FlyFast");
        dragon.StartCoroutine(MeteorCouroutine(dragon));
        dragon.StartCoroutine(LightningCoroutine(dragon));
        yield return new WaitForSeconds(1.5f);
        dragon.StartCoroutine(MeteorCouroutine(dragon));
        dragon.StartCoroutine(LightningCoroutine(dragon));
        dragon.ChangeState(Dragon.EnemyState.READY);
    }
    IEnumerator MeteorCouroutine(Dragon dragon)
    {
        GameObject Meteor = ObjectPoolingManager.Instance.GetObject("MeteorShower", dragon.MeteorPos);
        yield return new WaitForSeconds(dragon.SkillReturnTime);
        ObjectPoolingManager.Instance.ReturnObject("MeteorShower", Meteor);
    }
    IEnumerator LightningCoroutine(Dragon dragon)
    {
        GameObject Lightning = ObjectPoolingManager.Instance.GetObject("Lightning", dragon.MeteorPos);
        yield return new WaitForSeconds(dragon.SkillReturnTime);
        ObjectPoolingManager.Instance.ReturnObject("Lightning", Lightning);
    }
    IEnumerator LightCoroutine(Dragon dragon)
    {
        while(dragon.LightForce.intensity > lightmin)
        {
            dragon.LightForce.intensity -= lightminus * Time.deltaTime;
            yield return null;
        }
    }
}
