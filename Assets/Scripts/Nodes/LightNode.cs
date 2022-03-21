using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class LightNode : Node
{
    public int lightcir;
    public string lightID;


    public override string getOffStr()
    {
        string str = lightID + " " + /*"06 00 05 00 00"*/ ValueSheet.LightUnitOFFCmd[lightcir];

        string sendstr = str + " " + CRC.CRCCalc(str);

        return sendstr;
    }

    public override string getOnStr()
    {
        string str = lightID + " " + /*"06 00 0 00 01"*/ValueSheet.LightUnitONCmd[lightcir];

        string sendstr = str + " " + CRC.CRCCalc(str);

        return sendstr;
    }

    public override async Task OnClick()
    {

        if (Utility.checkIp(ip))
        {

            string str = lightID + " " + /*"06 00 0 00 01"*/ValueSheet.LightUnitONCmd[lightcir];

            string sendstr = str + " " + CRC.CRCCalc(str);

           // Debug.Log(sendstr);

            //await TCP.INSTANCE.sendHEXMsg(ip, port, sendstr);

            await UDP.instance.sendudpHEXMsg(ip, port, sendstr);
        }
    }

    public override async Task OffClick()
    {
        if (Utility.checkIp(ip))
        {
            string str = lightID + " " + /*"06 00 05 00 00"*/ ValueSheet.LightUnitOFFCmd[lightcir];


            string sendstr = str + " " + CRC.CRCCalc(str);

            // await TCP.INSTANCE.sendHEXMsg(ip, port, sendstr);

            await UDP.instance.sendudpHEXMsg(ip, port, sendstr);

        }
    }
}
