using SimpleJSON;
using System.Collections.Generic;
using System;
using Diwip.Tools.Events;

public static class DataPopulator
{
    public class DataPopulationCompleteEvent:BaseEvent
    {

    }

    public class RequirementElement
    {
        public string RequirementType; //History or Resource
        public string RequirementName;
        public string ComparisonType; // >, <, >=, <=, !=, ==
        public float ComparisonValue;
    }

    public class ResultData
    {
        public Dictionary<string, DataPopulator.ResourceData> Resources;
        public Dictionary<string, DataPopulator.VehiclesData> Vehicles;
        public Dictionary<string, DataPopulator.BuildingsData> Buildings;
        public Dictionary<string, DataPopulator.ResearchData> Research;
    }

    public class DataElement
    {
        public string ParentType;
        public string Name;
    }

    public class ResourceData : DataElement
    {
        public float Value;
    }

    public class VehiclesData : DataElement
    {
        public VehiclesData()
        {
            this.ParentType = "Vehicles";
        }

        public List<ResourceData> Construction;
        public List<ResourceData> Operation;
        public List<ResourceData> Production;
    }

    public class BuildingsData : DataElement
    {
        public BuildingsData()
        {
            this.ParentType = "Buildings";
        }

        public List<ResourceData> Construction;
        public List<ResourceData> Operation;
        public List<ResourceData> Production;
    }

    public class ResearchData : DataElement
    {
        public ResearchData()
        {
            this.ParentType = "Research";
        }

        public List<ResourceData> Construction;
        public ResultData Result;
        public List<RequirementElement> Requirements;
    }

    public static List<DataElement> DataTypes;

    public static void PopulateFromJSON(JSONNode jsonData)
    {
        List<string> dataKeys = jsonData.AsObject.GetKeys();

        StaticData.Resources = PopulateResources(jsonData["Resources"]);
        StaticData.Vehicles = PopulateVehicles(jsonData["Vehicles"]);
        StaticData.Buildings = PopulateBuildings(jsonData["Buildings"]);
        StaticData.Research = PopulateResearch(jsonData["Research"]);

        //TODO - decide upon and implement actions
        UnityEngine.Debug.Log("Parsing complete");

        EventsManager.Dispatch(new DataPopulationCompleteEvent());
    }

    private static Dictionary<string, DataPopulator.ResearchData> PopulateResearch(JSONNode jsonData)
    {
        Dictionary<string, DataPopulator.ResearchData> Research = new Dictionary<string, ResearchData>();

        List<string> dataKeys = jsonData.AsObject.GetKeys();

        foreach (string key in dataKeys)
        {
            ResearchData r = new ResearchData { Name = key };
            r.Construction = PopulateResourceValue(jsonData[key]["Costs"]);
            r.Requirements = PopulateRequirements(jsonData[key]["Requirements"]);
            r.Result = PopulateResults(jsonData[key]["Results"]);
            Research.Add(key, r);
        }

        return Research;
    }

    private static Dictionary<string, BuildingsData> PopulateBuildings(JSONNode jsonData)
    {
        Dictionary<string, DataPopulator.BuildingsData> Buildings = new Dictionary<string, BuildingsData>();

        List<string> dataKeys = jsonData.AsObject.GetKeys();

        foreach (string key in dataKeys)
        {
            BuildingsData b = new BuildingsData { Name = key };
            b.Construction = PopulateResourceValue(jsonData[key]["Construction"]);
            b.Operation = PopulateResourceValue(jsonData[key]["Operation"]);
            b.Production = PopulateResourceValue(jsonData[key]["Production"]);
            Buildings.Add(key, b);
        }

        return Buildings;
    }

    private static Dictionary<string, VehiclesData> PopulateVehicles(JSONNode jsonData)
    {
        Dictionary<string, DataPopulator.VehiclesData> Vehicles = new Dictionary<string, VehiclesData>();

        List<string> dataKeys = jsonData.AsObject.GetKeys();

        foreach (string key in dataKeys)
        {
            VehiclesData v = new VehiclesData { Name = key };
            v.Construction = PopulateResourceValue(jsonData[key]["Construction"]);
            v.Operation = PopulateResourceValue(jsonData[key]["Operation"]);
            v.Production = PopulateResourceValue(jsonData[key]["Production"]);
            Vehicles.Add(key, v);
        }

        return Vehicles;
    }

    private static Dictionary<string, ResourceData> PopulateResources(JSONNode jsonData)
    {
        Dictionary<string, DataPopulator.ResourceData> Resources = new Dictionary<string, ResourceData>();

        List<string> dataKeys = jsonData.AsObject.GetKeys();

        foreach (string key in dataKeys)
        {
            Resources.Add(key, new ResourceData { Name = key });
        }

        return Resources;
    }

    private static List<ResourceData> PopulateResourceValue(JSONNode jsonData)
    {
        List<ResourceData> answer = new List<ResourceData>();

        List<string> dataKeys = jsonData.AsObject.GetKeys();

        foreach (string key in dataKeys)
        {
            answer.Add(new ResourceData { Name = key, Value = float.Parse(jsonData[key].Value) });
        }

        return answer;
    }

    private static List<RequirementElement> PopulateRequirements(JSONNode jsonData)
    {
        List<RequirementElement> answer = new List<RequirementElement>();

        answer.AddRange(ExtractRequirements(jsonData, "History"));
        answer.AddRange(ExtractRequirements(jsonData, "Resources"));

        return answer;
    }
    
    private static List<RequirementElement> ExtractRequirements(JSONNode jsonData, string typeKey)
    {
        List<RequirementElement> answer = new List<RequirementElement>();

        JSONNode typeNode = jsonData[typeKey];

        List<string> dataKeys = typeNode.AsObject.GetKeys();

        foreach (string key in dataKeys)
        {
            RequirementElement r = new RequirementElement();
            r.RequirementType = typeKey;
            r.RequirementName = key;
            r.ComparisonType = typeNode[key]["Comparison"];
            r.ComparisonValue = typeNode[key]["Value"].AsFloat;
            answer.Add(r);
        }

        return answer;
    }

    private static ResultData PopulateResults(JSONNode jsonData)
    {
        ResultData answer = new ResultData();

        answer.Resources =   PopulateResources(jsonData["Resources"]);
        answer.Vehicles =    PopulateVehicles(jsonData["Vehicles"]);
        answer.Buildings =   PopulateBuildings(jsonData["Buildings"]);
        answer.Research =    PopulateResearch(jsonData["Research"]);

        return answer;
    }
}
