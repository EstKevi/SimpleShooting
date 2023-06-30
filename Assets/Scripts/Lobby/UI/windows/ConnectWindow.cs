using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils;

namespace Lobby.UI.windows
{
    public class ConnectWindow : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private ChangeImageColor image;
        [SerializeField] private Button submitButton;
        [SerializeField] private Button backButton;

        [NonSerialized] public readonly UnityEvent windowClosed = new();
        [NonSerialized] public readonly UnityEvent buttonSubmitClicked = new();

        public string AddressName => inputField.text;
        public Color ColorInput => image.ImageColor;

        private void Awake()
        {
            inputField.EnsureOrNull();
            image.EnsureOrNull();
            
            submitButton.EnsureOrNull().onClick.AddListener(buttonSubmitClicked.Invoke);
            
            backButton.EnsureOrNull().onClick.AddListener(() =>
            {
                windowClosed.Invoke();
                gameObject.SetActive(false);
            });
        }
    }
}