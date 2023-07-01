using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Game.playerScripts
{
    public class PlayerHealth : NetworkBehaviour
    {
        [NonSerialized] public readonly UnityEvent playerDeath = new();

        [SyncVar]
        [SerializeField] private int health;

        private int maxHealth;

        private void Awake() => maxHealth = health;

        public void MakeDamage(int damage)
        {
            health -= damage;
            health = Mathf.Clamp(health, 0, maxHealth);

            if (health <= 0)
            {
                playerDeath.Invoke();
            }
        }
    }
}