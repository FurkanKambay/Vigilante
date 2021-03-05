using UnityEngine;

namespace Vigilante
{
    public class EntityHealth : MonoBehaviour
    {
        public bool IsPlayer;
        public float InitialHealth = 100f;
        public float RegenerationRate;

        public float Health => health;
        public bool IsAlive => health > 0f;

        private float health;

        private void OnEnable() => health = InitialHealth;

        private void FixedUpdate()
        {
            if (IsAlive)
                RegenerateHealth(Time.fixedDeltaTime);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Damaging"))
            {
                Arrow arrow = other.gameObject.GetComponent<Arrow>();
                if (IsPlayer && arrow.IsPlayerOwned) return;

                TakeDamage(Arrow.DamageValue);
            }
        }

        private void TakeDamage(float amount)
        {
            health = Mathf.Max(0f, health - amount);

            if (health == 0f)
                gameObject.SetActive(false);
        }

        private void RegenerateHealth(float deltaTime)
            => health = Mathf.Min(100f, health + RegenerationRate * deltaTime);
    }
}
