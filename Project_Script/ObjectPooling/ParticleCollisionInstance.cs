using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleCollisionInstance : MonoBehaviour
{
    Dragon dragon;
    public string SpawnTagName;
    public GameObject[] EffectsOnCollision;
    public bool UseWorldSpacePosition;
    public float Offset = 0;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public bool useOnlyRotationOffset = true;
    public bool UseFirePointRotation;
    public float Vibration;
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    void Start()
    {
        dragon = FindObjectOfType<Dragon>();
        part = GetComponent<ParticleSystem>();
    }
    void OnParticleCollision(GameObject other)
    {
        int numParticleEvent = part.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < numParticleEvent; i++) //이벤트 만큼
        {
            foreach (var effect in EffectsOnCollision)
            {
                GameObject obj = ObjectPoolingManager.Instance.GetObject(SpawnTagName, gameObject, 
                                                      collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion());
                StartCoroutine(ReturnCoroutine(obj));
                ShakeCamera.instance.OnShakeCamera(Vibration, Vibration);
                if (UseFirePointRotation)
                {
                    obj.transform.LookAt(transform.position);
                }
                else if (rotationOffset != Vector3.zero && useOnlyRotationOffset)
                {
                    obj.transform.rotation = Quaternion.Euler(rotationOffset);
                }
                else
                {
                    obj.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal);
                    obj.transform.rotation *= Quaternion.Euler(rotationOffset);
                }
                
            }
        }

    }
    IEnumerator ReturnCoroutine(GameObject obj)
    {
        yield return new WaitForSeconds(dragon.ReturnTime);
        ObjectPoolingManager.Instance.ReturnObject(SpawnTagName, obj);
    }
}
