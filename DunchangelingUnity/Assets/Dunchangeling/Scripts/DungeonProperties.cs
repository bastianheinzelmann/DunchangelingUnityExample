using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential), System.Serializable]
public struct DungeonProperties
{
    public int NumSpecialRooms;
    public int NumRooms;
    public int NumOpponents;
    
    [MarshalAs(UnmanagedType.U1)]
    public bool EndRoomDeadEnd;
    [MarshalAs(UnmanagedType.U1)]
    public bool SpecialIsDeadEnd;
    [MarshalAs(UnmanagedType.U1)]
    public bool FlankingRoutes;
    public int NumFlankingRoutes;
    public float BranchingFactor;
}
