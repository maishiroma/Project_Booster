using UnityEngine;
using Visuals;

namespace Gimmick
{
    public class BoostPanel : SpawnObj
    {
        // If interacted, the player will move faster
        protected override void Effect(GameObject target)
        {
            foreach (TerrainMovement currTerrain in terrainGroups)
            {
                currTerrain.CurrMoveSpeed += effectModifier;
            }
            
            // We also make sure the camera effect is on as well as invoke the score counter
            mainCamera.IsBoosting = true;
            scoreSystem.InvokeComboTime();
        }
    }
}