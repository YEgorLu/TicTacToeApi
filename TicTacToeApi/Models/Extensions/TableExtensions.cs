namespace TicTacToeApi.Models.Extensions
{
    public static class TableExtensions
    {
        public static PointValue FindWinner(this Table table)
        {
            var winner = table.FindHorizontalWinner();
            if (winner == PointValue.Empty)
                winner = table.FindVerticalWinner();
            if (winner == PointValue.Empty)
                winner = table.FindDiagonalWinner();
            return winner;
        }

        private static PointValue FindDiagonalWinner(this Table table)
        {
            var points = table.Points;
            var size = table.Size;
            PointValue last = points[0].Value;
            var found = true;
            for (var i = 0; i < points.Count; i += size + 1)
            {
                if (points[i].Value != last)
                {
                    found = false;
                    break;
                }
            }
            if (found)
                return last;

            last = points[size - 1].Value;
            for (var i = size - 1; i <= points.Count; i += size - 1)
            {
                if (points[i].Value != last)
                    return PointValue.Empty;
            }

            return last;
        }

        private static PointValue FindVerticalWinner(this Table table)
        {
            var points = table.Points;
            var size = table.Size;
            for (var i = 0; i < size; i++)
            {
                var foundWinner = true;
                PointValue last = points[size * i].Value;
                for (var j = i + size; j < points.Count; j += size)
                {
                    if (points[j].Value != last)
                    {
                        foundWinner = false;
                        break;
                    }
                }
                if (foundWinner)
                    return last;
            }

            return PointValue.Empty;
        }

        private static PointValue FindHorizontalWinner(this Table table)
        {
            var points = table.Points;
            var size = table.Size;
            for (var i = 0; i < size; i++)
            {
                var foundWinner = true;
                PointValue last = points[size * i].Value;
                for (var j = 1; j < size; j++)
                {
                    if (points[size * i + j].Value != last)
                    {
                        foundWinner = false;
                        break;
                    }
                }
                if (foundWinner)
                    return last;
            }

            return PointValue.Empty;
        }
    }
}
