using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{

    [SerializeField]
    private Transform target;       //카메라가 추적하는 대상
    [SerializeField]
    private float minDistance = 3;  // 카메라와 타겟의 최소거리
    [SerializeField]
    private float maxDistance = 30; // 카메라와 타겟의 최대거리
    [SerializeField]
    private float wheelSpeed = 3000; // 마우스 휠 스크롤 속도
    [SerializeField]
    private float xMoveSpeed = 500; // 카메라의 y축 회전 속도
    [SerializeField]
    private float yMoveSpeed = 250; // 카메라의 x축 회전 속도
    private float yMinLimit = 5;    // 카메라의 x축 회전 제한 최소 값
    private float yMaxLimit = 80;   // 카메라의 x축 회전 제한 최대 값
    private float x, y;             // 마우스 이동 방향 값
    private float distance;         // 카메라와 target의 거리

    public Transform enemy;
    public Transform player;
    public Animator playani;
    public float cameraSlack;
    public float cameraDistance;
    public static bool Lockon;
    private Vector3 pivotPoint;
    Player playerScript;
    public bool IsOnShake { set; get; }
    private void Awake()
    {
        playerScript = FindObjectOfType<Player>();
        distance = Vector3.Distance(target.transform.position, transform.position);

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    private void Update()
    {
        if (playerScript.Lockon)
            LockCamera();
        else
            UnLockCamera();
    }
    private void LateUpdate()
    {
        if (target == null) return;
        if (IsOnShake == true) return;
        transform.position = transform.rotation * new Vector3(0, 0, -distance) + target.position;
    }
    public void LockCamera()
    {
        Vector3 current = pivotPoint;
        Vector3 target = player.transform.position + Vector3.up;
        pivotPoint = Vector3.MoveTowards(current, target, Vector3.Distance(current, target) * cameraSlack);

        transform.position = pivotPoint;
        transform.LookAt((enemy.position + player.position) / 2);

        transform.position -= transform.forward * cameraDistance;
    }
    public void UnLockCamera()
    {
        x += Input.GetAxis("Mouse X") * xMoveSpeed * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * yMoveSpeed * Time.deltaTime;
        y = ClampAngle(y, yMinLimit, yMaxLimit);
        transform.rotation = Quaternion.Euler(y, x, 0);

        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < 360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
