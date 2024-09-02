using System;
using UnityEngine;
using static UnityEngine.PlayerPrefs;

public class UserSettingsStorage
{
    private static UserSettingsStorage _instance;

    public static UserSettingsStorage Instance
    {
        get
        {
            _instance ??= new UserSettingsStorage();
            return _instance;
        }
    }

    public int Round
    {

        get => GetInt("round", 0);
        set => SetInt("round", value);
    }
}
