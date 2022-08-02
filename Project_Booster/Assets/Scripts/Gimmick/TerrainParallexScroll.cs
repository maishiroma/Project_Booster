using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmick
{
    public class TerrainParallexScroll : MonoBehaviour
    {
        [Header("External Refs")]
        [Tooltip("Ref to all of the terrainGroups in the level. Funneled into pairedTerrainLL")]
        public List<GameObject> terrainGroups = new List<GameObject>();

        [Header("Selection Variable")]
        [Tooltip("Name of Tag to select for")]
        public string tagToSelect = "Terrain";

        [Tooltip("What is the disitance between each plane group?")]
        public Vector3 addPlacement;

        private LinkedList<GameObject> pairedTerrainLL = new LinkedList<GameObject>();      // List of all plane groups

        // We set up the pairedTerrainLL and then discard terrainGroups
        private void Start()
        {
            foreach (GameObject currGroup in terrainGroups)
            {
                pairedTerrainLL.AddLast(currGroup);
            }
            terrainGroups.Clear();
        }

        // Detects if we hit the clenup barrier with any of the terrain.
        // If so, we force said terrain to move to the front
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tagToSelect))
            {
                DeleteGimmicksInTerrain(other.gameObject);
                MoveFirstMostTerrain();
            }
        }

        // When called, we move the firstmost terrain to the back of the line
        // of terrain groups
        private void MoveFirstMostTerrain()
        {            
            LinkedListNode<GameObject> firstObj = pairedTerrainLL.First;
            LinkedListNode<GameObject> lastObj = pairedTerrainLL.Last;

            pairedTerrainLL.RemoveFirst();
            pairedTerrainLL.AddAfter(lastObj, firstObj);

            firstObj.Value.transform.position = lastObj.Value.transform.position + addPlacement;
        }

        // We select all of the gimicks in the specified terrain object and removes them
        private void DeleteGimmicksInTerrain(GameObject firstObj)
        {
            SpawnObj[] children = firstObj.GetComponentsInChildren<SpawnObj>();
            foreach (SpawnObj currOne in children)
            {
                Destroy(currOne.gameObject);
            }           
        }

        // Helper method to get tthe last paired terrain value
        public GameObject GetLastTerrain()
        {
            return pairedTerrainLL.Last.Value;
        }
    }
}