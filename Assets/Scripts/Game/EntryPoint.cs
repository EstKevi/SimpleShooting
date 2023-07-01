using Mirror;
using UnityEngine;

namespace Game
{
    public class EntryPoint : MonoBehaviour
    {
        private NetworkManager networkManager;

        private void Awake() => networkManager = FindObjectOfType<NetworkManager>();
    }
}