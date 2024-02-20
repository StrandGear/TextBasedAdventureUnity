using Graphs2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tree;

[CreateAssetMenu(menuName = "TextAdventure/Room")]
public class Room : ScriptableObject
{
    [TextArea]
    public string description;
    public string roomName;
    public Sprite roomPicture;

    public Exit[] exits;

    [HideInInspector]
    public GraphNode2 roomNode;

    public int currentEventNodeID;

    public bool hasCharacter;
    public Character localCharacter;


}
