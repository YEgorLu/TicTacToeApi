namespace TicTacToeApi.Models.DTO
{
    public class GameInfoDTO
    {
        public int GameId { get; set; }
        public PointValue NextValue { get; set; }
        public string? Player1 { get; set; }
        public string? Player2 { get; set; }
        public PointValue Player1Value { get; set; }
        public PointValue Player2Value { get; set; }
        public string? Winner { get; set; }

        public TableDTO Table { get; set; } = null!;
    }
}
