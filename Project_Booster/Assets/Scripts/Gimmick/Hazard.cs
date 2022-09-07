using UnityEngine;
using Visuals;
using ScreenGUI;
using Management;

namespace Gimmick
{
    public class Hazard : SpawnObj
    {
        [Tooltip("How much damage does this hazard do?")]
        public float damageAmount = 0.33f;

        // If interacted, the player will move slower
        protected override void Effect(GameObject target)
        {
            if (target.GetComponent<Health>() != null)
            {
                Health playerHealth = target.GetComponent<Health>();
                if (!playerHealth.IsInvincible)
                {
                    // If the player lost all of their health after that one hit, we set up the game over logic
                    playerHealth.CurrHealth -= damageAmount;
                    if (playerHealth.IsDead)
                    {
                        foreach (TerrainMovement currTerrain in terrainGroups)
                        {
                            currTerrain.CurrMoveSpeed = 0f;
                        }
                        scoreSystem.SetFinalScore();
                        GameManager.Instance.GameStatus = GameStatus.GAME_OVER;
                    }
                    else
                    {
                        // The player is slowed down
                        foreach (TerrainMovement currTerrain in terrainGroups)
                        {
                            currTerrain.CurrMoveSpeed += effectModifier;
                        }

                        // We cancel the camera effect and restart the score counter
                        mainCamera.IsBoosting = false;
                        scoreSystem.ResetComboTime();
                    }
                }
            }
        }
    }
}
