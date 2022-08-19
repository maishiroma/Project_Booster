using UnityEngine;
using Visuals;
using ScreenGUI;

namespace Gimmick
{
    public class Hazard : SpawnObj
    {
        [Tooltip("How much damage does this hazard do?")]
        public float damageAmount = 0.33f;

        // If interacted, the player will move slower
        protected override void Effect(GameObject target)
        {
            foreach (TerrainMovement currTerrain in terrainGroups)
            {
                currTerrain.CurrMoveSpeed += effectModifier;
            }

            // We cancel the camera effect and restart the score counter
            mainCamera.IsBoosting = false;
            scoreSystem.ResetComboTime();
            
            // We also perform some health check operations
            if (target.GetComponent<Health>() != null)
            {
                Health playerHealth = target.GetComponent<Health>();
                playerHealth.CurrHealth -= damageAmount;

                // If the player lost all of their health after that one hit, we set up the game over logic
                if (playerHealth.IsDead)
                {
                    foreach (TerrainMovement currTerrain in terrainGroups)
                    {
                        currTerrain.CurrMoveSpeed = 0f;
                    }
                    scoreSystem.SetFinalScore();
                }
            }
        }
    }
}
