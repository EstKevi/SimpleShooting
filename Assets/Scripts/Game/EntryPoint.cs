using System;
using Mirror;

namespace Game
{
    public class EntryPoint : NetworkBehaviour
    {
        private NetworkManager networkManager;

        private void Awake()
        {
            networkManager ??= FindObjectOfType<NetworkManager>();
        }
    }
}