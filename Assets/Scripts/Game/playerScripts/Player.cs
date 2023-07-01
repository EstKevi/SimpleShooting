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
        [SerializeField] private UiWinWindow window;
        [SyncVar]
        [SerializeField]private int coins;

        public int Coins => coins;
    
        private Vector2 direction = new(0.5f,1);
        private float prShoot;

        [NonSerialized] public UnityEvent death;

        public Color PlayerColor => playerColor.Color;

        private void Awake()
        {
            death = playerHealth.playerDeath;
            joystick = FindObjectOfType<Joystick>();
            window = FindObjectOfType<UiWinWindow>();
            death.AddListener(() =>
            {
                // if (isServer)
                // {
                //     
                // }
            });
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

        public void ShowCanvas(Color winColor, int winCountCoins)
        {
            if(isLocalPlayer)
                CmdShowCanvas( winColor, winCountCoins);
        }
        [Command]
        private void CmdShowCanvas(Color winColor, int winCountCoins)
        {
            window.SetParams(winColor,winCountCoins);
            window.SetActiveWinWindow(true);
            if(isServer)
                NetworkServer.Destroy(gameObject);
        }

        public void TakeCoin() => coins++;
    }
}