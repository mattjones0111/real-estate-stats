using System.Linq;
using ConsoleApp;
using FundaApiClient.Model;
using NUnit.Framework;

namespace Tests
{
    public class AggregationTests
    {
        [Test]
        public void GivenPropertyData_CanCreateAggregateCount()
        {
            // arrange
            var properties = new[]
            {
                new Property { MakelaarId = 1, MakelaarNaam = "Agent A" },
                new Property { MakelaarId = 1, MakelaarNaam = "Agent A" },
                new Property { MakelaarId = 1, MakelaarNaam = "Agent A" },
                new Property { MakelaarId = 1, MakelaarNaam = "Agent A" },
                new Property { MakelaarId = 2, MakelaarNaam = "Agent B" },
                new Property { MakelaarId = 2, MakelaarNaam = "Agent B" },
                new Property { MakelaarId = 2, MakelaarNaam = "Agent B" },
                new Property { MakelaarId = 3, MakelaarNaam = "Agent C" },
                new Property { MakelaarId = 3, MakelaarNaam = "Agent C" },
                new Property { MakelaarId = 4, MakelaarNaam = "Agent D" }
            };

            // act
            var actual = properties.GetTopAgents().ToList();

            // assert
            Assert.IsNotNull(actual);

            Assert.AreEqual(4, actual.Count);

            Assert.AreEqual(4, actual[0].Count);
            Assert.AreEqual(3, actual[1].Count);
            Assert.AreEqual(2, actual[2].Count);
            Assert.AreEqual(1, actual[3].Count);

            Assert.AreEqual("Agent A", actual[0].Object.Name);
            Assert.AreEqual("Agent B", actual[1].Object.Name);
            Assert.AreEqual("Agent C", actual[2].Object.Name);
            Assert.AreEqual("Agent D", actual[3].Object.Name);
        }
    }
}
