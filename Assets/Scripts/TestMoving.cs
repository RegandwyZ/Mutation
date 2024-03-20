using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoving : MonoBehaviour
{
    private Vector3 _upDown = new Vector3(1, 0, 0);
    private Vector3 _leftRight = new Vector3(0, 0, 1);
    public float speed = 1.0f;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            targetPosition += _upDown;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            targetPosition += _leftRight;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
