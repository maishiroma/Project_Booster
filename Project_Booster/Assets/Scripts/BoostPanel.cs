using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visuals;
using Effects;

namespace Gimmick
{
    public class BoostPanel : MonoBehaviour
    {
        [Header("General Variables")]
        
        [Tooltip("The speed buff that the panel grants when touched")]
        public float speedBoostModifier;

        [Space]

        [Tooltip("The name of the tag to check to apply the boost towards")]
        public string applySpeedBoostTag;

        [Tooltip("The name of the tag that is associated with the ground/terrain")]
        public string terrainGroupTag;

        [Tooltip("The name of the tag associated with the main camera")]
        public string mainCameraTag;

        // Private Variables
        private static TerrainMovement[] terrainGroups;       // An array of the terrain objects in the level; all boost panels share this information
        private static CameraMovement mainCamera;           // Ref to the main camera object

        // This finds all of the terrain objects in the level and associates them to the private variable
        private void Start()
        {
            if (terrainGroups == null)
            {
                GameObject[] temp = GameObject.FindGameObjectsWithTag(terrainGroupTag);
                terrainGroups = new TerrainMovement[temp.Length];
                for (int index = 0; index < terrainGroups.Length; ++index)
                {
                    terrainGroups[index] = temp[index].GetComponent<TerrainMovement>();
                }
            }

            if (mainCamera == null)
            {
                mainCamera = GameObject.FindGameObjectWithTag(mainCameraTag).GetComponent<CameraMovement>();
            }
        }

        // If the specified object touches this, we "mock" the speed up by speeding up the terrain around the player
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(applySpeedBoostTag))
            {
                foreach(TerrainMovement currTerrain in terrainGroups)
                {
                    currTerrain.CurrMoveSpeed += speedBoostModifier;
                }
                mainCamera.IsBoosting = true;
            }
        }
    }
}