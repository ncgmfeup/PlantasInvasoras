using UnityEngine;
using UnityEngine.SceneManagement;
using StateNamespace;

public class GenericGameHUD : MonoBehaviour, IGameHUD {

	[SerializeField]
	GameObject m_winGameScreen, m_lostGameScreen, m_pausedGameScreen, m_startingGameScreen, m_gameHUD;

	void Awake () {
		if(!m_winGameScreen)
			Debug.LogError("Win game screen not assigned.");

		if(!m_lostGameScreen)
			Debug.LogError("Lost game screen not assigned.");

		if(!m_pausedGameScreen)
			Debug.LogError("Paused game screen not assigned.");

		if(!m_startingGameScreen)
			Debug.LogError("Starting game screen not assigned.");

		if(!m_gameHUD)
			Debug.LogError("Main game HUD not assigned.");
	}

	// Shows the correct part of the hud based on what the current state of the game is.
	public void UpdateGameHUD(StageManager.GameState currentState)
	{
		switch (currentState)
		{
			case StageManager.GameState.Playing:
				SetScreenElementVisibity(m_pausedGameScreen, false);
				SetScreenElementVisibity(m_startingGameScreen, false);
				break;
			case StageManager.GameState.Paused:
				SetScreenElementVisibity(m_pausedGameScreen, true);
				break;
			case StageManager.GameState.Starting:
				SetScreenElementVisibity(m_startingGameScreen, true);
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
		}
	}

	private void SetScreenElementVisibity(GameObject hudElement, bool isVisible)
	{
		if(hudElement)
			hudElement.SetActive(isVisible);
	}

	public void ReturnMainMenu()
	{
		SceneManager.LoadScene(0);
	}
}
