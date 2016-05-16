using System.Collections.Generic;

public static class StaticData
{
    public static Dictionary<string, DataPopulator.ResourceData> Resources;
    public static Dictionary<string, DataPopulator.VehiclesData> Vehicles;
    public static Dictionary<string, DataPopulator.BuildingsData> Buildings;
    public static Dictionary<string, DataPopulator.ResearchData> Research;

    /*
    TODO:
        Iteratively populate ExpandoObject from JSON
        Iteratively populate JSON from ExpandoObject and save
        Populate:
            Resources
            Vehicles
            Buildings
            Research
            Actions
            Events
*/
}
