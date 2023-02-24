using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private GridSystem _gridSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        _gridSystem = new GridSystem(10, 10, 2f);
    }

    private void Update()
    {
        Debug.Log(_gridSystem.GetGridPosition(MouseWorld.GetPosition()));
    }
}
