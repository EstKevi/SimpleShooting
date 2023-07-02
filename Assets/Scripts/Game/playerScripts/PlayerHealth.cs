using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Game.playerScripts
{
    public class PlayerHealth : NetworkBehaviour
    {
        [SerializeField] private Player player;
        
        [NonSerialized] public readonly UnityEvent playerDeath = new();

        [SyncVar]
        [SerializeField] private int health;

        private int maxHealth;
        public int Health => health;

        private void Awake() => maxHealth = health;

        [ClientRpc]
        public void RpcMakeDamage(int damage)
        {
            if (isServer)
            {
                player.RpcChangeHealthUi(damage);
            }
            
            if (!isLocalPlayer) return;
            var heal = health;
            heal -= damage;
            heal = Mathf.Clamp(heal, 0, maxHealth);

            health = heal;
            CmdSetHealth(heal);

            if (health <= 0)
            {
                playerDeath.Invoke();
            }
        }

        [Command]
        private void CmdSetHealth(int heal) => health = heal;
    }
}