using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui
{
    public class UiWinWindow : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI countCoins;
        [SerializeField] private Image playerColor;
        [SerializeField] private GameObject window;

        public void SetActiveWinWindow(bool state) => window.SetActive(state);

        public void SetParams(Color color, int coins)
        {
            countCoins.text = coins.ToString();
            playerColor.color = color;
        }
    }
}