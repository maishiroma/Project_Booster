using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenGUI
{
    public class Health : MonoBehaviour
    {
        [Header("General Settings")]
        [Tooltip("How many seconds does this entity have invulnerability?")]
        public float timeForInvincibility;
        [Tooltip("How dark is the filter?")]
        public float invincibilityVisualMaxAlpha;
        [Tooltip("How fast is the fade back to normal?")]
        public float lerpToMaxAlpha;

        [Header("GUI References")]
        [Tooltip("The image that is used to represent the player's invincibility")]
        public Image invincibilityVisual;
        [Tooltip("GameObject component referencing the player healthbar")]
        public Slider healthBar;
        [Tooltip("GameObject component referencing the health foreground")]
        public Image healthBarForeground;
        [Tooltip("GameObject Parent that encompases the GameOver GUI")]
        public GameObject gameOverVisuals;

        [Header("Health Bar Colors")]
        [Tooltip("Color that is used when health is above 0.6f")]
        public Color fullHealth;
        [Tooltip("Color that is used when health is above 0.3f, but less than 0.6f")]
        public Color halfHealth;
        [Tooltip("Color that is used when health is less than 0.3f")]
        public Color lowHealth;

        // Private Variables
        private float currHealth;       // How much health does the player currently have?
        private bool isDead;            // A shortcut property to determine if the player is dead
        private bool isInvincible;      // Is the player currently invinvible to hazards?

        // Getter/Setter
        public float CurrHealth
        {
            get { return currHealth; }
            set
            {
                if (value < 0f)
                {
                    currHealth = 0f;
                    isInvincible = true;
                    invincibilityVisual.color = new Color(invincibilityVisual.color.r, invincibilityVisual.color.g, invincibilityVisual.color.b, invincibilityVisualMaxAlpha);
                    StartCoroutine("IFrameTime");
                }
                else if (value > 1f)
                {
                    currHealth = 1f;
                }
                else
                {
                    currHealth = value;
                    isInvincible = true;
                    invincibilityVisual.color = new Color(invincibilityVisual.color.r, invincibilityVisual.color.g, invincibilityVisual.color.b, invincibilityVisualMaxAlpha);
                    StartCoroutine("IFrameTime");
                }
            }
        }

        // Getter that determines if the player is dead or not
        public bool IsDead
        {
            get
            {
                // If the player is already dead, we say the player is dead
                if (isDead == true) { return true; }
                
                // Else, we do a health check
                if (currHealth <= 0f) { 
                    return true; 
                }
                return false;
            }
        }

        // Getter that returns the player's invincible state
        public bool IsInvincible
        {
            get { return isInvincible; }
        }

        // Sets up the GameObject with the proper visuals
        private void Start()
        {
            isDead = false;
            isInvincible = false;
            currHealth = 1f;
            
            healthBar.value = currHealth;
            healthBarForeground.color = fullHealth;
            
            invincibilityVisual.color = new Color(invincibilityVisual.color.r, invincibilityVisual.color.g, invincibilityVisual.color.b, 0f);
            
            gameOverVisuals.SetActive(false);
        }

        // Checks the player's status and updates the GUI accordingly
        private void Update()
        {
            if (IsDead)
            {
                // If the check to see if the player is dead returns true, we set
                // the game over visuals and then set isDead to true
                gameOverVisuals.SetActive(true);
                isDead = true;
                isInvincible = true;
            }
        }

        // We update the health GUI based on the current value of the health
        private void OnGUI()
        {
            healthBar.value = currHealth;
            if (currHealth <= 0.33f && currHealth > 0f)
            {
                healthBarForeground.color = lowHealth;
            }
            else if (currHealth >= 0.33f && currHealth <= 0.66f)
            {
                healthBarForeground.color = halfHealth;
            }
            else if (currHealth >= 0.66f)
            {
                healthBarForeground.color = fullHealth;
            }
            else
            {
                healthBarForeground.color = new Color(lowHealth.r, lowHealth.g, lowHealth.b, 0f);
            }
        }

        private void FixedUpdate()
        {
            // This logic updates the material on the visual for invicnciblity
            if (isInvincible)
            {
                float newAlpha = Mathf.Lerp(invincibilityVisual.color.a, 0f, lerpToMaxAlpha);
                invincibilityVisual.color = new Color(invincibilityVisual.color.r, invincibilityVisual.color.g, invincibilityVisual.color.b, newAlpha);
            }
        }

        // Sets the duration of the player's invincibility when they get hit
        private IEnumerator IFrameTime()
        {
            if (IsInvincible)
            {
                yield return new WaitForSeconds(timeForInvincibility);
                invincibilityVisual.color = new Color(invincibilityVisual.color.r, invincibilityVisual.color.g, invincibilityVisual.color.b, 0f);
                isInvincible = false;
            }
            yield return null;
        }
    }

}