using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visuals;
using ScreenGUI;

namespace Gimmick
{
    public class Hazard : SpawnObj
    {
        public float damageAmount;

        // If interacted, the player will move slower
        protected override void Effect(GameObject target)
        {
            foreach (TerrainMovement currTerrain in terrainGroups)
            {
                currTerrain.CurrMoveSpeed += effectModifier;
            }
            mainCamera.IsBoosting = false;
            scoreSystem.ResetComboTime();
            
            if (target.GetComponent<Health>() != null)
            {
                Health playerHealth = target.GetComponent<Health>();
                playerHealth.CurrHealth -= damageAmount;

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
