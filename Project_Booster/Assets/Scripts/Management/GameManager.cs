using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management
{
    public class GameManager : MonoBehaviour
    {
        // Public Static Variabbles
        public static GameManager Instance;     // Static Object Reference to this object

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
    }

}