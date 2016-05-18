using System;
using Diwip.Tools.Events;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class DataBottomBar : MonoBehaviour
{
    private PopulatableButton[] _buttons;

    void Awake()
    {
        _buttons = GetComponentsInChildren<PopulatableButton>(true);
        EventsManager.AddListener(typeof(DataPopulator.DataPopulationCompleteEvent), HandleDataPopulationCompleteEvent);

        DataPopulator.BuildingsData bd = new DataPopulator.BuildingsData();
        DataPopulator.DataElement de = (DataPopulator.DataElement)bd;
        Debug.Log(de);
        Dictionary<string, DataPopulator.DataElement> dict = new Dictionary<string, DataPopulator.DataElement>();
        dict.Add("entry", de);
    }

    private void HandleDataPopulationCompleteEvent(BaseEvent eventObject)
    {
        PropertyInfo[] staticProperties = typeof(StaticData).GetProperties(BindingFlags.Static | BindingFlags.Public);

        for (int i = 0; i < staticProperties.Length; i++)
        {
            _buttons[i].Populate(staticProperties[i].Name, HandleButtonClicked);
        }
    }

    private void HandleButtonClicked(PopulatableButton button)
    {
        EventsManager.Dispatch(new ItemsSelectionDisplay.ItemsListSelectedEvent()
        {
            PropertyName = button.Id,
            dataList = CastDict((IDictionary)typeof(StaticData).GetProperty(button.Id).GetValue(null, null)).ToDictionary(entry => (string)entry.Key, entry => (DataPopulator.DataElement)entry.Value)
        });
    }

    private IEnumerable<DictionaryEntry> CastDict(IDictionary dictionary)
    {
        foreach (DictionaryEntry entry in dictionary)
        {
            yield return entry;
        }
    }
}
