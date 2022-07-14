using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visuals
{
    public class ForceMoveTerrain : MonoBehaviour
    {
        [Header("Selection Variable")]
        [Tooltip("Name of Tag to select for")]
        public string tagToSelect = "Terrain";

        // Detects if we hit the clenup barrier with any of the terrain.
        // If so, we force said terrain to move to the front
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tagToSelect))
            {
                Debug.Log(other.gameObject.name + " hits Cleanup Barrier!");
                other.GetComponentInParent<MoveTerrain>().MoveFirstMostTerrain();
            }
        }
    }

}