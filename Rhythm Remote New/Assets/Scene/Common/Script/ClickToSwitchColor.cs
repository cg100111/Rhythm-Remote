using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickToSwitchColor : MonoBehaviour,IPointerClickHandler
{
    private Image _image;
    public Color _color;
    private Color _colorOrigin;
    private bool _switch = false;

    void Start()
    {
        _image = this.GetComponent<Image>();
        _colorOrigin = _image.color;
    }

    public void OnPointerClick(PointerEventData data)
    {
        _switch = !_switch;

        if (_switch)
            _image.color = _color;
        else
            _image.color = _colorOrigin;
    }

    public int GetStatas()
    {
        if (_switch)
            return 1;
        else
            return 0;
    }
}
