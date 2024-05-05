using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    private LevelSystem _levelSystem;

    private void Awake()
    {
        _levelSystem = FindObjectOfType<LevelSystem>();
        if (_levelSystem == null)
        {
            Debug.LogWarning("El LevelSystem no fue encontrado en la escena");
        }
    }

    public void ChangeLevel(string sceneToLoad)
    {
        if (sceneToLoad == null)
        {
            Debug.Log("sceneToLoad is null");
            return;
        }
        _levelSystem.LoadScene(sceneToLoad);
    }
}
