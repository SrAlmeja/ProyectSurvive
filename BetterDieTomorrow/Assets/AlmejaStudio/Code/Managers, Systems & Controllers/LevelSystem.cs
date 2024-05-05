using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    private static LevelSystem _instance;
    public static  LevelSystem Instance
    {
        get { return _instance; }
    }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            Debug.LogWarning("Mas controladores de tipo LevelSystem se han encontrado en la escena");
            return;
        }

        _instance = this;
        
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    
    public void Resume()
    {
        Time.timeScale = 1f;
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}