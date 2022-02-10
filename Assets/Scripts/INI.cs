using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INI : MonoBehaviour
{
    public GameObject WarningUI;
    // Start is called before the first frame update
    void Start()
    {
        ValueSheet.warningUI = WarningUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
