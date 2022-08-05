using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visuals;
using Player;

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
                target.GetComponent<Health>().CurrHealth -= damageAmount;
            }
        }
    }
}
