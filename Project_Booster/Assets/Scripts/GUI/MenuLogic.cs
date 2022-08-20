using UnityEngine;
using UnityEngine.InputSystem;
using Player;
using Management;

namespace ScreenGUI
{
    public class MenuLogic : MonoBehaviour
    {
        [Header("Level Vars")]
        [Tooltip("The name of the main level to play")]
        public string mainLevelName;

        // Private Variables
        private GameControls gameControls;                              // Ref to external control system to associate to this script
        private bool isLoading;                                         // Is the menu currently performing an operation?

        // Activates all of the controls for the player
        private void Awake()
        {
            // We need to first set this to be a new object before we can do anything
            gameControls = new GameControls();

            // Then we can set up calllbacks to specific methods that we want the controls to listen to
            gameControls.Menu.RestartLevel.performed += ctx => RestartLevel(ctx);
            gameControls.Menu.StartGame.performed += ctx => StartGame(ctx);
            gameControls.Menu.ExitGame.performed += ctx => ExitGame(ctx);
        }

        // We first grab the player component with its health and set private vars upp
        private void Start()
        {
            isLoading = false;
        }

        // Enables the controls when the player is active
        private void OnEnable()
        {
            gameControls.Enable();
        }

        // Diables the controls when the player is not active
        private void OnDisable()
        {
            gameControls.Disable();
        }

        // If we hit the specified button, and fulfill some of the requirements, we restart the level
        private void RestartLevel(InputAction.CallbackContext ctx)
        {
            if (!isLoading)
            {
                if (GameManager.Instance.GameStatus == GameStatus.GAME_OVER && ctx.ReadValueAsButton())
                {
                    isLoading = true;
                    GameManager.Instance.GameStatus = GameStatus.MAIN_GAME;
                    GameManager.Instance.ReloadCurrentLevel();
                }
            }
        }

        // When called, loads the main level
        private void StartGame(InputAction.CallbackContext ctx)
        {
            if (!isLoading)
            {
                if (GameManager.Instance.GameStatus == GameStatus.TITLE && ctx.ReadValueAsButton())
                {
                    isLoading = true;
                    GameManager.Instance.GameStatus = GameStatus.MAIN_GAME;
                    GameManager.Instance.LoadSpecificLevel(mainLevelName);
                }
            }
        }

        // When called, exits the game
        private void ExitGame(InputAction.CallbackContext ctx)
        {
            if (!isLoading)
            {
                if (GameManager.Instance.GameStatus != GameStatus.MAIN_GAME && ctx.ReadValueAsButton())
                {
                    isLoading = true;
                    GameManager.Instance.ExitGame();
                }
            }
        }
    }
}