using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Env 
{
    public static string CURRENT_LEVEL_PREFS = "currentlevel";
    public static string LEVEL_TIMER_PREFS = "level_";
    public static string VOLUME_MUTE = "volume_mute";

    public static bool GetVolumeStatus()
    {
        int volumeMute;
        if (PlayerPrefs.HasKey(Env.VOLUME_MUTE))
        {
            volumeMute = PlayerPrefs.GetInt(Env.VOLUME_MUTE);
        }
        else
        {
            volumeMute = 0;
        }
        return volumeMute == 0 ? false : true;   
    }
}
