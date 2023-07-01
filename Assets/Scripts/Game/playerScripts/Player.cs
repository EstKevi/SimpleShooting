using System;
using Mirror;
using UnityEngine;

namespace Game.playerScripts
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private PlayerMove playerMove;
        [SerializeField] private Joystick joystick;
        [SerializeField] private PlayerWeapon playerWeapon;
        
        private SpriteRenderer spriteRenderer;
        private Vector2 direction = new(0.5f,1);

        private void Awake()
        {
            joystick = FindObjectOfType<Joystick>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if(!isLocalPlayer || NetworkServer.active is false) return;
            var data = FindObjectOfType<DataSave>();
            spriteRenderer.color = data.Color;
            RpcChangeColorOnServer(spriteRenderer.color);
        }

        [ClientRpc]
        private void RpcChangeColorOnServer(Color color)
        {
            spriteRenderer.color = color;
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
    }
}