using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Player;
using Management;

namespace ScreenGUI
{
    public class MenuLogic : MonoBehaviour
    {
        private GameControls gameControls;                              // Ref to external control system to associate to this script
        private Health player;
        private bool isLoading;

        // Activates all of the controls for the player
        private void Awake()
        {
            // We need to first set this to be a new object before we can do anything
            gameControls = new GameControls();

            // Then we can set up calllbacks to specific methods that we want the controls to listen to
            gameControls.Menu.RestartLevel.performed += ctx => RestartLevel(ctx);

        }

        private void Start()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            }
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

        private void RestartLevel(InputAction.CallbackContext ctx)
        {
            if (player != null && !isLoading)
            {
                if (player.IsDead && ctx.ReadValueAsButton())
                {
                    isLoading = true;
                    GameManager.Instance.ReloadCurrentLevel();
                }
            }    
        }
    }

}