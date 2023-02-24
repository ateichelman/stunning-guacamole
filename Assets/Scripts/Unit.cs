using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isMoving;
    [SerializeField] private Animator unitAnimation;
    [SerializeField] private float turnSpeed = 7.5f;

    public float moveSpeed = 4f;
    public float movementMargin = 0.025f;

    private void Update()
    {
        if (isMoving)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * turnSpeed);

            if (Vector3.Distance(targetPosition, transform.position) < movementMargin)
            {
                unitAnimation.SetBool("isWalking", false);
                isMoving = false;
            }
            
        }



    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        unitAnimation.SetBool("isWalking", true);
        isMoving = true;
    }
}
