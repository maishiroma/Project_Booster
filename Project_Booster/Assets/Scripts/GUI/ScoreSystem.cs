using UnityEngine;
using TMPro;
using Gimmick;
using System.Collections;

namespace ScreenGUI
{
    public class ScoreSystem : MonoBehaviour
    {
        [Header("External Refs")]
        [Tooltip("Ref to the external spawner obj")]
        public Spawner spawnerObj;

        [Header("GUI Variables")]
        [Tooltip("Text Display for the score")]
        public TextMeshProUGUI scoreText;
        [Tooltip("Text Display for the current combo the player is on")]
        public TextMeshProUGUI scoreComboText;
        [Tooltip("Text Display for the final score count")]
        public TextMeshProUGUI finalScoreText;

        [Header("General Vars")]
        [Tooltip("How much time needs to pass before the combo is reset?")]
        public float comboDurationTimer;
        [Tooltip("How many combo strings does it take for a combo to start?")]
        public int comboStart;
        [Tooltip("The amount that the score needs to increase from the currSpawnIncreaseCap to increase spawn rate")]
        public int spawnIncreaseCap;

        // Private Variables
        private float scoreCounter;         // The actual score counter
        private int scoreComboAmount;       // How much of a combo counter is on
        private float currComboDuration;    // The current duration of the current combo
        private int currSpawnIncreaseCap;   // The current score to "beat" in order to increase the difficulty

        // Sets up the private variables
        private void Start()
        {
            scoreCounter = 0;
            currSpawnIncreaseCap = spawnIncreaseCap;
            ResetComboTime();
        }

        // Handles the logic of tallying the score
        private void Update()
        {
            if (scoreComboAmount > 0)
            {
                scoreCounter += Time.deltaTime * scoreComboAmount;
                currComboDuration += Time.deltaTime;

                if (comboDurationTimer - currComboDuration < 1f && !IsInvoking("FlashText"))
                {
                    // Warning to let player know the combo is about to end
                    StartCoroutine("FlashText");
                }
                if (currComboDuration >= comboDurationTimer)
                {
                    // Once the combo is over, the combo timer is reset
                    ResetComboTime();
                }

                // Once the score hits a specific cap, we increase the difficulty
                if (scoreCounter > currSpawnIncreaseCap)
                {
                    currSpawnIncreaseCap += spawnIncreaseCap;
                    spawnerObj.IncreaseSpawnFrequency();
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

        // Coroutine that flashes the combo meter to the player
        private IEnumerator FlashText()
        {
            Color transparentColor = new Color(scoreComboText.color.r, scoreComboText.color.g, scoreComboText.color.b, 0.3f);
            Color currColor = scoreComboText.color;
            yield return new WaitForFixedUpdate();

            while (comboDurationTimer - currComboDuration > 0f)
            {
                scoreComboText.color = transparentColor;
                yield return new WaitForEndOfFrame();
                scoreComboText.color = currColor;
                yield return new WaitForEndOfFrame();
            }

            scoreComboText.color = currColor;
            yield return null;
        }

        // When called, we increment the combo meter
        public void InvokeComboTime()
        {
            if (currComboDuration < comboDurationTimer)
            {
                currComboDuration = 0f;
                scoreComboAmount += 1;
                StopCoroutine("FlashText");
                scoreComboText.color = new Color(scoreComboText.color.r, scoreComboText.color.g, scoreComboText.color.b, 1f);
            }
        }

        // When called, we reset the combo stats
        public void ResetComboTime()
        {
            currComboDuration = 0f;
            scoreComboAmount = 0;
            scoreComboText.color = new Color(scoreComboText.color.r, scoreComboText.color.g, scoreComboText.color.b, 1f);
        }

        // When called, we set up the final score counter up
        public void SetFinalScore()
        {
            finalScoreText.text = "Final Score: " + (int)scoreCounter;
        }
    }
}