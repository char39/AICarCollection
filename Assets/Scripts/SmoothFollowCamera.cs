using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform car;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float distance = 10.0f;
    [SerializeField]
    private float height = 5.0f;

    [SerializeField]
    private float rotationDamping;
    [SerializeField]
    private float heightDamping;

    void Start()
    {
        target = car;
    }

    void LateUpdate()
    {
        if (!target)
            return;

        var wantedRotationAngle = target.eulerAngles.y;
        var wantedHeight = target.position.y + height;

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        transform.LookAt(target);
    }
}
