using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Game
{
    public class EntryPoint : NetworkBehaviour
    {
        private NetworkManager networkManager;
        private readonly List<NetworkConnectionToClient> players = new();

        private void Awake() => networkManager = FindObjectOfType<NetworkManager>();

        private void Start()
        {
            var iteration = NetworkServer.connections.Count;
            NetworkServer.OnConnectedEvent += AddPlayer;

            for (int i = 0; i < iteration; i++)
            {
                players.Add(NetworkServer.connections[i]);
            }

            Debug.Log(players.Count);
        }

        private void AddPlayer(NetworkConnectionToClient player)
        {
            players.Add(player);
            Debug.Log(players.Count);
        }
    }
}