using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class PCNode : Node
{

    public override string getOffStr()
    {
        string sendstr = ValueSheet.MediaServerCmd[1];

        return sendstr;
    }

    public override string getOnStr()
    {
        string sendstr = ValueSheet.MediaServerCmd[0];

        return sendstr;
    }
    public override async Task OnClick()
    {

        if (Utility.checkIp(ip))
        {
            await TCP.INSTANCE.sendMsg(ip, port, getOnStr());
        }
    }

    public override async Task OffClick()
    {
        if (Utility.checkIp(ip))
        {
            await TCP.INSTANCE.sendMsg(ip, port, getOffStr());
        }
    }
}
