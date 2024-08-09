using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 CentOfMass = new Vector3(0, -0.9f, 0);

    public Transform path;                  // 경로
    public Transform[] pathTransforms;      // 경로 트랜스폼
    public List<Transform> pathList;        // 경로 리스트

    public WheelCollider wheelFrontLeft;
    public WheelCollider wheelFrontRight;
    public WheelCollider wheelRearLeft;
    public WheelCollider wheelRearRight;
    public Transform wheelFrontLeftTr;
    public Transform wheelFrontRightTr;
    public Transform wheelRearLeftTr;
    public Transform wheelRearRightTr;

    public float curSpeed = 0;              // 현재 속도
    private float maxSpeed = 100f;          // 최대 속도
    private int curNode = 0;                // 현재 노드
    private float maxTorque = 500f;                      // 최대 토크
    private float maxSteerAngle = 35f;                   // 최대 조향각
    private float maxBrake = 150000f;                    // 최대 브레이크

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = CentOfMass;
        path = GameObject.Find("PathTransform").transform;
        pathTransforms = path.GetComponentsInChildren<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)        // 경로 트랜스폼이 경로 자신이 아니라면
            {
                pathList.Add(pathTransforms[i]);            // 경로 리스트에 추가
            }
        }
    }

    void FixedUpdate()
    {
        ApplySteer();
        Drive();
        CheckWayPointDistance();
    }

    void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(pathList[curNode].position);
        float newSteer = relativeVector.x / relativeVector.magnitude * maxSteerAngle;
        wheelFrontLeft.steerAngle = newSteer;
        wheelFrontRight.steerAngle = newSteer;
    }

    void Drive()
    {
        curSpeed = 2 * Mathf.PI * wheelFrontLeft.radius * wheelFrontLeft.rpm * 60 / 1000;
        
        if (curSpeed < maxSpeed)
        {
            wheelRearLeft.motorTorque = maxTorque;
            wheelRearRight.motorTorque = maxTorque;
        }
        else
        {
            wheelRearLeft.motorTorque = 0;
            wheelRearRight.motorTorque = 0;
        }
    }

    void CheckWayPointDistance()
    {
        if (Vector3.Distance(transform.position, pathList[curNode].position) <= 20f)
        {
            if (curNode == pathList.Count - 1)
                curNode = 0;
            else
                curNode++;
        }
    }





}
