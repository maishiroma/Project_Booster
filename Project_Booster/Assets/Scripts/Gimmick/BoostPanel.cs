using System.Collections;
using System.Collections.Generic;
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
            mainCamera.IsBoosting = true;
            scoreSystem.InvokeComboTime();
        }
    }
}