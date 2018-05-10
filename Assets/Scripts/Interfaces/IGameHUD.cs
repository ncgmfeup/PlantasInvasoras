public interface IGameHUD {

	void SetStartingGameText(float startingTime);

	void ShowPauseScreen();

	void HidePauseScreen();

	void ShowGameWonScreen();

	void ShowGameLostScreen();

	void HideStartingScreen();

}
