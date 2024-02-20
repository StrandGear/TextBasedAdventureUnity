using Graphs2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphOfRooms : MonoBehaviour
{
    Graph2 roomsMap = new Graph2();
    public List<Room> allRooms;
    List<GraphNode2> nodes = new List<GraphNode2>();

    private void Awake()
    {
        foreach (Room room in allRooms)
        {
            GraphNode2 newNode = new GraphNode2(room.roomName);
            room.roomNode = newNode;
            roomsMap.addNode(room.roomNode);
            nodes.Add(room.roomNode);
        }

/*        foreach (Room room in allRooms)
        {
            foreach (Exit exit in room.exits)
            {
                roomsMap.addEdge(room.roomNode, exit.valueRoom.roomNode, exit.cost);
            }
        }*/

    }
    private void Start()
    {
         roomsMap.addEdge(nodes[0], nodes[1], 5); //farm vs town
         roomsMap.addEdge(nodes[0], nodes[2], 10); //farm vs academy
        roomsMap.addEdge(nodes[0], nodes[3], 12); //farm vs library

        roomsMap.addEdge(nodes[1], nodes[2], 3); //town vs academy
        roomsMap.addEdge(nodes[1], nodes[3], 6); //town vs library

        roomsMap.addEdge(nodes[2], nodes[3], 2); // academy vs library

        print(roomsMap.ToMatrix());
    }

    public void DisplayMap()
    {
        GetComponent<GameController>().LogStringWithReturn("Map: \n" + roomsMap.ToMatrix());
    }

}
