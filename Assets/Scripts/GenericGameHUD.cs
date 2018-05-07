using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using StateNamespace;

public class GenericGameHUD : MonoBehaviour, IGameHUD {

	[SerializeField]
	GameObject m_winGameScreen, m_lostGameScreen, m_pausedGameScreen, m_gameHUD, m_startingGameCounter;

	void Awake () {
		if(!m_winGameScreen)
			Debug.LogError("Win game screen not assigned.");

		if(!m_lostGameScreen)
			Debug.LogError("Lost game screen not assigned.");

		if(!m_pausedGameScreen)
			Debug.LogError("Paused game screen not assigned.");

		if(!m_startingGameCounter)
			Debug.LogError("Starting game counter not assigned.");

		if(!m_gameHUD)
			Debug.LogError("Main game HUD not assigned.");
	}

	// Shows the correct part of the hud based on what the current state of the game is.
	public void UpdateGameHUD(StageManager.GameState currentState)
	{  
		/*
		switch (currentState)
		{
			case StageManager.GameState.Playing:
				SetScreenElementVisibity(m_pausedGameScreen, false);
				SetScreenElementVisibity(m_startingGameCounter, false);
				break;
			case StageManager.GameState.Paused:
				SetScreenElementVisibity(m_pausedGameScreen, true);
				break;
			case StageManager.GameState.Starting:
				SetScreenElementVisibity(m_startingGameCounter, true);
				break;
			case StageManager.GameState.GameWon:
				SetScreenElementVisibity(m_winGameScreen, true);
				break;
			case StageManager.GameState.GameLost:
				SetScreenElementVisibity(m_lostGameScreen, true);
				break;
			default:
				Debug.LogWarning("Recieved new game state definition. Are you sure you defined a correct case for every state?");
				break;
		}  */
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
		if(startingTime > 0.5f)
			m_startingGameCounter.GetComponent<Text>().text = Mathf.Ceil(startingTime).ToString();
		else if(startingTime > 0f)
			m_startingGameCounter.GetComponent<Text>().text = "Start!";
	}

	public void PauseGame()
	{
		StageManager.sharedInstance.PauseGame();
	}

	public void ReturnMainMenu()
	{
		SceneManager.LoadScene(0);
	}

}
