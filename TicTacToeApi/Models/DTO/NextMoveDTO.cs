namespace TicTacToeApi.Models.DTO
{
    public class NextMoveDTO
    {
        public int GameId { get; set; }
        public PointValue NextValue { get; set; }
        public bool GameEnd { get; set; }
        public PointValue? Winner { get; set; }
    }
}
