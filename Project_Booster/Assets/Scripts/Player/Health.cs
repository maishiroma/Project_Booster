using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Health : MonoBehaviour
    {
        public Slider healthBar;
        public Image healthBarForeground;
        public GameObject gameOverVisuals;

        public Color fullHealth;
        public Color halfHealth;
        public Color lowHealth;

        // Private Variables
        private float currHealth;
        private bool isDead;

        public float CurrHealth
        {
            get { return currHealth; }
            set
            {
                if (value < 0f)
                {
                    currHealth = 0f;
                }
                else if (value > 1f)
                {
                    currHealth = 1f;
                }
                else
                {
                    currHealth = value;
                }
            }
        }

        public bool IsDead
        {
            get
            {
                if (currHealth <= 0f) { 
                    return true; 
                }
                return false;
            }
        }

        private void Start()
        {
            isDead = false;
            currHealth = 1f;
            healthBar.value = currHealth;
            healthBarForeground.color = fullHealth;
            gameOverVisuals.SetActive(false);
        }

        private void Update()
        {
            if (!isDead)
            {
                if (currHealth <= 0f)
                {
                    gameOverVisuals.SetActive(true);
                    isDead = true;
                }
            }
        }

        private void OnGUI()
        {
            healthBar.value = currHealth;
            if (currHealth >= 0.75f)
            {
                healthBarForeground.color = fullHealth;
            }
            else if (currHealth >= 0.5f)
            {
                healthBarForeground.color = halfHealth;
            }
            else if (currHealth >= 0.25f)
            {
                healthBarForeground.color = lowHealth;
            }
            else if (currHealth <= 0f)
            {
                healthBarForeground.color = new Color(lowHealth.r, lowHealth.g, lowHealth.b, 0f);
            }
        }
    }

}