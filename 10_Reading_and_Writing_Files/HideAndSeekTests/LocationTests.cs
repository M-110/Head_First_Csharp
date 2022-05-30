using System;
using System.Collections.Generic;
using System.Linq;
using HideAndSeek;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HideAndSeekTests;

[TestClass]
public class LocationTests
{
    Location center;

    /// <summary>
    /// Initialize test by creating a new center location and adding a room in each direction
    /// </summary>
    [TestInitialize]
    public void Initialize()
    {
        center = new Location("Center Room");
        Assert.AreSame("Center Room", center.ToString());
        Assert.AreEqual(0, center.ExitList.Count());

        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            var name = direction.ToString();
            center.AddExit(direction, new Location($"{name} Room"));
        }

        Assert.AreEqual(12, center.ExitList.Count());
    }

    /// <summary>
    /// Test that GetExit returns the location in a direction only if it exists
    /// </summary>
    [TestMethod]
    public void TestGetExit()
    {
        var eastRoom = center.GetExit(Direction.East);
        Assert.AreEqual("East Room", eastRoom.Name);
        Assert.AreSame(center, eastRoom.GetExit(Direction.West));
        Assert.AreSame(eastRoom, eastRoom.GetExit(Direction.Up));
    }

    /// <summary>
    /// Validate that the exit lists are working
    /// </summary>
    [TestMethod]
    public void TestExitList()
    {
        var expected = new List<string>
        {
            "Down Room",
            "East Room",
            "In Room",
            "North Room",
            "Northeast Room",
            "Northwest Room",
            "Out Room",
            "South Room",
            "Southeast Room",
            "Southwest Room",
            "Up Room",
            "West Room",
        };
        var actual = center.ExitList.ToList();
        CollectionAssert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Validates that each room's name and return exit is created correctly
    /// </summary>
    [TestMethod]
    public void TestReturnExits()
    {
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            var otherRoom = center.GetExit(direction);
            var oppositeDirection = (Direction) (-(int) direction);
            Assert.AreSame(center, otherRoom.GetExit(oppositeDirection));
        }
    }


    /// <summary>
    /// Add a hall to one of the rooms and check that its name and exits were created.
    /// </summary>
    [TestMethod]
    public void TestAddHall()
    {
        var eastRoom = center.GetExit(Direction.East);
        var eastEastRoom = new Location("East East Room");
        eastRoom.AddExit(Direction.East, eastEastRoom);

        Assert.AreSame(center, eastEastRoom.GetExit(Direction.West).GetExit(Direction.West));

    }
}