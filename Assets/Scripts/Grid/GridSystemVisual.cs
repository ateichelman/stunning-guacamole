using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance { get; private set; }

    [SerializeField] private GameObject gridSystemVisualSinglePrefab;
    
    private GridSystemVisualSingle[,] _gridSystemVisualSingleArray;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one LevelGrid!");
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _gridSystemVisualSingleArray = new GridSystemVisualSingle[
            LevelGrid.Instance.GetWidth(),
            LevelGrid.Instance.GetHeight()
        ];
        
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                GameObject gridSystemSingleObject = 
                    Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), quaternion.identity);

                _gridSystemVisualSingleArray[x, z] = gridSystemSingleObject.GetComponent<GridSystemVisualSingle>();

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGridVisual();
    }

    private void UpdateGridVisual()
    {
        HideAllGridPositions();

        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();
        
        ShowGridPositions(selectedAction.GetValidActionGridPositionList());

        // Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        //
        // List<GridPosition> validPositions = selectedUnit.GetMoveAction().GetValidActionGridPositionList();
        // ShowGridPositions(validPositions);
    }

    public void HideAllGridPositions()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                _gridSystemVisualSingleArray[x, z].Hide();

            }
        }
    }

    public void ShowGridPositions(List<GridPosition> gridPositions)
    {
        foreach (var position in gridPositions)
        {
            _gridSystemVisualSingleArray[position.x, position.z].Show();
        }
    }
}
