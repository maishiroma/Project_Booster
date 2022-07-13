using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visuals
{
    public class MoveTerrain : MonoBehaviour
    {
        [Header("Movement Variables")]
        
        [Range(0.1f, 100f)]
        [Tooltip("How fast does the parallax move?")]
        public float moveSpeed;
        
        [Tooltip("What is the move modifier for the plane group?")]
        public Vector3 moveTranslation;

        [Tooltip("What is the disitance between each plane group?")]
        public Vector3 addPlacement;

        // Private Variable
        private LinkedList<GameObject> pairedTerrainLL = new LinkedList<GameObject>();      // List of all plane groups

        // Sets up variables and the Linked List
        private void Start()
        {
            for (int i = 0; i < gameObject.transform.childCount; ++i)
            {
                pairedTerrainLL.AddLast(gameObject.transform.GetChild(i).gameObject);
            }
        }

        private void FixedUpdate()
        {
            // Translates the ground group
            gameObject.transform.position += moveTranslation * moveSpeed;
        }

        // Method to move the firstmost terrain piece to the back of the linked list
        // Used to simulate parallax
        public void MoveFirstMostTerrain()
        {
            LinkedListNode<GameObject> firstObj = pairedTerrainLL.First;
            LinkedListNode<GameObject> lastObj = pairedTerrainLL.Last;

            pairedTerrainLL.RemoveFirst();
            pairedTerrainLL.AddAfter(lastObj, firstObj);

            firstObj.Value.transform.position = lastObj.Value.transform.position + addPlacement;

            Debug.Log("Moving Group" + firstObj.Value.gameObject.name + " to back.");
        }
    }
}