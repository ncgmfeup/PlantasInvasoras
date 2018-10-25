using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using StateNamespace;

public class GenericGameHUD : MonoBehaviour, IGameHUD {

	
	private StageManager m_stageManager;

	[SerializeField]
	GameObject m_winGameScreen,
			   m_lostGameScreen,
			   m_pausedGameScreen,
			   m_startingGameCounter,
			   m_gameHUD,
			   m_tutorialScreen,
			   m_nextPageButton,
			   m_skipButton;
	
	[SerializeField]
	Text 	m_tutorialTextBox,
			m_nextButtonText;

	public string[] texts;
	private int currentPage = 0;

	void Awake () {
		if(!m_winGameScreen)
			Debug.LogError("Win game screen not assigned.");

		if(!m_lostGameScreen)
			Debug.LogError("Lost game screen not assigned.");

		if(!m_pausedGameScreen)
			Debug.LogError("Paused game screen not assigned.");

		if(!m_startingGameCounter)
			Debug.LogError("Starting game counter not assigned.");
		if(!m_tutorialScreen)
			Debug.LogError("Tutorial screen not assigned.");

		currentPage = 0;
		m_tutorialTextBox.text = texts[0];

		m_stageManager = gameObject.GetComponent<StageManager>();
	}

	private void SetScreenElementVisibity(GameObject hudElement, bool isVisible)
	{
		if(hudElement)
			hudElement.SetActive(isVisible);
		else
		{
			#if UNITY_EDITOR
				Debug.LogError("Undefined element.");
			#endif
		}
	}

	public void SetStartingGameText(float startingTime)
	{
		if(startingTime > 0f)
			m_startingGameCounter.GetComponent<Text>().text = Mathf.Ceil(startingTime).ToString() + "...";
		else
			m_startingGameCounter.GetComponent<Text>().text = "Start!";
	}

	public void SetPauseScreenVisibility(bool isVisible)
	{
		SetScreenElementVisibity(m_startingGameCounter, isVisible);
	}

	public void SetGameWonScreenVisibility(bool isVisible)
	{
		SetScreenElementVisibity(m_winGameScreen, isVisible);
	}

	public void SetGameLostScreenVisibility(bool isVisible)
	{
		SetScreenElementVisibity(m_lostGameScreen, isVisible);
	}

	public void SetStartingScreenVisibility(bool isVisible)
	{
		SetScreenElementVisibity(m_startingGameCounter, isVisible);
	}

	public void SetGameHUDVisibility(bool isVisible)
	{
		SetScreenElementVisibity(m_gameHUD, isVisible);
	}

	public void SetTutorialVisibility(bool isVisible)
	{
		SetScreenElementVisibity(m_tutorialScreen, isVisible);
	}

	public void SetSkipButtonVisibility(bool isVisible)
	{
		SetScreenElementVisibity(m_skipButton, isVisible);
	}

	public void ReturnMainMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void NextTutorialPage()
	{
		currentPage++;
		if (currentPage >= texts.Length) {
			SetTutorialVisibility(false);
			SetStartingScreenVisibility(true);
			m_stageManager.StartGame();
		}
		else {
			if (currentPage == texts.Length - 1) {
				SetSkipButtonVisibility(false);
				m_nextButtonText.text = "Começar";
			}
			m_tutorialTextBox.text = texts[currentPage];
		}
	}

	public void SkipTutorial() {
		SetTutorialVisibility(false);
		SetStartingScreenVisibility(true);
		m_stageManager.StartGame();
	}

}
