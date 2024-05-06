using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    #region Variables

    private LevelSystem _levelSystem;

    [SerializeField] private Button[] buttons;

    #endregion
    

    private void Awake()
    {
        _levelSystem = FindObjectOfType<LevelSystem>();
        if (_levelSystem == null)
        {
            Debug.LogWarning("El LevelSystem no fue encontrado en la escena");
        }

        int unlokedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < unlokedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void ChangeLevel(int levelID)
    {
        string levelName = "0" + (levelID + 1) + "map" + levelID; 
        _levelSystem.LoadScene(levelName);
    }
}
