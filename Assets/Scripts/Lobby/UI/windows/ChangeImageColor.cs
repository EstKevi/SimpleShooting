using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Lobby.UI.windows
{
    public class ChangeImageColor : MonoBehaviour, IPointerClickHandler
    {
        private Image image;

        public Color ImageColor => image.color; 

        private void Awake()
        {
            image ??= GetComponent<Image>();
            ChangeColor();
        }

        private void ChangeColor() => image.color = Random.ColorHSV();

        public void OnPointerClick(PointerEventData _) => ChangeColor();
    }
}