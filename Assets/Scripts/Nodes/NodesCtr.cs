using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class NodesCtr : MonoBehaviour
{
    public List<PCNode> pcs = new List<PCNode>();

    public List<LightNode> Lights = new List<LightNode>();

    public List<LedNode> leds = new List<LedNode>();

    public List<LightNode> RackPower = new List<LightNode>();

    private List<Node> nodes = new List<Node>();


    public List<LightNode> ecoLighton = new List<LightNode>();

    public List<LightNode> ecoLightoff = new List<LightNode>();


    public Image FillImage;

    public Text persentageText;

    public GameObject processBar;

    private float lightindex;

    private void Start()
    {
        nodes.AddRange(pcs);
        //nodes.AddRange(Lights);//不添加灯光，原因是开关所有不包括灯光
        nodes.AddRange(leds);
        nodes.AddRange(RackPower);
    }


    private async Task ECOLightOn()
    {
        float index = 0;
        for (int i = 0; i < ecoLighton.Count; i++)
        {
            index++;
            await ecoLighton[i].OnClick();
            FillImage.fillAmount = index / (ecoLighton.Count+ ecoLightoff.Count);

            persentageText.text = Mathf.CeilToInt(FillImage.fillAmount * 100).ToString() + "%";
        }

        for (int i = 0; i < ecoLightoff.Count; i++)
        {
            index++;
            await ecoLightoff[i].OffClick();
            FillImage.fillAmount = index / (ecoLightoff.Count+ ecoLighton.Count);

            persentageText.text = Mathf.CeilToInt(FillImage.fillAmount * 100).ToString() + "%";
        }

        processBar.SetActive(false);

        TCP.INSTANCE.shutDownSocket();

    }


    private async Task OnClick()
    {
        float index = 0;
        //打开机柜
        for (int i = 0; i < RackPower.Count; i++)
        {
            index++;
            await RackPower[i].OnClick();
            FillImage.fillAmount = index / (nodes.Count);

            persentageText.text = Mathf.CeilToInt(FillImage.fillAmount * 100).ToString()+"%"; 
        }
        //如果需要打开机柜电源，交换机开始通讯需要需要等待80秒
        if (RackPower.Count > 0)
        {
            await Task.Delay(80000);
        }

        //打开LED电箱
        for (int i = 0; i < leds.Count; i++)
        {
            index++;
            await leds[i].OnClick();
            FillImage.fillAmount = index / (nodes.Count);

            persentageText.text = Mathf.CeilToInt(FillImage.fillAmount * 100).ToString() + "%";
        }
        //打开PC电箱
        for (int i = 0; i < pcs.Count; i++)
        {
            index++;

            Debug.Log(index);
            Debug.Log(pcs[i].ip);
            await pcs[i].OnClick();
            FillImage.fillAmount = index / (nodes.Count);

            persentageText.text = Mathf.CeilToInt(FillImage.fillAmount * 100).ToString() + "%";
        }

        //打开灯光
        for (int i = 0; i < Lights.Count; i++)
        {
            await Lights[i].OnClick();
            FillImage.fillAmount = (float)(i+1) / (Lights.Count);

            persentageText.text = Mathf.CeilToInt(FillImage.fillAmount * 100).ToString() + "%";
        }

        processBar.SetActive(false);
    }

    private async Task OffClick()
    {
        float index = 0;
        //关闭PC
        for (int i = 0; i < pcs.Count; i++)
        {
            index++;
            Debug.Log(index);

            await pcs[i].OffClick();
            FillImage.fillAmount = index / (nodes.Count);
            persentageText.text = Mathf.CeilToInt(FillImage.fillAmount*100).ToString()+"%";
        }
        //关闭LED
        for (int i = 0; i < leds.Count; i++)
        {
            index++;
            await leds[i].OffClick();
            FillImage.fillAmount = index / (nodes.Count);
            persentageText.text = Mathf.CeilToInt(FillImage.fillAmount * 100).ToString() + "%";
        }

        //关闭灯光
        for (int i = 0; i < Lights.Count; i++)
        {
            await Lights[i].OffClick();
            FillImage.fillAmount = (float)(i + 1) / (Lights.Count);

            persentageText.text = Mathf.CeilToInt(FillImage.fillAmount * 100).ToString() + "%";
        }

        if (RackPower.Count > 0)
        {
            await Task.Delay(20000);
        }

        //关闭机柜
        for (int i = 0; i < RackPower.Count; i++)
        {
            index++;
            await RackPower[i].OffClick();
            FillImage.fillAmount = index / (nodes.Count);
            persentageText.text = Mathf.CeilToInt(FillImage.fillAmount * 100).ToString() + "%";
        }
        processBar.SetActive(false);
    }


    public void EcoBtnClick()
    {
        ValueSheet.currentProcessBar = processBar;

        WarningGuiCtr.INSTANCE.m_TaskcallBack = ECOLightOn;

        UICtr.INSTANCE.OpenWarningGUI();
    }

    public void OnBtnClick()
    {
        ValueSheet.currentProcessBar = processBar;     

        WarningGuiCtr.INSTANCE.m_TaskcallBack = OnClick;

        UICtr.INSTANCE.OpenWarningGUI();
    }

    public void OffBtnClick()
    {
        ValueSheet.currentProcessBar = processBar;

        WarningGuiCtr.INSTANCE.m_TaskcallBack = OffClick;

        UICtr.INSTANCE.OpenWarningGUI();
    }
}

