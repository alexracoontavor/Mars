using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Diwip.Tools.Events;

public class ItemsSelectionDisplay : MonoBehaviour {

    public class ItemsListSelectedEvent:BaseEvent
    {
        public string PropertyName;
        public Dictionary<string, DataPopulator.DataElement> dataList;
    }

    public GameObject ItemPrefab;
    public Transform ContentTransform;

    private List<ItemSelectionElement> _items = new List<ItemSelectionElement>();

    private string _currentDisplayedProperty;

    void Awake()
    {
        EventsManager.AddListener(typeof(ItemsSelectionDisplay.ItemsListSelectedEvent), HandleItemsListSelectedEvent);
    }

    private void HandleItemsListSelectedEvent(BaseEvent eventObject)
    {
        ItemsListSelectedEvent e = (ItemsListSelectedEvent)eventObject;

        Populate(e.PropertyName, e.dataList);
    }

    public void Populate<T>(string propertyName, Dictionary<string, T> dataList) where T : DataPopulator.DataElement
    {
        Depopulate();

        if (_currentDisplayedProperty == propertyName)
        {
            _currentDisplayedProperty = "";
            return;
        }

        foreach (DataPopulator.DataElement item in dataList.Values)
        {
            GameObject instance = Instantiate(ItemPrefab);
            instance.transform.SetParent(ContentTransform, false);
            ItemSelectionElement element = instance.GetComponentInChildren<ItemSelectionElement>(true);
            element.Populate(item);
            _items.Add(element);
        }
    }

    private void Depopulate()
    {
        foreach (ItemSelectionElement item in _items)
        {
            Destroy(item.gameObject);
        }

        _items.Clear();
    }
}
