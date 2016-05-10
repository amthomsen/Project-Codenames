using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonBehaviour : BaseObject {

    Image _image;

    protected override void LateAwake() {
        _image = GetComponent<Image>();
	}

    void Start()
    {
        _image.color = GameManager.instance.startColor;
    }
    
    public void ChangeColor()
    {
        if (_image.color == GameManager.instance.startColor)
        {
            _image.color = GameManager.instance.red;
        }
        else if (_image.color == GameManager.instance.red)
        {
            _image.color = GameManager.instance.blue;
        }
        else if (_image.color == GameManager.instance.blue)
        {
            _image.color = GameManager.instance.grey;
        }
        else if (_image.color == GameManager.instance.grey)
        {
            _image.color = GameManager.instance.startColor;
        }
    }
}

