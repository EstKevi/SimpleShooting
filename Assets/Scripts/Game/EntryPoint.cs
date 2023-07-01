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
        private NetworkConnectionToClient lol;
        private readonly List<NetworkConnectionToClient> players = new();
        private void Awake() => NetworkServer.OnConnectedEvent += AddPlayer;

        private void AddPlayer(NetworkConnectionToClient player)
        {
            if(isServer is false) return;
            players.Add(player);
            StartCoroutine(AsyncAddListener(player));
        }

        private void Win(Player player)
        {
            window.SetParams(player.PlayerColor, player.Coins);
            window.SetActiveWinWindow(true);
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
                if (player.identity != null)
                {
                    if(player.identity.TryGetComponent<Player>(out var winPlayer))
                        Win(winPlayer);
                    continue;
                }
                
                if(player.identity.TryGetComponent<Player>(out var losePlayer))
                {
                    losePlayer.ShowCanvas(losePlayer.PlayerColor,1);
                }
            }
        } 
    }
}