using Microsoft.AspNetCore.Mvc;
using TicTacToeApi.Models;
using TicTacToeApi.Models.DTO;

namespace TicTacToeApi.Services
{
    public interface IGameService
    {
        Task<NextMoveDTO> CreateGame(string player1, int Size, PointValue player1Value);
        Task EndGame(int id);
        Task<GameInfoDTO> GetGameInfo(int gameId);
        Task<GameInfoDTO> ConnectToGame(int gameId, string player2);
        Task<GameInfoDTO> FindGameByIp(string ip);
        Task<NextMoveDTO> MakeMove(int gameId, int x, int y);
    }
}
