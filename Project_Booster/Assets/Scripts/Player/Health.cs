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

        public Color fullHealth;
        public Color halfHealth;
        public Color lowHealth;

        // Private Variables
        private float currHealth;

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

        private void Start()
        {
            currHealth = 1f;
            healthBar.value = currHealth;
            healthBarForeground.color = fullHealth;
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