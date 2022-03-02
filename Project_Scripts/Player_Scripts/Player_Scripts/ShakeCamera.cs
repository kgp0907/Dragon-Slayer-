using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private float shakeTime;
    private float shakeIntensity;
    private CameraControler cameraController;
    private bool isOnShake;
    public static ShakeCamera instance;
    private void Awake()
    {
      cameraController = GetComponent<CameraControler>();
    
      if (instance == null)
      instance = this;

      else if (instance != this)
       Destroy(this.gameObject);  
    }

    public void OnShakeCamera(float shakeTime = 0.2f, float shakeIntensity = 0.2f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        if (isOnShake == true) 
            return;
        StopCoroutine("ShakeByRotation");
        StartCoroutine("ShakeByRotation");
    }

    private IEnumerator ShakeByRotation()
    {
        isOnShake = true;
        Vector3 startRotation = transform.eulerAngles;

        float power = 10f;

        while (shakeTime > 0.0f)
        {
            float x = 0;
            float y = 0;
            float z = Random.Range(-1f, 1f);
            transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeIntensity * power);

            shakeTime -= Time.deltaTime;

            yield return null;
        }
    
        transform.rotation = Quaternion.Euler(startRotation);
        isOnShake = false;
    }  
}
