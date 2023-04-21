using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    
    private float _spinProgress = 0f;
    
    private void Update()
    {
        if (_isActive)
        {
            float spinAddAmount = 360f * Time.deltaTime;

            _spinProgress += spinAddAmount;

            if (_spinProgress >= 360f)
            {
                spinAddAmount = 0;
                _spinProgress = 0f;
                _isActive = false;
                _onActionComplete();
            }
            
            transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        }
    }

    public override void TakeAction(GridPosition gridPosition, Action onSpinComplete)
    {
        _onActionComplete = onSpinComplete;
        _isActive = true;
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = _unit.GetGridPosition();

        return new List<GridPosition>
        {
            unitGridPosition
        };
    }

    public override int GetActionPointsCost()
    {
        return 2;
    }
}
