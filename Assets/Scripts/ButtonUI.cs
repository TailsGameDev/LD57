using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField]
    private Sprite defaultSprite = null;
    [SerializeField]
    private Sprite hoverSprite = null;
    [SerializeField]
    private Sprite pressedSprite = null;

    [SerializeField]
    private Image image = null;


    public void OnPointerEnter()
    {
        image.sprite = hoverSprite;
    }
    public void OnPointerExit()
    {
        image.sprite = defaultSprite;
    }
    public void OnPointerDown()
    {
        image.sprite = pressedSprite;
    }
    public void OnPointerUp()
    {
        image.sprite = defaultSprite;
    }
}
