namespace GHTweaks.Serializable
{
    public class PocketGridConfig
    {
        public float CellSize 
        {
            get => cellSize;
            set
            {
                if (value > 20)
                    value = 20;

                cellSize = value;
            }
        }
        private float cellSize = -1;
    }
}
