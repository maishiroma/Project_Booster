using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ScreenGUI
{
    public class ScoreSystem : MonoBehaviour
    {
        [Header("GUI Variables")]
        [Tooltip("Text Display for the score")]
        public TextMeshProUGUI scoreText;
        [Tooltip("Text Display for the current combo the player is on")]
        public TextMeshProUGUI scoreComboText;

        public TextMeshProUGUI finalScoreText;

        [Tooltip("How much time needs to pass before the combo is reset?")]
        public float comboDurationTimer;
        [Tooltip("How many combo strings does it take for a combo to start?")]
        public int comboStart;

        // Private Variables
        private float scoreCounter;         // The actual score counter
        private int scoreComboAmount;       // How much of a combo counter is on
        private float currComboDuration;    // The current duration of the current combo

        // Sets up the private variables
        private void Start()
        {
            scoreCounter = 0;
            ResetComboTime();
        }

        // Handles the logic of tallying the score
        private void Update()
        {
            if (scoreComboAmount > 0)
            {
                scoreCounter += Time.deltaTime * scoreComboAmount;
                currComboDuration += Time.deltaTime;

                if (comboDurationTimer - currComboDuration < 1f)
                {
                    InvokeRepeating("EnableText", 0.1f, 0.5f);
                }
                if (currComboDuration >= comboDurationTimer)
                {
                    ResetComboTime();
                }
            }
            
        }

        // Updates to GUI
        private void OnGUI()
        {
            scoreText.text = "Score: " + (int)scoreCounter;
            if (scoreComboAmount > comboStart)
            {
                scoreComboText.text = "Combo! x" + scoreComboAmount;
            }
            else
            {
                scoreComboText.text = "";
            }
        }

        private void EnableText()
        {
            scoreComboText.enabled = !scoreComboText.enabled;
        }

        // When called, we increment the combo meter
        public void InvokeComboTime()
        {
            if (currComboDuration < comboDurationTimer)
            {
                CancelInvoke("EnableText");
                currComboDuration = 0f;
                scoreComboAmount += 1;
                scoreComboText.enabled = true;
            }
        }

        // When called, we reset the combo stats
        public void ResetComboTime()
        {
            CancelInvoke("EnableText");
            currComboDuration = 0f;
            scoreComboAmount = 0;
            scoreComboText.enabled = true;
        }

        public void SetFinalScore()
        {
            finalScoreText.text = "Final Score: " + (int)scoreCounter;
        }
    }
}