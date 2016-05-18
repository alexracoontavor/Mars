using System.Collections.Generic;

public static class StaticData
{
    private static Dictionary<string, DataPopulator.ResourceData>   _resources;
    private static Dictionary<string, DataPopulator.VehiclesData> _vehicles;
    private static Dictionary<string, DataPopulator.BuildingsData> _buildings;
    private static Dictionary<string, DataPopulator.ResearchData> _research;

    public static Dictionary<string, DataPopulator.ResourceData> Resources
    {
        get
        {
            return _resources;
        }

        set
        {
            _resources = value;
        }
    }

    public static Dictionary<string, DataPopulator.VehiclesData> Vehicles
    {
        get
        {
            return _vehicles;
        }

        set
        {
            _vehicles = value;
        }
    }

    public static Dictionary<string, DataPopulator.BuildingsData> Buildings
    {
        get
        {
            return _buildings;
        }

        set
        {
            _buildings = value;
        }
    }

    public static Dictionary<string, DataPopulator.ResearchData> Research
    {
        get
        {
            return _research;
        }

        set
        {
            _research = value;
        }
    }
}
