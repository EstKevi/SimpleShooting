using Mirror;
using UnityEngine;

namespace Game
{
    public class EntryPoint : MonoBehaviour
    {
        private NetworkManager networkManager;
        private DataSave dataSave;

        public Color PlayerColor => dataSave.Color;

        private void Awake()
        {
            dataSave = FindObjectOfType<DataSave>();
            networkManager = FindObjectOfType<NetworkManager>();
            Debug.Log(PlayerColor);
        }
    }
}