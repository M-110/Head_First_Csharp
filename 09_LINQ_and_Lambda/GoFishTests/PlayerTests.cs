using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoFish;
using System.Collections.Generic;
using System.Linq;

namespace GoFishTests;

[TestClass]
public class PlayerTests
{
    [TestMethod]
    public void TestGetNextHand()
    {
        var player = new Player("Chell", new List<Card>());
        player.GetNextHand(new());
        CollectionAssert.AreEqual(
            new Deck().Take(5).Select(card => card.ToString()).ToList(),
            player.Hand.Select(Card => Card.ToString()).ToList());
    }

    [TestMethod]
    public void TestDoYouHaveAny()
    {
        IEnumerable<Card> cards = new List<Card>()
        {
            new Card(Suit.Spades, Value.Jack),
            new Card(Suit.Clubs, Value.Three),
            new Card(Suit.Hearts, Value.Three),
            new Card(Suit.Diamonds, Value.Four),
            new Card(Suit.Diamonds, Value.Three),
            new Card(Suit.Clubs, Value.Jack)
        };

        var player = new Player("Chell", cards);

        var threes = player.DoYouHaveAny(Value.Three, new())
            .Select(card => card.ToString())
            .ToList();

        var expected = new List<string>()
        {
            "Three of Diamonds",
            "Three of Hearts",
            "Three of Clubs"
        };

        CollectionAssert.AreEqual(expected, threes);
        Assert.AreEqual(3, player.Hand.Count());

        expected = new List<string>()
        {
            "Jack of Spades",
            "Jack of Clubs"
        };

        var jacks = player.DoYouHaveAny(Value.Jack, new())
            .Select(card => card.ToString()).ToList();
        CollectionAssert.AreEqual(expected, jacks);

        Assert.AreEqual("Chell has 1 card and 0 books" + Environment.NewLine, player.Status);
    }

    [TestMethod]
    public void TestAddCardsAndPullOutBooks()
    {
        IEnumerable<Card> cards = new List<Card>()
        {
            new Card(Suit.Spades, Value.Jack),
            new Card(Suit.Clubs, Value.Three),
            new Card(Suit.Hearts, Value.Jack),
            new Card(Suit.Hearts, Value.Three),
            new Card(Suit.Diamonds, Value.Four),
            new Card(Suit.Diamonds, Value.Jack),
            new Card(Suit.Clubs, Value.Jack)
        };

        var player = new Player("Chell", cards);

        Assert.AreEqual(0, player.Books.Count());

        var cardsToAdd = new List<Card>()
        {
            new Card(Suit.Diamonds, Value.Three),
            new Card(Suit.Spades, Value.Three)
        };

        player.AddCardsAndPullOutBooks(cardsToAdd);

        var expected = new List<Value>()
        {
            Value.Three, Value.Jack
        };

        var books = player.Books.ToList();

        CollectionAssert.AreEqual(expected, books);

        var expectedStrings = new List<string>() { "Four of Diamonds" };
        var hand = player.Hand.Select(card => card.ToString()).ToList();
        CollectionAssert.AreEqual(expectedStrings, hand);

        Assert.AreEqual("Chell has 1 card and 2 books" + Environment.NewLine, player.Status);

    }

    [TestMethod]
    public void TestDrawCard()
    {
        var player = new Player("Chell", new List<Card>());
        player.DrawCard(new());
        Assert.AreEqual(1, player.Hand.Count());
        Assert.AreEqual("Ace of Diamonds", player.Hand.First().ToString());
    }

    [TestMethod]
    public void TestRandomValueFromHand()
    {
        var player = new Player("Chell", new Deck());
        Player.Random = new MockRandom() { ValueToReturn = 0 };
        Assert.AreEqual("Ace", player.RandomValueFromHand().ToString());
        Player.Random = new MockRandom() { ValueToReturn = 4 };
        Assert.AreEqual("Two", player.RandomValueFromHand().ToString());
        Player.Random = new MockRandom() { ValueToReturn = 8 };
        Assert.AreEqual("Three", player.RandomValueFromHand().ToString());
    }
}

/// <summary>
/// Mock Random for testing which always returns the same value.
/// </summary>
public class MockRandom : System.Random
{
public int ValueToReturn { get; set; } = 0;
public override int Next() => ValueToReturn;
public override int Next(int maxValue) => ValueToReturn;
public override int Next(int minValue, int maxValue) => ValueToReturn;
}