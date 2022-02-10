using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class ValueSheet 
{
    public static GameObject warningUI;

    public static Queue<m_taskcallBack> callBackList = new Queue<m_taskcallBack>();

    public static GameObject currentProcessBar;

    #region pc
    public static string[] MediaServerCmd = { "start", "stop", "read" };

    #endregion

    #region 灯光收发

    public static string[] LightUnitONCmd = { "06 00 02 00 01","06 00 03 00 01", "06 00 04 00 01", "06 00 05 00 01", "06 00 06 00 01",
    "06 00 07 00 01","06 00 08 00 01","06 00 09 00 01","06 00 0A 00 01" ,"06 00 0B 00 01","06 00 0C 00 01","06 00 0D 00 01",
    "06 00 0E 00 01"};
    public static string[] LightUnitOFFCmd = { "06 00 01 00 00"/*OFF*/,"06 00 03 00 00", "06 00 04 00 00", "06 00 05 00 00", "06 00 06 00 00",
    "06 00 07 00 00","06 00 08 00 00","06 00 09 00 00","06 00 0A 00 00" ,"06 00 0B 00 00","06 00 0C 00 00","06 00 0D 00 00",
    "06 00 0E 00 00"};
    public static string[] LightCmd = { "06 00 02 00 01"/*ON*/, "06 00 01 00 00"/*OFF*/, "03 00 03 00 01"/*READ*/};
    public static string[] LightReceiveCmd = { "03020001"/*READ回值开*/, "03020000"/*READ回值关*/, "0600020001"/*发送开后回值*/, "0600010000"/*发送关后回值*/};
    #endregion

    #region LED电柜
    public static string[] LEDCmd = { "00000000000601050801FF00"/*on*/, "00000000000601050802FF00"/*off*/, };

    #endregion

}
