using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using StateNamespace;

public class GenericGameHUD : MonoBehaviour, IGameHUD {

	[SerializeField]
	GameObject m_winGameScreen, m_lostGameScreen, m_pausedGameScreen, m_startingGameCounter;

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

	public void ShowPauseScreen()
	{
		SetScreenElementVisibity(m_startingGameCounter, true);
	}

	public void HidePauseScreen()
	{
		SetScreenElementVisibity(m_startingGameCounter, false);
	}

	public void ShowGameWonScreen()
	{
		SetScreenElementVisibity(m_winGameScreen, true);
	}

	public void ShowGameLostScreen()
	{
		SetScreenElementVisibity(m_lostGameScreen, true);
	}

	public void HideStartingScreen()
	{
		SetScreenElementVisibity(m_startingGameCounter, false);
	}

	public void ReturnMainMenu()
	{
		SceneManager.LoadScene(0);
	}

}
