using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        UnitActionSystem.Instance.OnBusyChanged.AddListener(UnitActionSystem_OnBusyChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void UnitActionSystem_OnBusyChanged(bool arg)
    {
        gameObject.SetActive(arg);
    }
}
