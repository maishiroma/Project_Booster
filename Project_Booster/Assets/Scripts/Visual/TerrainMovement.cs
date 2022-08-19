using UnityEngine;

namespace Visuals
{
    public class TerrainMovement : MonoBehaviour
    {
        [Header("Movement Variables")]
        [Tooltip("The maximum speed the terrain can go to")]
        public float maxMoveSpeed;

        [Tooltip("The min speed the terrain can go to")]
        public float minMoveSpeed;

        [SerializeField]
        private float currMoveSpeed;                                                        // The current speed the terrain is moving

        // Getter/Setter for the TerrainMovement
        public float CurrMoveSpeed
        {
            get { return currMoveSpeed; }
            set
            {
                if (value >= maxMoveSpeed)
                {
                    currMoveSpeed = maxMoveSpeed;
                }
                else if (value < minMoveSpeed)
                {
                    currMoveSpeed = minMoveSpeed;
                }
                else
                {
                    currMoveSpeed = value;
                }
            }
        }

        // Translates the ground group
        private void FixedUpdate()
        {
            gameObject.transform.position += new Vector3(0,0,-1) * currMoveSpeed;
        }
    }
}