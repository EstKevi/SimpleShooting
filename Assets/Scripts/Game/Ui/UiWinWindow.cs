using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui
{
    public class UiWinWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countCoinsWin;
        [SerializeField] private Image playerColor;
        [SerializeField] private GameObject window;
        [SerializeField] private Slider healthBar;
        [SerializeField] private TextMeshProUGUI countCountNow;
        
        public int UiValueHealth
        {
            get => (int) healthBar.value;
            set => healthBar.value = value;
        }
        
        public int CountCoin
        {
            set => countCountNow.text = value.ToString();
        }

        public void SetMaxHealthPlayer(int hp) => healthBar.maxValue = hp;

        public void SetActiveWinWindow(bool state) => window.SetActive(state);

        public void SetParams(Color color, int coins)
        {
            playerColor.color = color;
            countCoinsWin.text = coins.ToString();
        }
    }
}