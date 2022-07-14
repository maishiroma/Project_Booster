using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visuals;

namespace Gimmick
{
    public class Spawner : MonoBehaviour
    {
        [Header("External References")]
        [Tooltip("Reference to the prefab that holds information on the Boost Panel")]
        public BoostPanel boostPanelPrefab;
        [Tooltip("Reference to an Empty GameObject that will hold all of the boost panels")]
        public GameObject boostPanelParent;

        [Header("Spawn Variables")]
        [Tooltip("Translation modifier that will be applied to the spawned object")]
        public Vector3 spawnTransform;
        [Tooltip("Min time it take for an object to spawn")]
        public float minTimeToSpawn;
        [Tooltip("Max time it takes for an object to spawn")]
        public float maxTimeToSpawn;
        [Tooltip("Min numb of objs that would be spawned")]
        public int minSpawnCount;
        [Tooltip("Max numb of objs that would be spawned")]
        public int maxSpawnCount;

        // Private Variables
        private float timeToSpawn;      // How long does it take for another object to spawn?
        private float currTimePassed;   // How much time has passed since the last object spawned?

        private List<int> childIndexTaken = new List<int>();            // List of indexes that will be used as spawn points

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
                GetRandomChildIndexs();
                SpawnObjects();
                timeToSpawn = Random.Range(minTimeToSpawn, maxTimeToSpawn);
                currTimePassed = 0f;
            }
        }

        // Helper method to spawn a new object
        private void SpawnObjects()
        {
            // TODO: Once I get the hazard in, we need to make some logic to randomly choose between hazards and boost panels
            foreach (int currChildIndex in childIndexTaken)
            {
                Transform selectedWayPoint = gameObject.transform.GetChild(currChildIndex);
                Vector3 spawnLoc = selectedWayPoint.position + spawnTransform;
                BoostPanel newObj = GameObject.Instantiate(boostPanelPrefab, spawnLoc, selectedWayPoint.rotation, boostPanelParent.transform);
            }
        }

        // When called, clears the previous index list and prepares a new one, with unique index values
        private void GetRandomChildIndexs()
        {
            int spawnCount = Random.Range(minSpawnCount, maxSpawnCount);
            childIndexTaken.Clear();
            while(spawnCount > 0)
            {
                int ranIndex = Random.Range(0, gameObject.transform.childCount);
                while (childIndexTaken.Contains(ranIndex))
                {
                    ranIndex = Random.Range(0, gameObject.transform.childCount);
                }
                childIndexTaken.Add(ranIndex);
                --spawnCount;
            }

        }
    }

}