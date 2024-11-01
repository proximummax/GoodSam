using System;
using UnityEngine;
using static UnityEngine.PlayerPrefs;

public class UserDataStorage
{
    private static UserDataStorage _instance;

    public static UserDataStorage Instance
    {
        get
        {
            _instance ??= new UserDataStorage();
            return _instance;
        }
    }

    public int Round
    {
        get => GetInt("round", 0);
        set => SetInt("round", value);
    }
}
