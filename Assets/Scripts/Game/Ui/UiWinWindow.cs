using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui
{
    public class UiWinWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countCoins;
        [SerializeField] private Image playerColor;
        [SerializeField] private GameObject window;

        public void SetActiveWinWindow(bool state) => window.SetActive(state);

        public void SetParams(Color color, int coins)
        {playerColor.color = color;
            if(coins == 0) return;
            countCoins.text = coins.ToString();
            
        }
    }
}