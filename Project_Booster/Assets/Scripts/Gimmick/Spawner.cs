using System.Collections.Generic;
using UnityEngine;

namespace Gimmick
{
    public class Spawner : MonoBehaviour
    {
        [Header("External References")]
        [Tooltip("Reference to the prefab that holds information on what can be spawned")]
        public GameObject[] spawnObjPrefabs;

        [Tooltip("Reference to the object that will be spawning objects in the level")]
        public TerrainParallexScroll cleanupObj;

        [Header("Spawn Variables")]
        [Tooltip("Min time it take for an object to spawn")]
        public float minTimeToSpawn;
        [Tooltip("Max time it takes for an object to spawn")]
        public float maxTimeToSpawn;
        [Tooltip("Min numb of objs that would be spawned")]
        public int minSpawnCount;
        [Tooltip("Max numb of objs that would be spawned")]
        public int maxSpawnCount;

        public float modCounter;

        // Private Variables
        private float timeToSpawn;      // How long does it take for another object to spawn?
        private float currTimePassed;   // How much time has passed since the last object spawned?

        private List<Transform> spawnPos = new List<Transform>();       // List of objs that represent locations that are valid spawn points
        private List<int> spawnPosIndex = new List<int>();            // List of unique indexes that correspond to indexes in spawnPos

        // Setting up private variables
        private void Start()
        {
            timeToSpawn = Random.Range(minTimeToSpawn, maxTimeToSpawn);
            currTimePassed = 0f;
        }

        // Logic to get the spawned objects spawned
        private void FixedUpdate()
        {
            currTimePassed += Time.fixedDeltaTime;
            if (currTimePassed >= timeToSpawn)
            {
                GetNewSpawnPos();
                GetRandomChildIndexs();
                SpawnObjects();
                
                timeToSpawn = Random.Range(minTimeToSpawn, maxTimeToSpawn);
                currTimePassed = 0f;
            }
        }

        // Dynamically grabs the valid spawn points of the level and fills in the spawnPos
        private void GetNewSpawnPos()
        {
            spawnPos.Clear();
            GameObject lastTerrain = cleanupObj.GetLastTerrain();
            for (int rootChildIndex = 0; rootChildIndex < lastTerrain.transform.childCount; ++rootChildIndex)
            {
                Transform currRootChild = lastTerrain.transform.GetChild(rootChildIndex);
                for (int subChildIndex = 0; subChildIndex < currRootChild.childCount; ++subChildIndex)
                {
                    spawnPos.Add(currRootChild.GetChild(subChildIndex).GetChild(0));
                }
            }
        }

        // We fill in spawnPosIndex with indexes that correspond to spawnPos
        private void GetRandomChildIndexs()
        {
            int spawnCount = Random.Range(minSpawnCount, maxSpawnCount);
            spawnPosIndex.Clear();
            while (spawnCount > 0)
            {
                int ranIndex = Random.Range(0, spawnPos.Count);
                while (spawnPosIndex.Contains(ranIndex))
                {
                    ranIndex = Random.Range(0, spawnPos.Count);
                }
                spawnPosIndex.Add(ranIndex);
                --spawnCount;
            }
        }

        // We spawn objects at their corresponding locations that we found earlier
        private void SpawnObjects()
        {
            // We first select a random order in the list
            GameObject ranPattern = spawnObjPrefabs[(int)Random.Range(0, spawnObjPrefabs.Length)];
            for (int index = 0; index < ranPattern.transform.childCount; index++)
            {
                // We then take each child of the ran object (which contains patterns) and place it in a corresponding location
                // according to the spawn list here
                Transform selectedWayPoint = spawnPos[index];
                GameObject.Instantiate(ranPattern.transform.GetChild(index), selectedWayPoint.position, selectedWayPoint.rotation, selectedWayPoint);
            }
        }

        // Called externally, this increases the difficulty by changing the spawn rates
        public void IncreaseSpawnFrequency()
        {
            minTimeToSpawn = Mathf.Clamp(minTimeToSpawn - modCounter, 1f, minTimeToSpawn);
            maxTimeToSpawn = Mathf.Clamp(maxTimeToSpawn - modCounter, 2f, maxTimeToSpawn);
        }
    }
}