using Mirror;
using UnityEngine;

namespace Game.playerScripts
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private PlayerMove playerMove;

        private void Update()
        {
            if(isLocalPlayer is false) return;
            playerMove.Move(new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")) * (speed * Time.deltaTime));
        }
    }
}