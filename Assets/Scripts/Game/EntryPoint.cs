using System;
using System.Collections;
using System.Collections.Generic;
using Game.playerScripts;
using Mirror;
using UnityEngine;

namespace Game
{
    public class EntryPoint : NetworkBehaviour
    {
        [SerializeField] private float delayFoundWinners;
        [SerializeField] private float delayAddPlayer;
        [SerializeField] private WinnerParams winner;
        
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
            yield return new WaitForSecondsRealtime(delayAddPlayer);
            if(playerClient.identity.TryGetComponent<Player>(out var playerDeath))
            { 
                playerDeath.death.AddListener(() =>
                {
                    StartCoroutine(FoundWinnerAndWin());
                });
            }
        }

        private IEnumerator FoundWinnerAndWin()
        {
            yield return new WaitForSecondsRealtime(delayFoundWinners);
            if(isServer is false) yield break;
            
            foreach (var player in players)
            {
                if (player.identity.TryGetComponent<Player>(out var playerObj))
                {
                    if (playerObj.IsDead is false)
                    {;
                        winner = new WinnerParams(playerObj.Coins, playerObj.PlayerColor);
                        StartCoroutine(ShowCanvasAsync(playerObj));
                        continue;
                    }

                    StartCoroutine(ShowCanvasAsync(playerObj));
                }
            }
        }

        private IEnumerator ShowCanvasAsync(Player player)
        {
            yield return new WaitForSecondsRealtime(delayFoundWinners);
            player.RpcShowCanvas(winner.color, winner.countCoins);
        }
        
        [Serializable]
        private class WinnerParams
        {
            private WinnerParams(){}
            public WinnerParams(int countCoins, Color color)
            {
                this.countCoins = countCoins;
                this.color = color;
            }

            public int countCoins;
            public Color color;
        }
    }
}