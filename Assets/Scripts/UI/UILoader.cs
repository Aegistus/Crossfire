using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    public static UILoader current;
    private bool uiLoaded = false;
    private Scene uiScene;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        else
        {
            Destroy(this);
        }
        LoadUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (uiLoaded)
            {
                UnloadUI();
            }
            else
            {
                LoadUI();
            }
        }
    }

    public void LoadUI()
    {
        SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        uiScene = SceneManager.GetSceneByName("UI");
        uiLoaded = true;
    }

    public void UnloadUI()
    {
        SceneManager.UnloadSceneAsync(uiScene);
        uiLoaded = false;
    }
}
