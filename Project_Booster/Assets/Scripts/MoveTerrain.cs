using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visuals
{
    public class MoveTerrain : MonoBehaviour
    {
        [Header("Movement Variables")]
        [Tooltip("The maximum speed the terrain can go to")]
        public float maxMoveSpeed;

        [Tooltip("What is the move modifier for the plane group?")]
        public Vector3 moveTranslation;

        [Tooltip("What is the disitance between each plane group?")]
        public Vector3 addPlacement;

        // Private Variables
        private LinkedList<GameObject> pairedTerrainLL = new LinkedList<GameObject>();      // List of all plane groups
        private float originalSpeed;                                                        // The original move speed of the terrain
        
        [SerializeField]
        private float currMoveSpeed;                                                        // The current speed the terrain is moving

        public float CurrMoveSpeed
        {
            get { return currMoveSpeed; }
            set
            {
                if (value >= maxMoveSpeed)
                {
                    currMoveSpeed = maxMoveSpeed;
                }
                else if (value < 0)
                {
                    currMoveSpeed = 0f;
                }
                else
                {
                    currMoveSpeed = value;
                }
            }
        }

        // Sets up variables and the Linked List
        private void Start()
        {
            for (int i = 0; i < gameObject.transform.childCount; ++i)
            {
                pairedTerrainLL.AddLast(gameObject.transform.GetChild(i).gameObject);
            }
            originalSpeed = currMoveSpeed;
        }

        private void FixedUpdate()
        {
            // Translates the ground group
            gameObject.transform.position += moveTranslation * currMoveSpeed;
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