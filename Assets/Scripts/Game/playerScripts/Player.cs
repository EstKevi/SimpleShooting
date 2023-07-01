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
        private Vector2 direction = new(0.5f,1);

        private void Awake()
        {
            joystick = FindObjectOfType<Joystick>();
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