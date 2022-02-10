using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public static class Utility 
{

    public static bool checkIp(string ipStr)
    {
        IPAddress ip;
        if (IPAddress.TryParse(ipStr, out ip))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //十六进制字符串转byte数组
    public static byte[] strToToHexByte(string hexString)
    {
        Debug.Log(hexString);

        hexString = hexString.Replace(" ", "");
        if ((hexString.Length % 2) != 0)
            hexString += " ";
        byte[] returnBytes = new byte[hexString.Length / 2];
        for (int i = 0; i < returnBytes.Length; i++)
            returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
        return returnBytes;
    }

}
