namespace GHTweaks.Models
{
    public class GridSize
    {
        public int RowCount { get; set; }

        public int ColumnCount { get; set; }


        public GridSize() : this(5,10) { }

        public GridSize(int rowCount, int columnCount) 
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
        }

        public override string ToString() => $"RowCount: {RowCount}, ColumnCount: {ColumnCount}";
    }
}
