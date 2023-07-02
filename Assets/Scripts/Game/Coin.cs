using Game.playerScripts;
using Mirror;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class Coin : NetworkBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out var player) && isServer)
            {
                player.RpcTakeCoin();
                NetworkServer.Destroy(gameObject);
            }
        }
    }
}