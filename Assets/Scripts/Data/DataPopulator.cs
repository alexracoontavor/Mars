using SimpleJSON;
using System.Collections.Generic;
using System;

public static class DataPopulator
{
    public class RequirementElement
    {
        public string RequirementType; //History or Resource
        public string RequirementName;
        public string ComparisonType; // >, <, >=, <=, !=, ==
        public float ComparisonValue;
    }

    public class ChangeElement
    {
        public DataElement AffectedElement;
        public float AffectValue;
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
        public List<ChangeElement> Results;
        public List<RequirementElement> Requirements;
    }

    public static List<DataElement> DataTypes;

    public static void PopulateFromJSON(JSONNode jsonData)
    {
        List<string> dataKeys = jsonData.AsObject.GetKeys();

        PopulateResources(jsonData["Resources"]);
        PopulateVehicles(jsonData["Vehicles"]);
        PopulateBuildings(jsonData["Buildings"]);
        PopulateResearch(jsonData["Research"]);
    }

    private static void PopulateResearch(JSONNode jsonData)
    {
        Dictionary<string, DataPopulator.ResearchData> Research = new Dictionary<string, ResearchData>();

        List<string> dataKeys = jsonData.AsObject.GetKeys();

        foreach (string key in dataKeys)
        {
            ResearchData r = new ResearchData { Name = key };
            r.Construction = PopulateResourceValue(jsonData[key]["Costs"]);
            r.Requirements = PopulateRequirements(jsonData[key]["Requirements"]);
            r.Results = PopulateResults(jsonData[key]["Results"]);
            Research.Add(key, r);
        }

        StaticData.Research = Research;
    }

    private static void PopulateBuildings(JSONNode jsonData)
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

        StaticData.Buildings = Buildings;
    }

    private static void PopulateVehicles(JSONNode jsonData)
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

        StaticData.Vehicles = Vehicles;
    }

    private static void PopulateResources(JSONNode jsonData)
    {
        Dictionary<string, DataPopulator.ResourceData> Resources = new Dictionary<string, ResourceData>();

        List<string> dataKeys = jsonData.AsObject.GetKeys();

        foreach (string key in dataKeys)
        {
            Resources.Add(key, new ResourceData { Name = key });
        }

        StaticData.Resources = Resources;
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

        List<string> dataKeys = jsonData.AsObject.GetKeys();

        foreach (string key in dataKeys)
        {
            RequirementElement r = new RequirementElement();
            //TODO - fill this up
            answer.Add(r);
        }

        return answer;
    }

    private static List<ChangeElement> PopulateResults(JSONNode jsonData)
    {
        List<ChangeElement> answer = new List<ChangeElement>();

        List<string> dataKeys = jsonData.AsObject.GetKeys();

        foreach (string key in dataKeys)
        {
            ChangeElement c = new ChangeElement();
            //TODO - fill this up
            answer.Add(c);
        }

        return answer;
    }
}
