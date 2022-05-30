using System;
using System.Collections.Generic;
using System.Linq;
using HideAndSeek;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HideAndSeekTests;

[TestClass]
public class HideAndSeekTests
{
    [TestMethod]
    public void TestHiding()
    {
        var hidingLocation = new LocationWithHidingPlace("Room", "under the bed");

        Assert.AreEqual("Room", hidingLocation.Name);
        Assert.AreEqual("Room", hidingLocation.ToString());
        Assert.AreEqual("under the bed", hidingLocation.HidingPlace);

        var opponent1 = new Opponent("Opponent1");
        var opponent2 = new Opponent("Opponent2");
        hidingLocation.Hide(opponent1);
        hidingLocation.Hide(opponent2);

        CollectionAssert.AreEqual(new List<Opponent>(){ opponent1, opponent2},
            hidingLocation.CheckHidingPlace().ToList());

        CollectionAssert.AreEqual(new List<Opponent>(),
            hidingLocation.CheckHidingPlace().ToList());
    }

    [TestMethod]
    public void TestHidingPlaces()
    {
        Assert.IsInstanceOfType(House.GetLocationByName("Garage"), typeof(LocationWithHidingPlace));
        Assert.IsInstanceOfType(House.GetLocationByName("Kitchen"), typeof(LocationWithHidingPlace));
        Assert.IsInstanceOfType(House.GetLocationByName("Living Room"), typeof(LocationWithHidingPlace));
        Assert.IsInstanceOfType(House.GetLocationByName("Bathroom"), typeof(LocationWithHidingPlace));
        Assert.IsInstanceOfType(House.GetLocationByName("Master Bedroom"), typeof(LocationWithHidingPlace));
        Assert.IsInstanceOfType(House.GetLocationByName("Master Bath"), typeof(LocationWithHidingPlace));
        Assert.IsInstanceOfType(House.GetLocationByName("Second Bathroom"), typeof(LocationWithHidingPlace));
        Assert.IsInstanceOfType(House.GetLocationByName("Kids Room"), typeof(LocationWithHidingPlace));
        Assert.IsInstanceOfType(House.GetLocationByName("Nursery"), typeof(LocationWithHidingPlace));
        Assert.IsInstanceOfType(House.GetLocationByName("Pantry"), typeof(LocationWithHidingPlace));
        Assert.IsInstanceOfType(House.GetLocationByName("Attic"), typeof(LocationWithHidingPlace));
    }

    [TestMethod]
    public void TestClearHidingPlaces()
    {
        var garage = House.GetLocationByName("Garage") as LocationWithHidingPlace;
        garage.Hide(new Opponent("Opponent1"));

        var attic = House.GetLocationByName("Garage") as LocationWithHidingPlace;
        attic.Hide(new Opponent("Opponent2"));
        attic.Hide(new Opponent("Opponent3"));
        attic.Hide(new Opponent("Opponent4"));

        House.ClearHidingPlaces();
        Assert.AreEqual(0, garage.CheckHidingPlace().Count());
        Assert.AreEqual(0, attic.CheckHidingPlace().Count());
    }

}

/// <summary>
/// Mock Random for testing that uses a list to return values
/// </summary>
public class MockRandomWithValueList : System.Random
{
    Queue<int> valuesToReturn;
    public MockRandomWithValueList(IEnumerable<int> values) =>
        valuesToReturn = new Queue<int>(values);
    public int NextValue()
    {
        var nextValue = valuesToReturn.Dequeue();
        valuesToReturn.Enqueue(nextValue);
        return nextValue;
    }
    public override int Next() => NextValue();
    public override int Next(int maxValue) => Next(0, maxValue);
    public override int Next(int minValue, int maxValue)
    {
        var next = NextValue();
        return next >= minValue && next < maxValue ? next : minValue;
    }
}