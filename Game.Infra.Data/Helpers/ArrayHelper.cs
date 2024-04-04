using System.Text;

namespace Game.Infra.Data.Helpers
{
    public static class ArrayHelper
    {
        public static int[,] DeserializeFromStringTo2DArray(string? twoDimensionalStringArray)
        {
            if (string.IsNullOrEmpty(twoDimensionalStringArray))
                return new int[0, 0];

            var rows = twoDimensionalStringArray.Split('|').Select(row => row.Split(',').Select(int.Parse).ToArray()).ToArray();
            var rowCount = rows.Length;
            var columnCount = rows[0].Length;
            var result = new int[rowCount, columnCount];

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    result[row, column] = rows[row][column];
                }
            }

            return result;
        }

        public static string? SerializeFrom2DArrayToString(int[,] matrix)
        {
            if (matrix == null)
                return null;

            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var sb = new StringBuilder();

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    sb.Append(matrix[row, column]);
                    if (column < columnCount - 1)
                        sb.Append(',');
                }

                if (row < rowCount - 1)
                    sb.Append('|');
            }

            return sb.ToString();
        }

        /// <summary>
        /// // Check if the array is non-rectangular
        /// </summary>
        /// <param name="array">The selected array</param>
        /// <returns>Returns <code>true</code> if the array is rectangular</returns>
        public static bool IsRectangular(int[,] array)
        {
            try
            {
                int rows = array.GetLength(0);
                int cols = array.GetLength(1);

                if (rows == 0 || cols == 0)
                    return false;

                for (int row = 1; row < rows; row++)
                    if (array.GetLength(1) != cols)
                        return false;

                // Array is valid
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
