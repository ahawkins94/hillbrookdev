using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuV1 : MonoBehaviour {

    public static int buttonPosY = 300;
    public static int buttonPosX = 500;
    public static int buttonWidth = 300;
    public static int buttonHeight = 50;
    public static int spacing = 40;

    public void OnGUI() {
        if (GUI.Button(new Rect(buttonPosX, buttonPosY, buttonWidth, buttonHeight), "Play"))
        {
            LoadLevelGeneration();
        }
    }

    public static void GoLoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
  
    public void LoadLevelGeneration()
        {
            SceneManager.LoadScene("Version 0.1");
        }
}

