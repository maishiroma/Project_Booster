using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management
{
    // Keeps track of the overall game state
    public enum GameStatus
    {
        TITLE,  // Player is in title screen
        MAIN_GAME,  // Player is in main game
        GAME_OVER   // Player has game overed
    }

    public class GameManager : MonoBehaviour
    {
        // Public Static Variabbles
        public static GameManager Instance;     // Static Object Reference to this object

        // Private Variables
        private GameStatus gameStatus;          // Ref to the current state the game is in

        // Getter/Setter
        public GameStatus GameStatus
        {
            get { return gameStatus; }
            set
            {
                gameStatus = value;
            }
        }

        // Sets up the static public object and makes it persistent across levels
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        // Initializes the game status
        private void Start()
        {
            gameStatus = GameStatus.TITLE;
        }

        // Quickly reloads the current scene
        public void ReloadCurrentLevel()
        {
            string currScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currScene);
        }

        // Loads a specific level up
        public void LoadSpecificLevel(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }

        // Quits the game
        public void ExitGame()
        {
            Application.Quit();
        }
    }

}