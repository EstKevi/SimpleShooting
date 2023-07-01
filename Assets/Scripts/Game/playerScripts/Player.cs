using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Game.playerScripts
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private PlayerMove playerMove;
        [SerializeField] private Joystick joystick;
        [SerializeField] private PlayerWeapon playerWeapon;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private PlayerColor playerColor;
        
        private Vector2 direction = new(0.5f,1);

        [NonSerialized] public UnityEvent death;

        public Color PlayerColor => playerColor.Color;

        private void Awake()
        {
            death = playerHealth.playerDeath;
            joystick = FindObjectOfType<Joystick>();
            death.AddListener(() =>
            {
                if (isServer)
                {
                    NetworkServer.Destroy(gameObject);
                }
            });
        }

        private void Update()
        {
            if(isLocalPlayer is false) return;
            
            var directionMove = new Vector2(joystick.Horizontal, joystick.Vertical);
            playerMove.Move(directionMove * (speed * Time.deltaTime));
            
            if (directionMove != Vector2.zero)
                direction = directionMove;
            
            if (Input.GetKeyDown(KeyCode.Space)) playerWeapon.Shoot(direction);
        }

        [ContextMenu("kill")]
        public void KillPlayer()
        {
            playerHealth.MakeDamage(100);
        }
    }
}