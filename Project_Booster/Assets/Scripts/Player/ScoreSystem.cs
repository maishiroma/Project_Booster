using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;
using TMPro;

namespace Player
{
    public class ScoreSystem : MonoBehaviour
    {
        // A Static Class Instance to reference this object where it needs to be in
        public static ScoreSystem Instance;

        public TextMeshProUGUI scoreText;
        public string mainCameraTag;

        // Private Variables
        private CameraMovement mainCamera;
        private float scoreCounter;

        public int ScoreCounter
        {
            get { return (int)scoreCounter; }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            mainCamera = GameObject.FindGameObjectWithTag(mainCameraTag).GetComponent<CameraMovement>();
            scoreCounter = 0;
        }

        private void Update()
        {
            scoreCounter += Time.deltaTime * GetRate();
        }

        private void OnGUI()
        {
            scoreText.text = "Score: " + ScoreCounter;
        }

        private int GetRate()
        {
            if (mainCamera.IsBoosting)
            {
                return 2;
            }
            return 1;
        }

    }

}