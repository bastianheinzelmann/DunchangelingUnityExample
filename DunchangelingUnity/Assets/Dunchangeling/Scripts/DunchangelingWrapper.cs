using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class DunchangelingWrapper : MonoBehaviour
{

    //[DllImport("DunchangelingUnity")]
    //public static extern int addWrapper(int num1, int num2);
    //[DllImport("DunchangelingUnity")]
    //public static extern int multiplyWrapper(int num1, int num2);
    //[DllImport("DunchangelingUnity")]
    //public static extern int substractWrapper(int num1, int num2);
    //[DllImport("DunchangelingUnity")]
    //public static extern int divideWrapper(int num1, int num2);
    [DllImport("DunchangelingUnity")]
    public static extern int generateLayout(out IntPtr p, out int xSize, out int ySize, out int numData, DungeonProperties dprops, int[] rooms, GeneticAlgorithmProperties gaProps);

    public static DungeonGrid GenerateLayoutWrapper(DungeonProperties dprops, RoomCollection roomCollection, GeneticAlgorithmProperties gaProps)
    {
        int xSize;
        int ySize;
        int numData;
        IntPtr ptr;

        int arraySize = generateLayout(out ptr, out xSize, out ySize, out numData, dprops, roomCollection.ConvertToIntArray(), gaProps);

        Debug.Log("Grid size: " + arraySize);
        Debug.Log("Grid size: " + xSize * ySize * numData);       

        int[] layout = new int[xSize * ySize * numData];
        Marshal.Copy(ptr, layout, 0, xSize * ySize * numData);
        Marshal.FreeHGlobal(ptr);

        //for (int i = 0; i < size; i++)
        //{
        //    Debug.Log("Element " + i + ": " + layout[i]);
        //}

        return new DungeonGrid(layout, xSize, ySize, numData);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
