public interface IGameHUD {

	void SetStartingGameText(float startingTime);

	void UpdateGameHUD(StateNamespace.StageManager.GameState currentState);

}
