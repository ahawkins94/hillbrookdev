using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //private AssetBundle myLoadedAssetBundle;
    //private string[] scenePaths;

    public static int buttonPosY = 400;
    public static int buttonPosX = 200;
    public static int buttonWidth = 300;
    public static int buttonHeight = 50;
    public static int spacing = 40;

    //public void Start()
    //{
    //    myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/Scenes");
    //    scenePaths = myLoadedAssetBundle.GetAllScenePaths();
    //}

    public void OnGUI() {
        if(GUI.Button(new Rect(buttonPosX, buttonPosY, buttonWidth, buttonHeight), "Main Game (Mobile Controls)")) {
            LoadMainGameMobileControls();
        }
        if (GUI.Button(new Rect(buttonPosX, buttonPosY - 1 * (spacing + buttonHeight), buttonWidth, buttonHeight), "Main Game (Unity Controls)")) {
            LoadMainGameUnityControls();
        }
        if (GUI.Button(new Rect(buttonPosX, buttonPosY - 2 * (spacing + buttonHeight), buttonWidth, buttonHeight), "Level Generation (Unity Controls)"))
        {
            LoadLevelGenerationUnityControls();
        }
        if (GUI.Button(new Rect(buttonPosX, buttonPosY - 3 * (spacing + buttonHeight), buttonWidth, buttonHeight), "Mobile Swipe Test (Mobile Controls)"))
        {
            LoadMobileSwipeTest();
        }
        if (GUI.Button(new Rect(buttonPosX, buttonPosY - 4 * (spacing + buttonHeight), buttonWidth, buttonHeight), "Player Controller (Unity Controls)"))
        {
            LoadPlayerUnityController();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Escape");
            GoLoadScene("Main");
        }
    }

    public void GoLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainGameMobileControls()
    {
        SceneManager.LoadScene("MainGameMobileControls");
    }

    public void LoadMainGameUnityControls()
    {
        SceneManager.LoadScene("MainGameUnityControls");
    }

    public void LoadLevelGenerationUnityControls()
    {
        SceneManager.LoadScene("LevelGenerationTest");
    }

    public void LoadMobileSwipeTest()
    {
        SceneManager.LoadScene("TestMobileControls");
    }

    public void LoadPlayerUnityController()
    {
        SceneManager.LoadScene("PlayerUnityController");
    }
}
