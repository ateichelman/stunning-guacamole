using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public UnityEvent<bool> OnBusyChanged;
    public event EventHandler OnActionStarted;
    
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private BaseAction _selectedAction;
    private bool _isBusy;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one UnitActionSystem!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        //select the default unit
        SetSelectedUnit(selectedUnit);
    }

    private void Update()
    {
        if (_isBusy)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (TryHandleUnitSelection())
        {
            return;
        }
        
        HandleSelectedAction();
    }


    private void HandleSelectedAction()
    {
        // TODO: If the course doesn't cover it, we need to come back and build some logic to clear the active action
        // after use. That way, you click an action, click again to use it, and then HAVE to click the button again
        // to use the action again. Currently, you only need to click the button once.
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if (_selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                if (selectedUnit.TrySpendActionPointsToTakeAction(_selectedAction))
                {
                    SetBusy();
                    _selectedAction.TakeAction(mouseGridPosition, ClearBusy);
                    
                    OnActionStarted?.Invoke(this,EventArgs.Empty);
                }

            }
        }
    }

    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetMoveAction());
        
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        // This does the same thing as above^^^
        // if (OnSelectedUnitChanged != null)
        // {
        //     OnSelectedUnitChanged(this, EventArgs.Empty);
        // }
    }

    private void SetBusy()
    {
        _isBusy = true;
        OnBusyChanged?.Invoke(true);
    }

    private void ClearBusy()
    {
        _isBusy = false;
        OnBusyChanged?.Invoke(false);
    }

    public void SetSelectedAction(BaseAction incomingAction)
    {
        _selectedAction = incomingAction;
        
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }

    public BaseAction GetSelectedAction()
    {
        return _selectedAction;
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
