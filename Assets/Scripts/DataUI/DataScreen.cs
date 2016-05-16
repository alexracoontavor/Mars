using Diwip.Tools;
using Diwip.UI.Screens.ConcreteExample;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine;

public class DataScreen : SimpleScreen
{
    public Text OutputText;

    public void HandleLoadClicked()
    {
        string dataString = SerializedDataImporter.LoadStringData("MarsTest.json");

        if (dataString != null)
        {
            OutputText.text = "Data string loaded with length " + dataString.Length + "\n";
            JSONNode jsonData = JSONNode.Parse(dataString);
            DataPopulator.PopulateFromJSON(jsonData);
        }
        else
        {
            OutputText.text = "Data string failed to load!";
        }
    }
}
