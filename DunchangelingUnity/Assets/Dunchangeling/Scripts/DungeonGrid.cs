public class DungeonGrid
{
    public int[] DungeonArray;
    public int XSize;
    public int YSize;
    public int NumData;

    public DungeonGrid(int[] array, int xSize, int ySize, int numData)
    {
        DungeonArray = array;
        XSize = xSize;
        YSize = ySize;
        NumData = numData;
    }

    public int this[int x, int y, int data]
    {
        get
        {
            return (DungeonArray[(XSize * NumData * y) + (x * NumData) + data]);
        }
        set
        {
            DungeonArray[(XSize * NumData * y) + (x * NumData) + data] = value;
        }
    }
}
