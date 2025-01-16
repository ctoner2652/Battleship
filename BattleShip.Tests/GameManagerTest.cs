using Battleship.UI;
using Battleship.UI.GameLogic;
using Battleship.UI.Implementations;
using Battleship.UI.Interfaces;
using NUnit.Framework;

namespace BattleShip.Tests
{
    [TestFixture]
    public class GameManagerTest
    {
        [Test]
        public void TestShipOverlap_True()
        {
            IPlayer player1 = new HumanPlayer()
            {
                Name = "Test"
            };
            IPlayer player2 = new HumanPlayer();
            GameManager gm = new GameManager(player1,player2);
            player1.PlayerShips[0].ShipCoordinates = ["A1", "B1", "C1", "D1", "E1"];
            String[] placement = ["A2", "B2", "C2", "D2"];
            var result = gm.ValidateShipPlacement(placement, player1.PlayerShips);
            Assert.That(result, Is.True);
        }
        [Test]
        public void TestShipOverlap_False()
        {
            IPlayer player1 = new HumanPlayer()
            {
                Name = "Test"
            };
            IPlayer player2 = new HumanPlayer();
            GameManager gm = new GameManager(player1, player2);
            player1.PlayerShips[0].ShipCoordinates = ["A1", "B1", "C1", "D1", "E1"];
            String[] placement = ["A1", "B1", "C1", "D1"];
            var result = gm.ValidateShipPlacement(placement, player1.PlayerShips);
            Assert.That(result, Is.False);
        }
        [Test]
        public void TestShipOffGrid_False()
        {
            IPlayer player1 = new HumanPlayer()
            {
                Name = "Test"
            };
            IPlayer player2 = new HumanPlayer();
            GameManager gm = new GameManager(player1, player2);
            
            String[] placement = ["J10", "K10", "L10", "M10", "N10"];
            var result = gm.ValidateShipPlacement(placement, player1.PlayerShips);
            Assert.That(result, Is.False);
        }
        public void TestShipOffGrid_True()
        {
            IPlayer player1 = new HumanPlayer()
            {
                Name = "Test"
            };
            IPlayer player2 = new HumanPlayer();
            GameManager gm = new GameManager(player1, player2);

            String[] placement = ["A1", "B1", "C1", "D1", "E1"];
            var result = gm.ValidateShipPlacement(placement, player1.PlayerShips);
            Assert.That(result, Is.True);
        }
        [Test]
        public void TestShotHit()
        {
            IPlayer player1 = new TestPlayer();
            IPlayer player2 = new TestPlayer()
            {
                ManualMove = "A1"
            };
            GameManager gm = new GameManager(player1, player2);
            player1.PlayerShips[0].ShipCoordinates = ["A1", "B1", "C1", "D1", "E1"];
            var result = gm.PlayRound(player2);
            Assert.That(result, Is.EqualTo(MoveResult.Hit));
        }
        [Test]
        public void TestShotMiss()
        {
            IPlayer player1 = new TestPlayer();
            IPlayer player2 = new TestPlayer()
            {
                ManualMove = "A3"
            };
            GameManager gm = new GameManager(player1, player2);
            player1.PlayerShips[0].ShipCoordinates = ["A1", "B1", "C1", "D1", "E1"];
            var result = gm.PlayRound(player2);
            Assert.That(result, Is.EqualTo(MoveResult.Miss));
        }
        [Test]
        public void TestShotSunk()
        {
            IPlayer player1 = new TestPlayer();
            IPlayer player2 = new TestPlayer()
            {
                ManualMove = "A1"
            };
            GameManager gm = new GameManager(player1, player2);
            player1.PlayerShips[0].ShipCoordinates = ["A1", "B1", "C1", "D1", "E1"];
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "B1";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "C1";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "D1";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "E1";
            var result = gm.PlayRound(player2);
            Assert.That(result, Is.EqualTo(MoveResult.Sunk));
        }
        [Test]
        public void TestGameOver()
        {
            IPlayer player1 = new TestPlayer();
            IPlayer player2 = new TestPlayer()
            {
                ManualMove = "A1"
            };
            GameManager gm = new GameManager(player1, player2);
            player1.PlayerShips[0].ShipCoordinates = ["A1", "B1", "C1", "D1", "E1"];
            player1.PlayerShips[1].ShipCoordinates = ["A2", "B2", "C2", "D2"];
            player1.PlayerShips[2].ShipCoordinates = ["A3", "B3", "C3"];
            player1.PlayerShips[3].ShipCoordinates = ["A4", "B4", "C4"];
            player1.PlayerShips[4].ShipCoordinates = ["A5", "B5"];
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "B1";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "C1";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "D1";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "E1";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "A2";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "B2";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "C2";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "D2";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "A3";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "B3";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "C3";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "A4";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "B4";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "C4";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "A5";
            gm.PlayRound(player2);
            ((TestPlayer)player2).ManualMove = "B5";
            gm.PlayRound(player2);
            var result1 = gm.GameOver(player1);
            var result2 = gm.GameOver(player2);
            Assert.That(result2, Is.True);
            Assert.That(result1, Is.False);
        }

    }
}
