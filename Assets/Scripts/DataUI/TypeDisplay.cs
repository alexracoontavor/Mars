using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypeDisplay : MonoBehaviour {

    public Text NameText;
    protected DataPopulator.DataElement _data;

    public void Populate(DataPopulator.DataElement data)
    {
        this._data = data;
        this.NameText.text = data.Name;
    }
}
