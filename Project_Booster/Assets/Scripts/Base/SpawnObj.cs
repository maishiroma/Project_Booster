using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visuals;
using Effects;
using Player;

namespace Gimmick
{
    public abstract class SpawnObj : MonoBehaviour
    {
        [Tooltip("The speed buff that the panel grants when touched")]
        public float effectModifier;

        [Space]

        [Tooltip("The name of the tag to check to apply the boost towards")]
        public string colliderTagCheck;

        [Tooltip("The name of the tag that is associated with the ground/terrain")]
        public string terrainGroupTag;

        [Tooltip("The name of the tag associated with the main camera")]
        public string mainCameraTag;

        // protected Variables
        protected static TerrainMovement[] terrainGroups;       // An array of the terrain objects in the level; all boost panels share this information
        protected static CameraMovement mainCamera;             // Ref to the main camera object
        protected static ScoreSystem scoreSystem;               // Ref to the scoring system of the game

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

            if (scoreSystem == null)
            {
                scoreSystem = GameObject.FindObjectOfType<ScoreSystem>();
            }
        }

        // If the specified object touches this, we "mock" the speed up by speeding up the terrain around the player
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(colliderTagCheck))
            {
                Effect(other.gameObject);
            }
        }

        // The method that all objects should implement, since this will be ran once the detection has hit
        protected abstract void Effect(GameObject target);
    }

}