using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using StateNamespace;

public class GenericGameHUD : MonoBehaviour, IGameHUD {

	[SerializeField]
	GameObject m_winGameScreen,
			   m_lostGameScreen,
			   m_pausedGameScreen,
			   m_startingGameCounter,
			   m_gameHUD;

	void Awake () {
		if(!m_winGameScreen)
			Debug.LogError("Win game screen not assigned.");

		if(!m_lostGameScreen)
			Debug.LogError("Lost game screen not assigned.");

		if(!m_pausedGameScreen)
			Debug.LogError("Paused game screen not assigned.");

		if(!m_startingGameCounter)
			Debug.LogError("Starting game counter not assigned.");
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

	public void ReturnMainMenu()
	{
		SceneManager.LoadScene(0);
	}

}
