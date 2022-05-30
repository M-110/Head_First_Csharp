using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoFish;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoFishTests;

[TestClass]
public class GameControllerTests
{
    [TestInitialize]
    public void Initialize()
    {
        Player.Random = new MockRandom() {ValueToReturn = 0};
    }

    [TestMethod]
    public void TestConstructor()
    {
        var gameController = new GameController("Human",
            new List<string> {"Player1", "Player2", "Player3"});
        Assert.AreEqual("Starting a new game with players Human, Player1, Player2, Player3",
            gameController.Status);
    }

    [TestMethod]
    public void TestNextRound()
    {
        var gameController = new GameController("Chell", new List<string> {"Computer"});

        gameController.NextRound(gameController.Opponents.First(), Value.Six);
        Assert.AreEqual("Chell asked Computer for Sixes" +
                        Environment.NewLine + "Computer has 1 Six card" +
                        Environment.NewLine + "Computer asked Chell for Sevens" +
                        Environment.NewLine + "Computer drew a card" +
                        Environment.NewLine + "Chell has 6 cards and 0 books" +
                        Environment.NewLine + "Computer has 5 cards and 0 books" +
                        Environment.NewLine + "The stock has 41 cards" +
                        Environment.NewLine + Environment.NewLine, gameController.Status);
    }

    [TestMethod]
    public void TestNewGame()
    {
        Player.Random = new MockRandom() {ValueToReturn = 0};
        var gameController = new GameController("Chell", new List<string> {"Computer"});
        gameController.NextRound(gameController.Opponents.First(), Value.Six);
        gameController.NewGame();
        Assert.AreEqual("Chell", gameController.HumanPlayer.Name);
        Assert.AreEqual("Computer", gameController.Opponents.First().Name);
        Assert.AreEqual("Starting a new game", gameController.Status);
    }

}
