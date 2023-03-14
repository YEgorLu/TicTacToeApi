namespace TicTacToeApi.Models
{
    public class Point
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public PointValue Value { get; set; }
        public int TableId { get; set; }
        public Table Table { get; set; } = null!;
    }
}
