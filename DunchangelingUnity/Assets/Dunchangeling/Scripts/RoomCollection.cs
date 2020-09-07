using System.Collections;
using System.Collections.Generic;

public class RoomCollection
{
    List<int> concatenatedRooms;

    public RoomCollection()
    {
        concatenatedRooms = new List<int>();
    }

    public void AddRoom(int[] newRoom, int width, int height)
    {
        foreach(var i in newRoom)
        {
            if(i < 0)
            {
                throw new System.Exception();
            }
        }

        // [arraySize, width, height, rooooooooooom, widht, height, ...]
        concatenatedRooms.Add(width);
        concatenatedRooms.Add(height);
        concatenatedRooms.AddRange(newRoom);
    }

    public int[] ConvertToIntArray()
    {
        int[] concatenatedRoomsIntArr = new int[concatenatedRooms.Count + 1];
        concatenatedRoomsIntArr[0] = concatenatedRooms.Count;
        for(int i = 0; i < concatenatedRooms.Count; i++)
        {
            concatenatedRoomsIntArr[i + 1] = concatenatedRooms[i];
        }

        return concatenatedRoomsIntArr;
    }
}
