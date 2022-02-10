using System;
using System.Collections.Generic;
using System.Text;

public static class CRC
{
    public static string CRCCalc(string data)
    {
        string[] datas = data.Split(' ');
        List<byte> bytedata = new List<byte>();

        foreach (string str in datas)
        {
            bytedata.Add(byte.Parse(str, System.Globalization.NumberStyles.AllowHexSpecifier));
        }
        byte[] crcbuf = bytedata.ToArray();
        //���㲢��дCRCУ����
        int crc = 0xffff;
        int len = crcbuf.Length;
        for (int n = 0; n < len; n++)
        {
            byte i;
            crc = crc ^ crcbuf[n];
            for (i = 0; i < 8; i++)
            {
                int TT;
                TT = crc & 1;
                crc = crc >> 1;
                crc = crc & 0x7fff;
                if (TT == 1)
                {
                    crc = crc ^ 0xa001;
                }
                crc = crc & 0xffff;
            }

        }
        string[] redata = new string[2];
        redata[1] = Convert.ToString((byte)((crc >> 8) & 0xff), 16);
        redata[0] = Convert.ToString((byte)((crc & 0xff)), 16);
        //Debug.Log("У������" + redata[0] + " " + redata[1]);
        if (redata[0].Length == 1)
        {
            redata[0] = "0" + redata[0];
        }

        return redata[0] + " " + redata[1];
    }

}
