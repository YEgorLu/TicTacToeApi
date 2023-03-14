using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using System.Security.Claims;
using TicTacToeApi.Models;
using TicTacToeApi.Models.DTO;
using TicTacToeApi.Services;

namespace TicTacToeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicTacToeController : ControllerBase
    {
        private readonly IGameService gameService;

        public TicTacToeController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpPost]
        public async Task<ActionResult<NextMoveDTO>> CreateGame(int size, PointValue player1Value)
        {
            NextMoveDTO nextMove;
            try
            {
                nextMove = await gameService.CreateGame(GetIp(), size, player1Value);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(nextMove);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EndGame(int id)
        {
            try
            {
                await gameService.EndGame(id);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }

        [HttpGet("{gameId}")]
        public async Task<ActionResult<GameInfoDTO>> GetGameInfo(int gameId)
        {
            GameInfoDTO info;
            try
            {
                info = await gameService.GetGameInfo(gameId);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }

            return Ok(info);
        }

        [HttpPut("{gameId}")]
        public async Task<ActionResult<GameInfoDTO>> ConnectToGame(int gameId)
        {
            GameInfoDTO info;
            try
            {
                info = await gameService.ConnectToGame(gameId, GetIp());
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
            return Ok(info);
        }

        [HttpPost("{gameId}/move/")]
        public async Task<ActionResult<NextMoveDTO>> MakeMove(int gameId, int x, int y)
        {
            NextMoveDTO move;
            try
            {
                move = await gameService.MakeMove(gameId, x, y);
            }
            catch (Exception e)
            {
                if (e is ArgumentException || e is InvalidOperationException)
                    return BadRequest(e.Message);
                throw;
            }

            return Ok(move);
        }

        [HttpGet]
        public async Task<ActionResult<GameInfoDTO>> FindYourGame()
        {
            GameInfoDTO info;
            try
            {
                info = await gameService.FindGameByIp(GetIp());
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }

            return Ok(info);
        }

        private string GetIp()
        {
            var ip = Response.HttpContext.Connection.RemoteIpAddress?.ToString();
            if (ip == "::1")
                ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Last().ToString();
            return ip;
        }
    }
}
