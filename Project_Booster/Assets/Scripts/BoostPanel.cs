using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visuals;
using Effects;

namespace Gimmick
{
    public class BoostPanel : SpawnObj
    {
        protected override void Effect()
        {
            foreach (TerrainMovement currTerrain in terrainGroups)
            {
                currTerrain.CurrMoveSpeed += effectModifier;
            }
            mainCamera.IsBoosting = true;
        }
    }
}