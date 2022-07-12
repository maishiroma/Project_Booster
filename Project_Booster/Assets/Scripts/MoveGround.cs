using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visuals
{
    public class MoveGround : MonoBehaviour
    {
        public float moveSpeed;
        public Vector3 moveTranslation;
        public Vector3 addPlacement;
        
        //TODO: Queue first object, move it to end of last item's position and requeue
        private List<GameObject> pairedTerrainQueue = new List<GameObject>();

        private void Start()
        {
            for (int i = 0; i < gameObject.transform.childCount; ++i)
            {
                pairedTerrainQueue.Add(gameObject.transform.GetChild(i).gameObject);
            }
        }

        private void FixedUpdate()
        {
            gameObject.transform.position += moveTranslation * moveSpeed;
        }
    }
}