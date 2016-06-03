using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickToColor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image _image;
    public Color _color;
    private Color _colorOrigin;

    void Start()
    {
        _image = this.GetComponent<Image>();
        _colorOrigin = _image.color;
    }

    public void OnPointerDown(PointerEventData data)
    {
        _image.color = _color;
    }

    public void OnPointerUp(PointerEventData data)
    {
        _image.color = _colorOrigin;
    }
}
