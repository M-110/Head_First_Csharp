using GoFish;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoFishTests;

[TestClass]
public class GameStateTests
{
    [TestMethod]
    public void TestConstructor()
    {
        var computerPlayerNames = new List<string>()
        {
            "Computer1",
            "Computer2",
            "Computer3"
        };
        var gameState = new GameState("Human", computerPlayerNames, new Deck());

        CollectionAssert.AreEqual(
            new List<string> { "Human", "Computer1", "Computer2", "Computer3" },
            gameState.Players.Select(player => player.Name).ToList());

        Assert.AreEqual(5, gameState.HumanPlayer.Hand.Count());
    }

    [TestMethod]
    public void TestRandomPlayer()
    {
        var computerPlayerNames = new List<string>()
        {
            "Computer1",
            "Computer2",
            "Computer3"
        };
        var gameState = new GameState("Human", computerPlayerNames, new Deck());
        Player.Random = new MockRandom() {ValueToReturn = 1};
        Assert.AreEqual("Computer2", 
            gameState.RandomPlayer(gameState.Players.First()).Name);

        Player.Random = new MockRandom() { ValueToReturn = 0 };
        Assert.AreEqual("Computer1",
            gameState.RandomPlayer(gameState.Players.First()).Name);
    }

    [TestMethod]
    public void TestPlayRound()
    {
        var deck = new Deck();
        deck.Clear();
        var cardsToAdd = new List<Card>()
        {
            // Human's cards
            new (Suit.Spades, Value.Jack),
            new (Suit.Hearts, Value.Jack),
            new (Suit.Spades, Value.Six),
            new (Suit.Diamonds, Value.Jack),
            new (Suit.Hearts, Value.Six),

            // Computer cards
            new (Suit.Diamonds, Value.Six),
            new (Suit.Clubs, Value.Six),
            new (Suit.Spades, Value.Seven),
            new (Suit.Clubs, Value.Jack),
            new (Suit.Spades, Value.Nine),

            // Cards for human to draw
            new (Suit.Hearts, Value.Queen),
            new (Suit.Spades, Value.King)
        };

        cardsToAdd.ForEach(card => deck.Add(card));

        var gameState = new GameState("Chell", new List<string>() {"Computer"}, deck);

        var human = gameState.HumanPlayer;
        var computer = gameState.Opponents.First();

        Assert.AreEqual("Chell", human.Name);
        Assert.AreEqual(5, human.Hand.Count());
        Assert.AreEqual("Computer", computer.Name);
        Assert.AreEqual(5, computer.Hand.Count());

        var message = gameState.PlayRound(human, computer, Value.Jack, deck);
        Assert.AreEqual("Chell asked Computer for Jacks" + Environment.NewLine + "Computer has 1 Jack card" +  Environment.NewLine,
            message);
        Assert.AreEqual(1, human.Books.Count());
        Assert.AreEqual(2, human.Hand.Count());
        Assert.AreEqual(0, computer.Books.Count());
        Assert.AreEqual(4, computer.Hand.Count());

        message = gameState.PlayRound(computer, human, Value.Six, deck);
        Assert.AreEqual("Computer asked Chell for Sixes" + Environment.NewLine + "Chell has 2 Six cards" + Environment.NewLine,
            message);
        Assert.AreEqual(1, human.Books.Count());
        Assert.AreEqual(2, human.Hand.Count());
        Assert.AreEqual(1, computer.Books.Count());
        Assert.AreEqual(2, computer.Hand.Count());

        message = gameState.PlayRound(human, computer, Value.Queen, deck);
        Assert.AreEqual("Chell asked Computer for Queens" + Environment.NewLine + "The stock is out of cards" + Environment.NewLine,
            message);
        Assert.AreEqual(1, human.Books.Count());
        Assert.AreEqual(2, human.Hand.Count());
    }

    [TestMethod]
    public void TestCheckForAWinner()
    {
        var computerPlayerNames = new List<string>()
        {
            "Computer1",
            "Computer2",
            "Computer3"
        };

        var emptyDeck = new Deck();
        emptyDeck.Clear();
        var gameState = new GameState("Human", computerPlayerNames, emptyDeck);
        var actual = gameState.CheckForWinner();
        Assert.AreEqual("The winners are Human and Computer1 and Computer2 and Computer3" + Environment.NewLine,
            actual);

    }
}
