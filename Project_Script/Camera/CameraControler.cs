using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{

    [SerializeField]
    private Transform target;       //ī�޶� �����ϴ� ���
    [SerializeField]
    private float minDistance = 3;  // ī�޶�� Ÿ���� �ּҰŸ�
    [SerializeField]
    private float maxDistance = 30; // ī�޶�� Ÿ���� �ִ�Ÿ�
    [SerializeField]
    private float wheelSpeed = 3000; // ���콺 �� ��ũ�� �ӵ�
    [SerializeField]
    private float xMoveSpeed = 500; // ī�޶��� y�� ȸ�� �ӵ�
    [SerializeField]
    private float yMoveSpeed = 250; // ī�޶��� x�� ȸ�� �ӵ�
    private float yMinLimit = 5;    // ī�޶��� x�� ȸ�� ���� �ּ� ��
    private float yMaxLimit = 80;   // ī�޶��� x�� ȸ�� ���� �ִ� ��
    private float x, y;             // ���콺 �̵� ���� ��
    private float distance;         // ī�޶�� target�� �Ÿ�

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
