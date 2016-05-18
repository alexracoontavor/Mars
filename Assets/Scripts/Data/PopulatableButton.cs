using UnityEngine;
using UnityEngine.UI;
using System;

public class PopulatableButton : MonoBehaviour {

    private Text _text;
    private Button _button;
    private string _id;
    private Action<PopulatableButton> _clickCallback;

    public string Id
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }

    // Use this for initialization
    void Start () {
        _text = GetComponentInChildren<Text>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => HandleClick());
    }

    void HandleClick()
    {
        if (_clickCallback != null)
        {
            _clickCallback(this);
        }
    }

    public void Populate(string id, Action<PopulatableButton> clickCallback)
    {
        this._id = id;
        this._clickCallback = clickCallback;
        this._text.text = id;
    }
}
