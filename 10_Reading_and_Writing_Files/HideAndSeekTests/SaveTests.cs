using System.Linq;
using HideAndSeek;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HideAndSeekTests;

[TestClass]
public class SaveTests
{
    GameController gameController;

    [TestInitialize]
    public void Initialize()
    {
        gameController = new GameController();
    }
    
    [TestMethod]
    public void TestSaveLoad()
    {
        gameController.ParseInput("out");
        var savedPlayerLocation = gameController.CurrentLocation;
        var savedFoundOpponents = gameController.foundOpponents;
        var savedMoves = gameController.MoveNumber;
        gameController.ParseInput("save my_save");

        gameController = new GameController();
        gameController.ParseInput("load my_save");
        Assert.AreEqual(savedPlayerLocation.Name, gameController.CurrentLocation.Name);
        Assert.AreEqual(savedFoundOpponents.Count, gameController.foundOpponents.Count);
        Assert.AreEqual(savedMoves, gameController.MoveNumber);
    }

}