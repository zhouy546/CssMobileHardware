using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;



public class Node 
{
    public string name;
    public string ip;
    public int port;

    public virtual string getOffStr()
    {
        return "";
    }

    public virtual string getOnStr()
    {
        return "";
    }

    public virtual async Task OnClick()
    {
        await Task.Yield();
    }

    public virtual async Task OffClick()
    {
        await Task.Yield();

    }

}
