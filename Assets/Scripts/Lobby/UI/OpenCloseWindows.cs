using UnityEngine;
using Utils;

namespace Lobby.UI
{
    public class OpenCloseWindows : MonoBehaviour
    {
        [SerializeField] private GameObject buttons;
        [SerializeField] private GameObject windowConnect;
        [SerializeField] private GameObject windowCreate;

        private void Awake()
        {
            buttons.EnsureOrNull();
            windowCreate.EnsureOrNull();
            windowConnect.EnsureOrNull();
        }

        public void OpenConnectWindow()
        {
            windowConnect.SetActive(true);
            buttons.SetActive(false);
            windowCreate.SetActive(false);
        }

        public void OpenCreateWindow()
        {
            windowCreate.SetActive(true);
            buttons.SetActive(false);
            windowConnect.SetActive(false);
        }

        public void OpenButtons()
        {
            buttons.SetActive(true);
            windowCreate.SetActive(false);
            windowConnect.SetActive(false);
        }
    }
}