using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem _gridSystem;
    private GridPosition _gridPosition;

    private List<Unit> _unitList;

    public void AddUnit(Unit unit)
    {
        _unitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        _unitList.Remove(unit);
    }

    public List<Unit> GetUnitList()
    {
        return _unitList;
    }

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this._gridPosition = gridPosition;
        this._gridSystem = gridSystem;
        _unitList = new List<Unit>();
    }

    public override string ToString()
    {
        string unitString = "";
        foreach (var unit in _unitList)
        {
            unitString += $"{unit}\n";
        }
        
        return _gridPosition.ToString() + $"\n{unitString}";
    }

    public bool HasAnyUnit()
    {
        return _unitList.Count > 0;
    }
}