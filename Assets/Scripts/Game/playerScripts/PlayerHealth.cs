using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Game.playerScripts
{
    public class PlayerHealth : NetworkBehaviour
    {
        [NonSerialized] public readonly UnityEvent playerDeath = new();
        [NonSerialized] public readonly UnityEvent<int> playerTakeDamage = new();

        [SyncVar]
        [SerializeField] private int health;

        private int maxHealth;
        public int Health => health;

        private void Awake() => maxHealth = health;

        public void MakeDamage(int damage)
        {
            health -= damage;
            health = Mathf.Clamp(health, 0, maxHealth);
            
            playerTakeDamage.Invoke(damage);

            if (health <= 0)
            {
                playerDeath.Invoke();
            }
        }
    }
}