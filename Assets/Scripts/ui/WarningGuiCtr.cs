using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public delegate Task m_taskcallBack();

public class WarningGuiCtr : MonoBehaviour
{

    public m_taskcallBack m_TaskcallBack;
    public static WarningGuiCtr INSTANCE;
    // Start is called before the first frame update

    private void Start()
    {
        INSTANCE = this;
    }
    public void OnClick()
    {
        ValueSheet.currentProcessBar.SetActive(true);

        ValueSheet.callBackList.Enqueue(m_TaskcallBack);

        UICtr.INSTANCE.CloseWarningGUI();
    }

    public void OffClick()
    {
        m_TaskcallBack = null;

        UICtr.INSTANCE.CloseWarningGUI();
    }
}
