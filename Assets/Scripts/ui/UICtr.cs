using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtr : MonoBehaviour
{
    public static UICtr INSTANCE;
    // Start is called before the first frame update
    void Start()
    {
        INSTANCE = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenWarningGUI()
    {
        ValueSheet.warningUI.SetActive(true);
    }

    public void CloseWarningGUI()
    {
        ValueSheet.warningUI.SetActive(false);
    }


}
