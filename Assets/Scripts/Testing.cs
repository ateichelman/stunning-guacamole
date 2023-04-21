using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private GridSystem _gridSystem;

    [SerializeField] private Transform gridDebugObjectPrefab;

    [SerializeField] private Unit _unit;
    
    // Start is called before the first frame update
    void Start()
    {
        _gridSystem = new GridSystem(10, 10, 2f);
        
        _gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
        
    }

    private void Update()
    {
        GridSystemVisual.Instance.HideAllGridPositions();
        List<GridPosition> validPositions = _unit.GetMoveAction().GetValidActionGridPositionList();
        GridSystemVisual.Instance.ShowGridPositions(validPositions);
    }
}
