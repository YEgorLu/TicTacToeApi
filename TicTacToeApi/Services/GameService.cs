using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TicTacToeApi.Data;
using TicTacToeApi.Models;
using TicTacToeApi.Models.DTO;
using TicTacToeApi.Models.Extensions;

namespace TicTacToeApi.Services
{
    public class GameService : IGameService
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public GameService(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<NextMoveDTO> CreateGame(string player1, int size, PointValue player1Value)
        {

            if (size < 3)
                throw new ArgumentException("Size is invalid.");

            var player2Value = GetOppositeValue(player1Value);
            var newGame = new Game { Player1 = player1, Player1Value = player1Value, Player2Value = player2Value };
            var a = await context.AddAsync(newGame);
            var pointsCount = size * size;
            var newTable = new Models.Table
            {
                Game = newGame,
                GameId = a.Entity.Id,
                Next = PointValue.Cross,
                Size = size,
                UnusedPoints = pointsCount
            };
            var b = await context.AddAsync(newTable);
            var points = Enumerable.Range(0, pointsCount).Select(i => new Point
            {
                Table = newTable,
                TableId = b.Entity.Id,
                X = i % size,
                Y = (int)(i / size),
                Value = PointValue.Empty
            });
            await context.AddRangeAsync(points);
            await context.SaveChangesAsync();
            return new NextMoveDTO { GameId = a.Entity.Id, GameEnd = false, NextValue = player1Value, Winner = null };
        }

        public async Task EndGame(int gameId)
        {
            var game = await context.Games.FindAsync(gameId)
                ?? throw new InvalidOperationException(string.Format("Game {0} does not exist", gameId));
            context.Games.Remove(game);
            await context.SaveChangesAsync();
        }

        public async Task<GameInfoDTO> GetGameInfo(int gameId)
        {
            var game = await context.Games
                .Include(g => g.Table)
                .Include(g => g.Table.Points)
                .FirstOrDefaultAsync(g => g.Id == gameId)
                ?? throw new InvalidOperationException(string.Format("Game {0} does not exist", gameId));
            return mapper.Map<Game, GameInfoDTO>(game);
        }

        public async Task<NextMoveDTO> MakeMove(int gameId, int x, int y)
        {
            var game = await context.Games
                .Include(g => g.Table.Points)
                .FirstOrDefaultAsync(g => g.Id == gameId)
                ?? throw new InvalidOperationException(string.Format("Game {0} does not exist", gameId));

            if (x < 0 || x >= game.Table.Size || y < 0 || y >= game.Table.Size)
                throw new ArgumentOutOfRangeException(string.Format("Point [{0}, {1}] is out of table range.", x, y));

            var point = game.Table.Points.FirstOrDefault(p => p.X == x && p.Y == y);
            point!.Value = game.Table.Next;
            game.Table.Next = GetOppositeValue(game.Table.Next);
            game.Table.UnusedPoints--;
            await context.SaveChangesAsync();

            var winner = game.Table.FindWinner();
            var gameEnd = game.Table.UnusedPoints == 0 || winner != PointValue.Empty;
            var nextValue = game.Table.Next;
            if (gameEnd)
                await EndGame(gameId);

            return new NextMoveDTO { GameId = gameId, GameEnd = gameEnd, NextValue = nextValue, Winner = winner };
        }

        public async Task<GameInfoDTO> ConnectToGame(int gameId, string player2)
        {
            var game = await context.Games
                .Include(g => g.Table)
                .Include(g => g.Table.Points)
                .FirstOrDefaultAsync(g => g.Id == gameId)
                ?? throw new InvalidOperationException(string.Format("Game {0} does not exist", gameId));
            game.Player2 = player2;
            context.SaveChanges();
            return mapper.Map<GameInfoDTO>(game);
        }

        public async Task<GameInfoDTO> FindGameByIp(string ip)
        {
            var game = await context.Games.
                Include(g => g.Table)
                .Include(g => g.Table.Points)
                .FirstOrDefaultAsync(g => g.Player1 == ip || g.Player2 == ip)
                ?? throw new InvalidOperationException("You have 0 games");
            return mapper.Map<GameInfoDTO>(game);
        }

        private static PointValue GetOppositeValue(PointValue cur)
        {
            var pointVals = Enum.GetValues<PointValue>();
            return pointVals[pointVals.Length - (int)cur];
        }

    }
}
