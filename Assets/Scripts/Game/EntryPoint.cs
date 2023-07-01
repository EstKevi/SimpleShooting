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
        private List<NetworkConnectionToClient> players = new();
        private void Awake() => NetworkServer.OnConnectedEvent += AddPlayer;

        private void AddPlayer(NetworkConnectionToClient player)
        {
            players.Add(player);
        }

        private void Win(Player player)
        {
            window.SetParams(player.PlayerColor,1);
            window.SetActiveWinWindow(true);
        }

        private void OnDestroy() => NetworkServer.OnConnectedEvent -= AddPlayer;

        private IEnumerator AsyncAddListener(NetworkConnectionToClient playerClient)
        {
            yield return new WaitForSecondsRealtime(delay);
            if(playerClient.identity.TryGetComponent<Player>(out var playerDeath))
            {
                playerDeath.death.AddListener(() =>
                {
                    foreach (var player in players)
                    {
                        if (player != null)
                        {
                            Win(playerDeath);
                        }
                    }
                });
            }
        }
    }
}