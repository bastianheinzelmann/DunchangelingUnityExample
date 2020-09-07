using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    const int EMPTY = 0;
    const int FILLED = 1;
    const int HAZARD = 5;
    const int DOOR = 2;
    const int END_ROOM = 2;
    const int START_ROOM = 3;
    const int SPECIAL_ROOM = 4;

    public GameObject DoorPrefab;
    public GameObject FloorPrefab;
    public GameObject LavaFloorPrefab;
    public GameObject WallPrefab;
    public GameObject PlayerPrefab;
    public GameObject GoalPrefab;

    public DungeonProperties DungeonProperties;
    public GeneticAlgorithmProperties GeneticAlgorithmProperties;

    public float floorSize = 1.0f;

    DungeonGrid dungeonGrid;
    RoomCollection roomCollection;

    public float FloorOffset;

    void CreateRooms()
    {
        int[] room =
        {
            0, 0, 1, 1,
            0, 0, 1, 1,
            1, 1, 1, 1,
            1, 1, 1, 1
        };

        int[] room2 =
        {
            1, 1,
            1, 1
        };

        int[] room3 =
        {
            1, 1, 1,
            1, 1, 1,
            1, 3, 1,
            1, 1, 1,
            1, 1, 1
        };

        int[] room4 =
        {
            1, 1, 1, 1, 1,
            1, 3, 3, 3, 1,
            1, 1, 1, 1, 1
        };

        int[] room5 = 
        {
            1, 1, 1, 1,
            1, 1, 1, 1,
            0, 0, 1, 1,
            0, 0, 1, 1
        };

        int[] room6 = 
        {
            1, 1, 0, 0,
            1, 1, 0, 0,
            1, 1, 1, 1,
            1, 1, 1, 1,
            1, 1, 0, 0,
            1, 1, 0, 0
        };

        int[] room7 =
        {
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1
        };

        int[] room8 = 
        {
            1, 1, 1,
            1, 0, 0,
            1, 0, 0,
            1, 0, 0,
            1, 0, 0,
            1, 0, 0,
            1, 0, 0,
            1, 1, 1,
        };

        roomCollection.AddRoom(room, 4, 4);
        roomCollection.AddRoom(room2, 2, 2);
        roomCollection.AddRoom(room3, 3, 5);
        roomCollection.AddRoom(room4, 5, 3);
        roomCollection.AddRoom(room5, 4, 4);
        roomCollection.AddRoom(room6, 4, 6);
    }

    void GenerateDungeon()
    {
        roomCollection = new RoomCollection();
        CreateRooms();

        bool playerInstantiated = false;

        dungeonGrid = DunchangelingWrapper.GenerateLayoutWrapper(DungeonProperties, roomCollection, GeneticAlgorithmProperties);
        for(int y = 0; y < dungeonGrid.YSize; y++)
        {
            for (int x = 0; x < dungeonGrid.XSize; x++)
            {              
                if(dungeonGrid[x, y, (int)DungeonData.Tile] != EMPTY)
                {
                    GameObject floorTile;

                    switch(dungeonGrid[x, y, (int)DungeonData.Tile])
                    {
                        case FILLED: floorTile = FloorPrefab; break;
                        case HAZARD: floorTile = LavaFloorPrefab; break;
                        case SPECIAL_ROOM: floorTile = FloorPrefab; break;
                        case END_ROOM:
                            {
                                floorTile = FloorPrefab;
                                Instantiate(GoalPrefab, new Vector3(x * floorSize, 0.5f, -y * floorSize), Quaternion.identity);
                                break;
                            }
                        case START_ROOM:
                            {
                                floorTile = FloorPrefab;
                                if (!playerInstantiated)
                                {
                                    Instantiate(PlayerPrefab, new Vector3(x * floorSize, 0.5f, -y * floorSize), Quaternion.identity);
                                    playerInstantiated = true;
                                }
                                break;
                            }
                        default: floorTile = FloorPrefab; break;
                    }

                    var tile = Instantiate(floorTile, new Vector3(x * floorSize, 0, -y * floorSize), Quaternion.identity);
                    tile.transform.parent = this.transform;

                    switch (dungeonGrid[x, y, (int)DungeonData.North])
                    {
                        case 1:
                            {
                                var wall = Instantiate(WallPrefab, new Vector3(x * floorSize, 0.5f, -y * floorSize + 1.0f), Quaternion.identity, tile.transform);
                                break;
                            }
                        case 2:
                            {
                                var wall = Instantiate(DoorPrefab, new Vector3(x * floorSize, 0.5f, -y * floorSize + 1.0f), Quaternion.identity, tile.transform);
                                break;
                            }
                    }

                    switch (dungeonGrid[x, y, (int)DungeonData.East])
                    {
                        case 1:
                            {
                                var wall = Instantiate(WallPrefab, new Vector3(x * floorSize + 1.0f, 0.5f, -y * floorSize), Quaternion.Euler(new Vector3(0, 90, 0)), tile.transform);
                                break;
                            }
                        case 2:
                            {
                                var wall = Instantiate(DoorPrefab, new Vector3(x * floorSize + 1.0f, 0.5f, -y * floorSize), Quaternion.Euler(new Vector3(0, 90, 0)), tile.transform);
                                break;
                            }
                    }

                    switch (dungeonGrid[x, y, (int)DungeonData.South])
                    {
                        case 1:
                            {
                                var wall = Instantiate(WallPrefab, new Vector3(x * floorSize, 0.5f, -y * floorSize), Quaternion.identity, tile.transform);
                                break;
                            }
                        case 2:
                            {
                                var wall = Instantiate(DoorPrefab, new Vector3(x * floorSize, 0.5f, -y * floorSize), Quaternion.identity, tile.transform);
                                break;
                            }
                    }

                    switch (dungeonGrid[x, y, (int)DungeonData.West])
                    {
                        case 1:
                            {
                                var wall = Instantiate(WallPrefab, new Vector3(x * floorSize, 0.5f, -y * floorSize), Quaternion.Euler(new Vector3(0, 90, 0)), tile.transform);
                                break;
                            }
                        case 2:
                            {
                                var wall = Instantiate(DoorPrefab, new Vector3(x * floorSize, 0.5f, -y * floorSize), Quaternion.Euler(new Vector3(0, 90, 0)), tile.transform);
                                break;
                            }
                    }
                }
            }
        }
    }

    void Awake()
    {
        GenerateDungeon();
    }
}
