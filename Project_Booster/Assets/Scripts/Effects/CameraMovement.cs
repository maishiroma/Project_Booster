using UnityEngine;

namespace Effects
{

    public enum CameraDirection
    {
        FRONT,
        LEFT,
        RIGHT
    }

    public class CameraMovement : MonoBehaviour
    {
        [Header("Boost Effect Variables")]
        [Tooltip("The new Field of View to place the user at when boosting")]
        public float boostFOV;
        [Tooltip("How fast does the shift from the original FOV to the new FOV?")]
        public float transitionAccceleration;
        [Tooltip("The duration of the boost effect")]
        public float maxTimeBoostEffect;
        public float turnSpeed;
        public float turnFreq;

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
        private float timeToTurn;
        private CameraDirection currDirection;

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
            currDirection = CameraDirection.FRONT;

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
                    gameObject.transform.position = origCameraPos;
                }
            }
            else
            {  
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

            timeToTurn += Time.deltaTime;
            if (timeToTurn > turnFreq)
            {
                RandomTurn();
                timeToTurn = 0f;
            }
            else
            {
                CameraMockTurning();
            }
        }

        private void RandomTurn()
        {
            int ranNumb = Random.Range(0, 3);
            switch(ranNumb)
            {
                case 0:
                    currDirection = CameraDirection.FRONT;
                    break;
                case 1:
                    currDirection = CameraDirection.LEFT;
                    break;
                case 2:
                    currDirection = CameraDirection.RIGHT;
                    break;
                default:
                    currDirection = CameraDirection.FRONT;
                    break;
            }
        }

        private void CameraMockTurning()
        {
            Quaternion newDir = Quaternion.identity;
            switch(currDirection)
            {
                case CameraDirection.FRONT:
                    newDir = Quaternion.Euler(0f, 0f, 0f);
                    break;
                case CameraDirection.LEFT:
                    newDir = Quaternion.Euler(0f, 15f, 0f);
                    break;
                case CameraDirection.RIGHT:
                    newDir = Quaternion.Euler(0f, -15f, 0f);
                    break;
                default:
                    newDir = Quaternion.Euler(0f, 0f, 0f);
                    break;
            }
            gameObject.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, newDir, Time.fixedDeltaTime * turnSpeed);
        }
    }
}