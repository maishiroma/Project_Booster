using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Boost Effect Variables")]
        [Tooltip("The new Field of View to place the user at when boosting")]
        public float boostFOV;
        [Tooltip("How fast does the shift from the original FOV to the new FOV?")]
        public float transitionAccceleration;
        [Tooltip("The duration of the boost effect")]
        public float maxTimeBoostEffect;

        [Header("Camera Shaking Variables")]
        [Range(0.01f, 1f)]
        [Tooltip("How strong is the camera shaking effect?")]
        public float shakeFrequency;

        // Private Variables
        private Camera mainCamera;      // Ref to the main camera component
        private Vector3 origCameraPos;  // The original camera position
        private bool isBoosting;        // Is the camera in a boost state?
        private float origFOV;          // Ref to the original FOV
        private float timeBoostEffect;  // The amount of time that the player is in a boost state

        // Getter/Setter for IsBoosting
        public bool IsBoosting
        {
            get { return isBoosting; }
            set
            {
                if (value == true && isBoosting == true)
                {
                    // If we are boosting again, we reset the timer for boosting
                    timeBoostEffect = 0f;
                }
                else if (value == false)
                {
                    // If we for some reason are not boosting anymore, we stop the time boost effet
                    timeBoostEffect = 0f;
                }
                isBoosting = value;
            }
        }

        // We set up some variables for the camera
        private void Start()
        {
            mainCamera = GetComponent<Camera>();
            origFOV = mainCamera.fieldOfView;
            isBoosting = false;

            origCameraPos = gameObject.transform.position;
        }

        // Handles Camera Shaking and FOV changing
        private void FixedUpdate()
        {
            if(isBoosting)
            {
                // FOV Expanding
                mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, boostFOV, timeBoostEffect * transitionAccceleration);
                
                // Camera Shaking Effect
                gameObject.transform.position = origCameraPos + Random.insideUnitSphere * shakeFrequency;
                if (timeBoostEffect < maxTimeBoostEffect)
                {
                    timeBoostEffect += Time.fixedDeltaTime;
                }
                else
                {
                    isBoosting = false;
                    timeBoostEffect = 0f;
                }
            }
            else
            {
                // Moving the camera back to start
                if (gameObject.transform.position != origCameraPos)
                {
                    gameObject.transform.position = origCameraPos;
                }
                
                // FOV Moving back to start
                if (!Mathf.Approximately(mainCamera.fieldOfView, origFOV))
                {
                    mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, origFOV, Time.fixedDeltaTime * transitionAccceleration);
                }
                else
                {
                    mainCamera.fieldOfView = origFOV;
                }
            }
        }
    }
}