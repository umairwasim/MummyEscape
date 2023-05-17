using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSpriteReplacer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite defaultSprite, pressedSprite;

    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = defaultSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.sprite = pressedSprite;
    }
}
