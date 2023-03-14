namespace TicTacToeApi.Models.DTO
{
    public class TableDTO
    {
        public int Size { get; set; }
        public int UnusedPoints { get; set; }
        public List<PointDTO> Points { get; set; } = null!;
    }
}
