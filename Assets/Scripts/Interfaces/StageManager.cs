using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolNamespace;
using PlantNamespace;

namespace StateNamespace {
	public abstract class StageManager : MonoBehaviour
    {
        public enum GameState {
            Starting, Playing, Paused, GameWon, GameLost
        }

        public static StageManager sharedInstance;

        [SerializeField]
        protected GameState m_gameState;

        public Player m_scenePlayer;

        [SerializeField]
        protected int m_maxInvadingPlantsBeforeGameLost;

        private TouchManager touchManager;

        protected IGameHUD m_currentHUD;

        private float m_timeBetweenTaps = 0.5f;
        protected bool canUseTool;
        [SerializeField]
        private float m_timeUntilGameStarts = 3.5f;

        private void Awake()
        {
            if (!sharedInstance) {
                sharedInstance = this;
            }
            else
            {
                Debug.LogWarning("Found an extra StageManager in the scene! Destroying GameObject: " 
                    + gameObject.name + "...");
                Destroy(gameObject);
            }
        }

        // Use this for initialization
        void Start()
        {
            canUseTool = true;
            touchManager = GetComponent<TouchManager>();
            if(!touchManager)
                Debug.LogError("Touch manager component not found. Please create one on the GameObject: " + gameObject.name);

            m_currentHUD = GetComponent<IGameHUD>();
            if(m_currentHUD == null)
                Debug.LogError("Game HUD component not found. Please create one on the GameObject: " + gameObject.name);

            CheckIfPlayerExists();

            InitializeVariables();

            m_gameState = GameState.Starting;
        }

        void CheckIfPlayerExists() {
            GameObject gameManager = GameObject.Find("GameManager");
            m_scenePlayer = gameManager.GetComponent<Player>();
        }


        // Update is called once per frame
        void Update() {
            if(m_timeUntilGameStarts > 0.0f)
            {
                m_timeUntilGameStarts -= Time.deltaTime;
                m_currentHUD.SetStartingGameText(m_timeUntilGameStarts);
            }
            else if(!(m_gameState == GameState.Paused || 
                 m_gameState == GameState.GameLost ||
                 m_gameState == GameState.GameWon))
            {
                UpdateGameState();

                touchManager.updateTouch();

                HandleDifficulty();

                CheckGameState();

                m_currentHUD.UpdateGameHUD(m_gameState);
            }
        }

        // To detect the win/lose condition.
        protected virtual bool CheckGameState()
        {
            if (m_gameState == GameState.Paused)
                return false;

            m_gameState = GameState.Playing;
            int currentlySpawnedInvadingPlants = 
                PlantObjectPooler.sharedInstance.GetNumberOfActiveInvadingPlants();

            //If all invading plants in the plant pooler are inactive
            if (currentlySpawnedInvadingPlants == 0) {
                m_gameState = GameState.GameWon;
            }

            return true;
        }

        // Initializes all stage specific variables
        public abstract void InitializeVariables();

        // Stage checks to see how challenging game is becoming, might come in handy.
        public abstract void HandleDifficulty();

        // Updates all the necessary components in a frame
        public abstract void UpdateGameState();
        public void touched(Vector3 touch) {
            if (m_scenePlayer.GetSelectedWeapon() == Utils.BOMB_SEL && canUseTool) {

                canUseTool = false;
                
                GameObject newTool = Instantiate(m_scenePlayer.GetTool(Utils.BOMB_SEL),
                    touch, Quaternion.identity);
                
                newTool.GetComponent<Bomb>().UseTool(touch);

                StartCoroutine("DecreaseTime");
                

                //Play Sound
                //soundManager.playBombSound();
            }
        }
        public abstract void HitSomething(GameObject obj); 

        IEnumerator DecreaseTime() {
            yield return new WaitForSeconds(m_timeBetweenTaps);
            canUseTool = true;
        }

        public void SpawnInvadingPlant(Vector3 pos) {
            PlantObjectPooler.sharedInstance.SpawnInvadingPlantAtPosition(pos);
        }

        public void SpawnNativePlant(Vector3 pos) {
            PlantObjectPooler.sharedInstance.SpawnNativePlantAtPosition(pos);
        }
        
        public void PauseGame()
        {
            m_gameState = GameState.Paused;
        }

    }
}