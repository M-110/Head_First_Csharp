using Microsoft.VisualStudio.TestTools.UnitTesting;
using JimmyLinq;
using System.Collections.Generic;
using System.Linq;

namespace JimmyLinqUnitTests
{
    [TestClass]
    public class ComicAnalyzerTests
    {
        IEnumerable<Comic> testComics = new[]
        {
            new Comic("Issue 1", 1),
            new Comic("Issue 2", 2),
            new Comic("Issue 3", 3)
        };

        [TestMethod]
        public void ComicAnalyzer_Should_Group_Comics()
        {
            var testPrices = new Dictionary<int, decimal>()
            {
                {1, 20M },
                {2, 10M },
                {3, 1000M }
            };

            var groups = ComicAnalyzer.GroupComicsByPrice(testComics, testPrices);

            Assert.AreEqual(2, groups.Count());
            Assert.AreEqual(PriceRange.Cheap, groups.First().Key);
            Assert.AreEqual(2, groups.First().First().Issue);
            Assert.AreEqual("Issue 2", groups.First().First().Name);
        }

        [TestMethod]
        public void ComicAnalyzer_Should_Generate_A_List_Of_Reviews()
        {
            var testReviews = new[]
            {
                new Review(1, Critics.MuddyCritic, 14.5f),
                new Review(1, Critics.RottenTornadoes, 59.93f),
                new Review(2, Critics.MuddyCritic, 40.3f),
                new Review(2, Critics.RottenTornadoes, 95.11f),
                new Review(2, Critics.RottenTornadoes, 95.11f)
            };

            var expectedResults = new[]
            {
                "MuddyCritic rated #1 'Issue 1': 14.50",
                "RottenTornadoes rated #1 'Issue 1': 59.93",
                "MuddyCritic rated #2 'Issue 2': 40.30",
                "RottenTornadoes rated #2 'Issue 2': 95.11",
                "RottenTornadoes rated #2 'Issue 2': 95.11",
            };

            var actualResult = ComicAnalyzer.GetReviews(testComics, testReviews).ToList();
            CollectionAssert.AreEqual(expectedResults, actualResult);
        }
    }
}