using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public enum MoveState
    {
        MOVING,
        STOPPING,
        STOPPED,
    }
    
    public class PlayerMovement : MonoBehaviour
    {
        public List<Transform> pathNodes = new List<Transform>();
        public int currNodeIndex = 0;
        public float maxAcceleration;

        private MoveState currMoveState;
        private int moveInput;
        private GameControls gameControls;
        private Rigidbody2D rb2d;
        private float acceleration;

        public float moveSpeed;
        public float stopSpeed;

        // Activates all of the controls for the player
        private void Awake()
        {
            // We need to first set this to be a new object before we can do anything
            gameControls = new GameControls();

            // Then we can set up calllbacks to specific methods that we want the controls to listen to
            gameControls.Player.HorizontalMovement.performed += ctx => Move(ctx);
            gameControls.Player.HorizontalMovement.canceled += ctx => Move(ctx);

            rb2d = GetComponent<Rigidbody2D>();
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

        private void Start()
        {
            rb2d.position = pathNodes[currNodeIndex].position;
        }

        private void FixedUpdate()
        {
            if (moveInput != 0f)
            {
                acceleration += Time.fixedDeltaTime * moveSpeed;
                acceleration = Mathf.Clamp(acceleration, 0f, maxAcceleration);
            }
            else
            {
                acceleration = Mathf.Clamp(Mathf.Lerp(acceleration, 0.1f, stopSpeed), 0f, maxAcceleration);
            }

            if (Vector2.Distance(rb2d.position, GetNodeInList(currNodeIndex).position) < 0.1f)
            {
                currNodeIndex = GetProperNodeIndex(currNodeIndex + moveInput);
                if (Mathf.Approximately(moveInput, 0f))
                {
                    acceleration = 0f;
                    currMoveState = MoveState.STOPPED;
                }
            }
            rb2d.position = Vector2.MoveTowards(rb2d.position, GetNodeInList(currNodeIndex).position, acceleration);
        }

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

        private void Move(InputAction.CallbackContext ctx)
        { 
            if (ctx.ReadValue<float>() < 0)
            {
                moveInput = 1;
                currMoveState = MoveState.MOVING;
            }
            else if (ctx.ReadValue<float>() > 0)
            {
                moveInput = -1;
                currMoveState = MoveState.MOVING;
            }
            else
            {
                moveInput = 0;
                currMoveState = MoveState.STOPPING;
            }
        }
    }
}
