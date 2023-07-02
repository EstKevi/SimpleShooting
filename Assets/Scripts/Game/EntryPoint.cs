using System.Collections;
using System.Collections.Generic;
using Game.playerScripts;
using Game.Ui;
using Mirror;
using UnityEngine;

namespace Game
{
    public class EntryPoint : NetworkBehaviour
    {
        [SerializeField] private UiWinWindow window;
        [SerializeField] private float delay;
        private Player playerWin;
        
        private NetworkConnectionToClient lol;
        private readonly List<NetworkConnectionToClient> players = new();
        private void Awake() => NetworkServer.OnConnectedEvent += AddPlayer;

        private void AddPlayer(NetworkConnectionToClient player)
        {
            if(isServer is false) return;
            players.Add(player);
            StartCoroutine(AsyncAddListener(player));
        }

        private void OnDestroy() => NetworkServer.OnConnectedEvent -= AddPlayer;

        private IEnumerator AsyncAddListener(NetworkConnectionToClient playerClient)
        {
            yield return new WaitForSecondsRealtime(delay);
            if(playerClient.identity.TryGetComponent<Player>(out var playerDeath))
            {
                playerDeath.death.AddListener(FoundWinnerAndWin);
            }
        }

        private void FoundWinnerAndWin()
        {
            if(isServer is false) return;
            
            foreach (var player in players)
            {
                if (player.identity.TryGetComponent<Player>(out var playerObj))
                {
                    if (playerObj.IsDead is false)
                    {
                        playerWin = playerObj;
                        playerObj.RpcShowCanvas(playerObj.PlayerColor, playerObj.Coins);
                    }

                    playerObj.RpcShowCanvas(playerWin.PlayerColor, playerWin.Coins);
                }
            }
        } 
    }
}