using System;
using Lobby.UI;
using Mirror;
using UnityEngine;
using Utils;

namespace Lobby
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private NetworkManager networkManager;
        [SerializeField] private UiInputs uiInputs;
        [SerializeField] private DataSave dataSave;
        
        private void Awake()
        {
            networkManager.EnsureOrNull();
            
            uiInputs.startGameButtonClicked.AddListener(data =>
            {

                dataSave.Color = data.color;
                
                switch (data.networkType)
                {
                    case NetworkType.Host:
                        networkManager.StartHost();
                        break;
                    case NetworkType.Client:
                        networkManager.StartClient();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
            DontDestroyOnLoad(dataSave);
        }
    }
}