using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("External Refs")]
        [Tooltip("References to external objects that are required for this script")]
        public List<Transform> pathNodes = new List<Transform>();       // List of GameObjects that act as WayPoints for the object
        
        [Header("Movement Vars")]
        [Tooltip("Values to change the player movement speed and how it works")]
        
        [Range(0.001f, 10f)]
        public float accelerationSpeed;                                         // How fast does this object accelerate?
        [Range(0.001f, 10f)]
        public float stopSpeed;                                         // How fast does this object stop?
        [Range(0.001f, 10f)]
        public float maxAcceleration;                                   // The maximum accceleration that this object can go
        [Range(0, 20)]
        public int currNodeIndex = 0;                                   // Index spot that the player is currently on.

        // Private Variables
        private int moveInput;                                          // The current Movement Input that the script detects
        private float currAcceleration;                                 // The current Acceleration the player is at
        
        private GameControls gameControls;                              // Ref to external control system to associate to this script
        private Rigidbody rb2d;                                       // Ref to the RB that dictates the movement for this object
        
        // Activates all of the controls for the player
        private void Awake()
        {
            // We need to first set this to be a new object before we can do anything
            gameControls = new GameControls();

            // Then we can set up calllbacks to specific methods that we want the controls to listen to
            gameControls.Player.HorizontalMovement.performed += ctx => Move(ctx);
            gameControls.Player.HorizontalMovement.canceled += ctx => Move(ctx);

            rb2d = GetComponent<Rigidbody>();
        }

        // Enables the controls when the player is active
        private void OnEnable()
        {
            gameControls.Enable();
        }

        // Diables the controls when the player is not active
        private void OnDisable()
        {
            gameControls.Disable();
        }

        // Sets up the object and its start position
        private void Start()
        {
            // We use the currNodeIndex as the starting location
            rb2d.position = pathNodes[currNodeIndex].position;
        }

        // This handles all movement logic for the player
        private void FixedUpdate()
        {
            // This if statemennt controls the current acceleration for the object based if the player is moving
            if (moveInput != 0f)
            {
                currAcceleration += Time.fixedDeltaTime * accelerationSpeed;
                currAcceleration = Mathf.Clamp(currAcceleration, 0f, maxAcceleration);
            }
            else
            {
                currAcceleration = Mathf.Clamp(Mathf.Lerp(currAcceleration, 0.1f, stopSpeed), 0f, maxAcceleration);
            }

            // We check the distance the player is to a waypoint and if we meet it, we change the index to be the next one
            if (Vector2.Distance(rb2d.position, GetNodeInList(currNodeIndex).position) < 0.1f)
            {
                currNodeIndex = GetProperNodeIndex(currNodeIndex + moveInput);
                if (Mathf.Approximately(moveInput, 0f))
                {
                    currAcceleration = 0f;
                }
            }

            // We then update the movement of the player using the information that we got here
            rb2d.position = Vector2.MoveTowards(rb2d.position, GetNodeInList(currNodeIndex).position, currAcceleration);
        }

        // Move context that reads in the input from the player
        private void Move(InputAction.CallbackContext ctx)
        {
            if (ctx.ReadValue<float>() < 0)
            {
                moveInput = 1;
            }
            else if (ctx.ReadValue<float>() > 0)
            {
                moveInput = -1;
            }
            else
            {
                moveInput = 0;
            }
        }

        // A helper method to get a valid Node object in the NodeList
        private Transform GetNodeInList(int index)
        {
            if (index < 0)
            {
                return pathNodes[pathNodes.Count - 1];
            }
            else if (index >= pathNodes.Count)
            {
                return pathNodes[0];
            }
            else
            {
                return pathNodes[index];
            }
        }

        // A Helper method to get a valid index that is valid in the list
        private int GetProperNodeIndex(int index)
        {
            if (index < 0)
            {
                return pathNodes.Count - 1;
            }
            else if (index >= pathNodes.Count)
            {
                return 0;
            }
            else
            {
                return index;
            }
        }
    }
}
