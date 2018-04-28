using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[AddComponentMenu("Game Menu/Menu Functionality")]
public class MenuFunctions : MonoBehaviour {

    [SerializeField]
    GameObject mainMenuScreen, levelSelectScreen, creditsScreen, encyclopediaScreen;

    public void PlayGame(string levelName)  {
        SceneManager.LoadScene(levelName);
    }

    public void SetMenuScreen(string menu)
    {
        SetMainMenuScreenVisiblity(false);
        SetLevelSelectVisibility(false);
        SetEncyclopediaVisibility(false);
        SetCreditsVisibility(false);
        switch (menu)
        {
            case "main menu":
                SetMainMenuScreenVisiblity(true);
                SetLevelSelectVisibility(false);
                SetEncyclopediaVisibility(false);
                SetCreditsVisibility(false);
                break;
            case "level select":
                SetMainMenuScreenVisiblity(false);
                SetLevelSelectVisibility(true);
                SetEncyclopediaVisibility(false);
                SetCreditsVisibility(false);
                break;
            case "enciclopedia":
                SetMainMenuScreenVisiblity(false);
                SetLevelSelectVisibility(false);
                SetEncyclopediaVisibility(true);
                SetCreditsVisibility(false);
                break;
            case "creditos":
                SetMainMenuScreenVisiblity(false);
                SetLevelSelectVisibility(false);
                SetEncyclopediaVisibility(false);
                SetCreditsVisibility(true);
                break;
            default:
                Debug.Log("Error! Unrecognized menu!");
                SetMainMenuScreenVisiblity(true);
                SetLevelSelectVisibility(false);
                SetEncyclopediaVisibility(false);
                SetCreditsVisibility(false);
                break;
        }
    }

    public void SetMainMenuScreenVisiblity(bool isVisible)
    {
        if (mainMenuScreen)
            mainMenuScreen.SetActive(isVisible);
    }

    public void SetLevelSelectVisibility(bool isVisible)
    {
        if(levelSelectScreen) 
            levelSelectScreen.SetActive(isVisible);
    }

    public void SetCreditsVisibility(bool isVisible)
    {
        if(creditsScreen)
            creditsScreen.SetActive(isVisible);
    }

    public void SetEncyclopediaVisibility(bool isVisible)
    {
        if(encyclopediaScreen)
            encyclopediaScreen.SetActive(isVisible);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
