using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{

    [SerializeField] private TextMeshPro text;
    private GridObject _gridObject;

    public void SetGridObject(GridObject gridObject)
    {
        _gridObject = gridObject;
        //TMPro.TextMeshPro text = GetComponent<TMPro.TextMeshPro>();
        //text.SetText($"x: {_gridObject.GridPosition.x}; z: {_gridObject.GridPosition.z}");
        
    }

    private void Update()
    {
        text.text = _gridObject.ToString();
    }
}
