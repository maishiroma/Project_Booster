using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visuals;

namespace Gimmick
{
    public class BoostPanel : MonoBehaviour
    {
        [Header("General Variables")]
        [Tooltip("The current move speed the panel is traveling at")]
        public float currMoveSpeed;
        
        [Tooltip("The speed buff that the panel grants when touched")]
        public float speedBoostModifier;

        [Tooltip("The name of the tag to check to apply the boost towards")]
        public string applySpeedBoostTag;

        [Tooltip("The name of the tag that is associated with the groundd/terrain")]
        public string backgroundTerrainTag;

        // Private Variables
        private static MoveTerrain[] terrainGroups;       // An array of the terrain objects in the level

        // This finds all of the terrain objects in the level and associates them to the private variable
        private void Start()
        {
            if (terrainGroups == null)
            {
                GameObject[] temp = GameObject.FindGameObjectsWithTag(backgroundTerrainTag);
                terrainGroups = new MoveTerrain[temp.Length];
                for (int index = 0; index < terrainGroups.Length; ++index)
                {
                    terrainGroups[index] = temp[index].GetComponent<MoveTerrain>();
                }
            }
            // We also force the boost panels to be removed from the scene
            Invoke("DeleteMe", 5f);
        }

        private void FixedUpdate()
        {
            // We translate the panels based on the move vector and at the speed the move speed was going at
            currMoveSpeed = GetNewMoveSpeed();
            gameObject.transform.position += new Vector3(0,0,-1) * currMoveSpeed;
        }

        // If the specified object touches this, we "mock" the speed up by speeding up the terrain around the player
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(applySpeedBoostTag))
            {
                foreach(MoveTerrain currTerrain in terrainGroups)
                {
                    currTerrain.CurrMoveSpeed += speedBoostModifier;
                }
            }
        }

        // A helper method to just remove this gameobject from the scene
        // Called through an invoke
        private void DeleteMe()
        {
            Destroy(gameObject);
        }

        // We query the terrain's move speed to determine how fast we move the panels
        private float GetNewMoveSpeed()
        {
            int numbChanged = 0;
            float newValue = 0;
            foreach (MoveTerrain currTerrain in terrainGroups)
            {
                if (currTerrain.CurrMoveSpeed != currMoveSpeed)
                {
                    newValue = currTerrain.CurrMoveSpeed;
                    ++numbChanged;
                }
            }

            if (numbChanged == terrainGroups.Length)
            {
                return newValue;
            }
            return currMoveSpeed;
        }
    }
}