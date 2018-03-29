using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[AddComponentMenu("Game Menu/Menu Functionality")]
public class MenuFunctions : MonoBehaviour {

    [SerializeField]
    GameObject mainMenuScreen, levelSelectScreen, creditsScreen, encyclopediaScreen;

    public void PlayGame(int levelBuildIndex)
    {
        SceneManager.LoadScene(levelBuildIndex);
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

    public void SetMainMenuScreenVisiblity(bool isVisible)
    {
        if (mainMenuScreen)
            mainMenuScreen.SetActive(isVisible);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
