namespace TicTacToeApi.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string? Player1 { get; set; }
        public string? Player2 { get; set; }
        public PointValue Player1Value { get; set; }
        public PointValue Player2Value { get; set; }
        public string? Winner { get; set; }
        public Table Table { get; set; } = null!;

    }
}
