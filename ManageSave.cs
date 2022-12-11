using System;
using UnityEngine;

public class ManageSave : MonoBehaviour
{
    public void Play()
    {
        if (PlayerPrefs.GetInt("NewGame") == 0)
        {
            NewSave();
        }
    }

    public void NewSave()
    {
        PlayerPrefs.SetInt("NewGame", 1);
        PlayerPrefs.SetInt("EnemyHp", 1000);
        PlayerPrefs.SetInt("PlayerHpCur", 3);
        PlayerPrefs.SetInt("PlayerHpMax", 75);
        PlayerPrefs.SetInt("ManaPool", 0);
        PlayerPrefs.SetInt("GodRune", 0);
        PlayerPrefs.SetInt("GiantRune", 0);
        PlayerPrefs.SetInt("LifeRune", 0);
        PlayerPrefs.SetInt("ManRune", 0);
    }

    
}
