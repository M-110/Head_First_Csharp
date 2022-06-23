namespace HideAndSeek;

public static class House
{
    public static Random Random = new Random();
    public static Location Entry;
    public static List<Location> _locations = new();

    static House()
    {
        Entry = new Location("Entry");

        var garage = new LocationWithHidingPlace("Garage", "behind the car");
        var hallway = new Location("Hallway");
        var livingRoom = new LocationWithHidingPlace("Living Room", "behind the sofa");
        var bathroom = new LocationWithHidingPlace("Bathroom", "behind the door");
        var kitchen = new LocationWithHidingPlace("Kitchen", "next to the stove");
        var pantry = new LocationWithHidingPlace("Pantry", "inside a cabinet");
        var kidsRoom = new LocationWithHidingPlace("Kids Room", "behind the door");
        var landing = new Location("Landing");
        var nursery = new LocationWithHidingPlace("Nursery", "inside a cabinet");
        var secondBathroom = new LocationWithHidingPlace("Second Bathroom", "in the shower");
        var masterBedroom = new LocationWithHidingPlace("Master Bedroom", "under the bed");
        var masterBath = new LocationWithHidingPlace("Master Bath", "inside a cabinet");
        var attic = new LocationWithHidingPlace("Attic", "in a trunk");

        _locations.Add(Entry);
        _locations.Add(garage);
        _locations.Add(hallway);
        _locations.Add(livingRoom);
        _locations.Add(bathroom);
        _locations.Add(kitchen);
        _locations.Add(pantry);
        _locations.Add(kidsRoom);
        _locations.Add(landing);
        _locations.Add(nursery);
        _locations.Add(secondBathroom);
        _locations.Add(masterBedroom);
        _locations.Add(masterBath);
        _locations.Add(attic);

        Entry.AddExit(Direction.Out, garage);
        Entry.AddExit(Direction.East, hallway);
        hallway.AddExit(Direction.Northwest, kitchen);
        hallway.AddExit(Direction.North, bathroom);
        hallway.AddExit(Direction.South, livingRoom);
        hallway.AddExit(Direction.Up, landing);
        landing.AddExit(Direction.Northwest, masterBedroom);
        masterBedroom.AddExit(Direction.East, masterBath);
        landing.AddExit(Direction.West, secondBathroom);
        landing.AddExit(Direction.Southwest, nursery);
        landing.AddExit(Direction.South, pantry);
        landing.AddExit(Direction.Southeast, kidsRoom);
        landing.AddExit(Direction.Up, attic);
    }

    public static Location GetLocationByName(string name)
    {
        foreach (var location in _locations)
            if (location.Name == name)
                return location;
        return _locations[0];
    }

    public static Location RandomExit(Location location)
    {
        var exits = location.Exits
            .OrderBy(pair => pair.Value.ToString())
            .Select(pair => pair.Value)
            .ToList();
        return exits[Random.Next()%exits.Count];
    }

    public static void ClearHidingPlaces()
    {
        foreach (var location in _locations.OfType<LocationWithHidingPlace?>())
        {
            location?.CheckHidingPlace();
        }
    }

}