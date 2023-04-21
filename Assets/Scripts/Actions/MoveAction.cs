using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] private Animator unitAnimation;
    [SerializeField] private float turnSpeed = 7.5f;
    [SerializeField] private int maxMoveDistance = 4;
    
    private Vector3 _targetPosition;
    
    public float moveSpeed = 4f;
    public float stoppingDistance = 0.1f;

    protected override void Awake()
    {
        base.Awake();
        _targetPosition = transform.position;
        
    }
    
    private void Update()
    {
        if (_isActive)
        {
            float stoppingDistance = 0.1f;
            if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (_targetPosition - transform.position).normalized;
                transform.position += moveDirection * moveSpeed * Time.deltaTime;

                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * turnSpeed);

                unitAnimation.SetBool("isWalking", true);
            }
            else
            {
                unitAnimation.SetBool("isWalking", false);
                _isActive = false;
                _onActionComplete();

            }
        }
    }

    public override void TakeAction(GridPosition gridPosition, Action onMoveComplete)
    {
        _onActionComplete = onMoveComplete;
        this._targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        _isActive = true;
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = _unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (unitGridPosition == testGridPosition)
                {
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }
                
                validGridPositionList.Add(testGridPosition);

            }
        }

        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }
}
