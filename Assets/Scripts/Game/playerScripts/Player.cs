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

        private void Awake()
        {
            joystick = FindObjectOfType<Joystick>();
        }

        private void Update()
        {
            if(isLocalPlayer is false) return;
            playerMove.Move(new Vector2(joystick.Horizontal,joystick.Vertical) * (speed * Time.deltaTime));
        }
    }
}