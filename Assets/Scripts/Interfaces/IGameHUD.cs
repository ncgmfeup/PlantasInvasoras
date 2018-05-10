public interface IGameHUD {

	void SetStartingGameText(float startingTime);

	void SetPauseScreenVisibility(bool isVisible);

	void SetGameWonScreenVisibility(bool isVisible);

	void SetGameLostScreenVisibility(bool isVisible);

	void SetStartingScreenVisibility(bool isVisible);

	void SetGameHUDVisibility(bool isVisible);
	
}
