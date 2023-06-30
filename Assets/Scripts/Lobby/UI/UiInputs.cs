using System;
using Lobby.UI.windows;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils;

namespace Lobby.UI
{
    public class UiInputs : MonoBehaviour
    {
        [Header("buttons")]
        [SerializeField] private Button connectButton;
        [SerializeField] private Button createButton;
        [Space]
        [Header("windows")]
        [SerializeField] private ConnectWindow connectWindow;
        [SerializeField] private CreateWindow createWindow;
        [Space]
        [Header("windows control")]
        [SerializeField] private OpenCloseWindows windows;

        [NonSerialized] public UnityEvent<DataPlayer> startGameButtonClicked = new();

        private void Awake()
        {
            windows.EnsureOrNull().OpenButtons();
            connectButton.EnsureOrNull().onClick.AddListener(windows.OpenConnectWindow);
            createButton.EnsureOrNull().onClick.AddListener(windows.OpenCreateWindow);
            
            connectWindow.EnsureOrNull().windowClosed.AddListener(windows.OpenButtons);
            createWindow.EnsureOrNull().windowClosed.AddListener(windows.OpenButtons);

            connectWindow.buttonSubmitClicked.AddListener(() =>
                startGameButtonClicked.Invoke(
                    new DataPlayer(
                        // connectWindow.AddressName,
                        connectWindow.ColorInput,
                        NetworkType.Client
                    )
                ));

            createWindow.buttonSubmitClicked.AddListener(() =>
                startGameButtonClicked.Invoke(
                    new DataPlayer(
                        // createWindow.AddressName,
                        createWindow.ColorInput,
                        NetworkType.Host
                    )
                ));
        }
    }

    public enum NetworkType
    {
        Host,
        Client,
    }
    
    public class DataPlayer
    {
        private DataPlayer(){}

        public DataPlayer(/*string nameAddress,*/ Color color, NetworkType networkType)
        {
            this.networkType = networkType;
            // this.nameAddress = nameAddress;
            this.color = color;
        }

        public readonly NetworkType networkType;
        // public readonly string nameAddress;
        public readonly Color color;
    }
}