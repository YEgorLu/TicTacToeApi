namespace TicTacToeApi.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public PointValue Next { get; set; }
        public int UnusedPoints { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;
        public List<Point> Points { get; set; } = null!;
    }
}
