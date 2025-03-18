using System.Collections;

namespace GHTweaks.Models
{
    public class GridSize : IEqualityComparer
    {
        public int RowCount { get; set; }

        public int ColumnCount { get; set; }


        public GridSize() : this(5,10) { }

        public GridSize(int rowCount, int columnCount) 
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
        }



        public new bool Equals(object x, object y)
        {
            if (x is GridSize gridSize1 && y is GridSize gridSize2)
                return gridSize1.RowCount == gridSize2.RowCount && gridSize1.ColumnCount == gridSize2.ColumnCount;
            
            return false;
        }

        public int GetHashCode(object obj) => obj.GetHashCode();

        public override string ToString() => $"RowCount: {RowCount}, ColumnCount: {ColumnCount}";
    }
}
