using NUnit.Framework;
using TicTacToeApi.Data;
using TicTacToeApi.Models;
using TicTacToeApi.Services;
using System.Drawing;
using Point = TicTacToeApi.Models.Point;
using System.Reflection;

namespace TicTacToeApi.Tests
{
    [TestFixture]
    public class GetWinnerTest
    {
        private WebApplication _app;
        private GameService _gameService;

        [SetUp]
        public void SetUp()
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddDbContext<DataContext>();
            builder.Services.AddScoped<GameService, GameService>();
            _app = builder.Build();
            _gameService = _app.Services.GetService<GameService>()!;
        }

        private static List<Point> MakePointsList(int size, params PointValue[] points)
        {
            var pointsList = new List<Point>();
            int curX = 0;
            int curY = 0;
            for (var i = 0; i < points.Length; i++)
            {
                var point = new Point { Value = points[i], X = curX, Y = curY };
                curX++;
                if (curX >= size)
                {
                    curX = 0;
                    curY++;
                }
                pointsList.Add(point);
            }

            return pointsList;
        }

        [Test]
        [TestCase(PointValue.Cross, 4,
            PointValue.Cross, PointValue.Cross, PointValue.Cross, PointValue.Cross,
            PointValue.Cross, PointValue.Cross, PointValue.Cross, PointValue.Cross,
            PointValue.Cross, PointValue.Cross, PointValue.Cross, PointValue.Cross,
            PointValue.Cross, PointValue.Cross, PointValue.Cross, PointValue.Cross)]
        [TestCase(PointValue.Cross, 4,
            PointValue.Cross, PointValue.Circle, PointValue.Cross, PointValue.Cross,
            PointValue.Circle, PointValue.Cross, PointValue.Cross, PointValue.Cross,
            PointValue.Cross, PointValue.Cross, PointValue.Empty, PointValue.Cross,
            PointValue.Cross, PointValue.Cross, PointValue.Cross, PointValue.Cross)]
        [TestCase(PointValue.Empty, 4,
            PointValue.Cross, PointValue.Circle, PointValue.Cross, PointValue.Cross,
            PointValue.Circle, PointValue.Cross, PointValue.Cross, PointValue.Cross,
            PointValue.Cross, PointValue.Cross, PointValue.Empty, PointValue.Cross,
            PointValue.Cross, PointValue.Cross, PointValue.Cross, PointValue.Empty)]
        [TestCase(PointValue.Circle, 4,
            PointValue.Cross, PointValue.Circle, PointValue.Cross, PointValue.Cross,
            PointValue.Circle, PointValue.Cross, PointValue.Cross, PointValue.Cross,
            PointValue.Circle, PointValue.Circle, PointValue.Circle, PointValue.Circle,
            PointValue.Cross, PointValue.Cross, PointValue.Cross, PointValue.Empty)]
        public void CanGetHorizontalWinner(PointValue expectedWinner, int size, params PointValue[] points)
        {
            var winner = GetWinner("FindHorizontalWinner", size, points);
            Assert.AreEqual(expectedWinner, winner);
        }

        [Test]
        [TestCase(PointValue.Cross, 4,
            PointValue.Cross, PointValue.Circle, PointValue.Cross, PointValue.Cross,
            PointValue.Circle, PointValue.Cross, PointValue.Cross, PointValue.Cross,
            PointValue.Cross, PointValue.Cross, PointValue.Empty, PointValue.Cross,
            PointValue.Cross, PointValue.Empty, PointValue.Cross, PointValue.Cross)]
        [TestCase(PointValue.Empty, 4,
            PointValue.Cross, PointValue.Circle, PointValue.Cross, PointValue.Cross,
            PointValue.Circle, PointValue.Cross, PointValue.Cross, PointValue.Empty,
            PointValue.Cross, PointValue.Cross, PointValue.Empty, PointValue.Cross,
            PointValue.Cross, PointValue.Empty, PointValue.Cross, PointValue.Cross)]
        [TestCase(PointValue.Circle, 4,
            PointValue.Cross, PointValue.Circle, PointValue.Cross, PointValue.Cross,
            PointValue.Circle, PointValue.Circle, PointValue.Cross, PointValue.Empty,
            PointValue.Cross, PointValue.Circle, PointValue.Empty, PointValue.Cross,
            PointValue.Cross, PointValue.Circle, PointValue.Cross, PointValue.Cross)]
        public void CanGetVerticalWinner(PointValue expectedWinner, int size, params PointValue[] points)
        {
            var winner = GetWinner("FindVerticalWinner", size, points);
            Assert.AreEqual(expectedWinner, winner);
        }

        [Test]
        [TestCase(PointValue.Cross, 4,
            PointValue.Cross, PointValue.Circle, PointValue.Cross, PointValue.Cross,
            PointValue.Circle, PointValue.Cross, PointValue.Empty, PointValue.Cross,
            PointValue.Cross, PointValue.Cross, PointValue.Cross, PointValue.Empty,
            PointValue.Cross, PointValue.Empty, PointValue.Cross, PointValue.Cross)]
        [TestCase(PointValue.Cross, 4,
            PointValue.Cross, PointValue.Circle, PointValue.Cross, PointValue.Cross,
            PointValue.Circle, PointValue.Empty, PointValue.Cross, PointValue.Empty,
            PointValue.Cross, PointValue.Cross, PointValue.Empty, PointValue.Cross,
            PointValue.Cross, PointValue.Empty, PointValue.Cross, PointValue.Cross)]
        [TestCase(PointValue.Circle, 4,
            PointValue.Circle, PointValue.Circle, PointValue.Cross, PointValue.Cross,
            PointValue.Circle, PointValue.Circle, PointValue.Cross, PointValue.Empty,
            PointValue.Cross, PointValue.Circle, PointValue.Circle, PointValue.Cross,
            PointValue.Cross, PointValue.Circle, PointValue.Cross, PointValue.Circle)]
        [TestCase(PointValue.Circle, 4,
            PointValue.Cross, PointValue.Circle, PointValue.Cross, PointValue.Circle,
            PointValue.Circle, PointValue.Circle, PointValue.Circle, PointValue.Empty,
            PointValue.Cross, PointValue.Circle, PointValue.Circle, PointValue.Cross,
            PointValue.Circle, PointValue.Circle, PointValue.Cross, PointValue.Circle)]
        [TestCase(PointValue.Empty, 4,
            PointValue.Cross, PointValue.Circle, PointValue.Cross, PointValue.Circle,
            PointValue.Circle, PointValue.Circle, PointValue.Circle, PointValue.Empty,
            PointValue.Cross, PointValue.Empty, PointValue.Circle, PointValue.Cross,
            PointValue.Circle, PointValue.Circle, PointValue.Cross, PointValue.Circle)]
        public void CanGetDiagonalWinner(PointValue expectedWinner, int size, params PointValue[] points)
        {
            var winner = GetWinner("FindDiagonalWinner", size, points);
            Assert.AreEqual(expectedWinner, winner);
        }

        private PointValue GetWinner(string methodName, int size, PointValue[] points)
        {
            var pointsList = MakePointsList(size, points);
            var method = GetPrivateMethod(methodName);
            return (PointValue)(method?.Invoke(_gameService, new object[] { pointsList, size }))!;
        }

        private MethodInfo? GetPrivateMethod(string methodName)
        {
            return _gameService.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }
}
