using System.Runtime.InteropServices;

public enum MutationType { EMT_EDGE_BASED = 0, EMT_PRODUCTION_BASED = 1 }
public enum InitMode { EIM_PATH = 0, EIM_RANDOM = 1 }

[StructLayout(LayoutKind.Sequential), System.Serializable]
public class GeneticAlgorithmProperties
{
    public int populationSize;
    public int maxGenerations;
    public int elitismRate;
    public int crossoverRate;
    public int convergenceBorder;
    public MutationType mutationType;
    public InitMode initMode;
    [MarshalAs(UnmanagedType.U1)]
    public bool doCrossover;
}
