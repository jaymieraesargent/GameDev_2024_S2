using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [AddComponentMenu("GameDev/Player/Health")]
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 100, currentHealth, regenValue;
        [SerializeField] Image displayImage;
        [SerializeField] Gradient gradientHealth;
        [SerializeField] Transform spawnPoint;
        private float timerValue;
        private bool canHeal = true;

        public void DamagePlayer(float damageValue)
        {
            timerValue = 0;
            canHeal = false;
            currentHealth -= damageValue;
            UpdateUI();
        }
        void UpdateUI()
        {
            displayImage.fillAmount = Mathf.Clamp01(currentHealth / maxHealth);
            displayImage.color = gradientHealth.Evaluate(displayImage.fillAmount);
        }
        void Respawn()
        {
            if (currentHealth <= 0)
            {
                //turn off CharacterController
                GetComponent<CharacterController>().enabled = false;
                //run respawn code
                //move the player
                this.transform.position = spawnPoint.position;
                this.transform.rotation = spawnPoint.rotation;
                //fix health
                currentHealth = maxHealth;
                //fix display of health
                UpdateUI();
                //turn on CharacterController
                GetComponent<CharacterController>().enabled = true;


            }
        }
        void HealOverTime()
        {
            if (canHeal)
            {
                if (currentHealth < maxHealth && currentHealth > 0)
                {
                    //current health to increase by a value over time 
                    currentHealth += regenValue * Time.deltaTime;
                    UpdateUI();
                }
            }           
        }
        void Timer()
        {
            if (!canHeal)
            {
                timerValue += Time.deltaTime;
                if (timerValue >= 1.5f)
                {
                    //allow healing
                    canHeal = true;
                    //reset timer
                    timerValue = 0;
                }
            }           
        }
        #region Unity Event Functiona
        private void Start()
        {
            currentHealth = maxHealth;
            displayImage.fillAmount = 1;
            UpdateUI();
        }
        private void Update()
        {
            HealOverTime();
            Respawn();
            Timer();
        }
        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.CompareTag("Damage"))
            {
                //Do a Thing!!
                Debug.Log("Hit Steve");
                DamagePlayer(10);
            }
        }
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            regenValue *= 2;
        }
        private void OnTriggerExit(Collider other)
        {
            regenValue /= 2;
        }
    }

    
}


