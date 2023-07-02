using System;
using Game.Ui;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Game.playerScripts
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float delayShoot;
        [SerializeField] private PlayerMove playerMove;
        [SerializeField] private Joystick joystick;
        [SerializeField] private PlayerWeapon playerWeapon;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private PlayerColor playerColor;
        [SerializeField] private UiWinWindow playerUi;
        
        [SyncVar]
        [SerializeField]private int coins;
        [SyncVar]
        [SerializeField] private bool isDead;
        
        private Vector2 direction = new(0.5f,1);
        private float prShoot;

        [NonSerialized] public readonly UnityEvent death = new();
        
        public int Coins => coins;
        public bool IsDead => isDead;
        
        public Color PlayerColor => playerColor.Color;

        private void Awake()
        {
            joystick = FindObjectOfType<Joystick>();
            playerUi = FindObjectOfType<UiWinWindow>();

            playerUi.SetMaxHealthPlayer(playerHealth.Health);
            
            playerHealth.playerDeath.AddListener((() =>
            {
                if(isLocalPlayer)
                {
                    isDead = true;
                    CmdPlayerDeath();
                }
            }));
        }

        [Command]
        private void CmdPlayerDeath()
        {
            isDead = true;
            death.Invoke();
        }

        [ClientRpc]
        public void RpcChangeHealthUi(int damage)
        {
            if(isLocalPlayer);
            playerUi.UiValueHealth -= damage;
        }

        private void Update()
        {
            if(isLocalPlayer is false) return;
            
            var directionMove = new Vector2(joystick.Horizontal, joystick.Vertical);
            playerMove.Move(directionMove * (speed * Time.deltaTime));
            
            if (directionMove != Vector2.zero)
            {
                direction = directionMove;
                if(Time.time > prShoot + delayShoot)
                {
                    prShoot = Time.time;
                    playerWeapon.Shoot(direction);
                }
            }
        }

        [ClientRpc]
        public void RpcShowCanvas(Color winColor, int winCountCoins)
        {
            if(isLocalPlayer)
            {
                playerUi.SetParams(winColor, winCountCoins);
                playerUi.SetActiveWinWindow(true);
            }
            // if(isServer)
            //     NetworkServer.Destroy(gameObject);
        }

        [ClientRpc]
        public void RpcTakeCoin()
        {
            coins++;
            if (isLocalPlayer)
            {
                playerUi.CountCoin = coins;
            }
        }
    }
}