using System.Collections;
using System.Collections.Generic;
using Tree;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    public Room currentRoom;
    GameController controller;
    public Character character;
    public Sprite transparanetSpriteHolder;
    Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();

    //public DecisionTreeDictValue[] decisionTreeCopied; //try out list later
    public List<DecisionTreeDictValue> decisionTreeCopied = new List<DecisionTreeDictValue>();
    public DecisionTreeDictionary eventsDictionary;
    Dictionary<DecisionTreeNode, int> invertEventsDictionary = new Dictionary<DecisionTreeNode, int>();
    int currNodeId = 0;
    DecisionTreeNode currNode;
    public bool eventIsOver = false;
    Sprite _characterSprite;

    private void Awake()
    {
        controller = GetComponent<GameController>();
        /*        invertEventsDictionary.Clear();
                eventsDictionary.Clear();*/
    }

    public void UnpackExitsInRoom()
    {
        for (int i = 0; i < currentRoom.exits.Length; i++)
        {
            exitDictionary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);
            controller.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
        }
    }

    public void UnpackRoomEvent()
    {
        if (currentRoom.localCharacter == null || currentRoom.localCharacter.dialogueIsOver )
        {
            EventOver();
            return;
        }
        else
            eventIsOver = false;

        foreach (DecisionTreeDictValue _value in currentRoom.localCharacter.decisionTree)
            decisionTreeCopied.Add(_value);

        for (int i = 0; i < decisionTreeCopied.Count; i++)
        {
            DecisionTreeDictValue _dictValue = decisionTreeCopied[i];
            eventsDictionary.Add(i, _dictValue);
            DecisionTreeNode _node = _dictValue.treeNode;
            invertEventsDictionary.Add(_node, i);
        }

        for (int i = 1; i < eventsDictionary.Count; i++)
        {
            if (eventsDictionary[i].isLeftChild)
                eventsDictionary[eventsDictionary[i].parentID].treeNode.AddLeftNode(eventsDictionary[i].treeNode);
            else
                eventsDictionary[eventsDictionary[i].parentID].treeNode.AddRightNode(eventsDictionary[i].treeNode);
            //Debug.Log(eventsDictionary[i].treeNode + " " +  eventsDictionary[i].treeNode.Parent);
        }
        currNode = eventsDictionary[0].treeNode;
    }

    public void AttemptToChangeRooms(string directionNoun)
    {
        if (!eventIsOver)
            return;

        else if (exitDictionary.ContainsKey(directionNoun))
        {
            currentRoom = exitDictionary[directionNoun];
            //controller.LogStringWithReturn("You head off to the " + directionNoun);
            controller.DisplayRoomText();
            controller.DisplaySprite(currentRoom.roomPicture);
        } 
        else
        {
            controller.LogStringWithReturn("There is no path to the " + directionNoun);
        }
    }

    public void StartRoomEvent(string answer = " ") 
    {
        string currEventText =" ";

        if (eventIsOver)
            return;

        if (currNodeId == 0)
        {
            controller.DisplayCharacterSprite(currentRoom.localCharacter.sprite);
            currEventText = eventsDictionary[0].treeNode.Text;
        }
        else
            print("Node is not existing");

        if (answer == "y" || answer == "yes")
        {
                DecisionTreeNode leftChildNode = (DecisionTreeNode)currNode.LeftChild;
                currNode = leftChildNode;
                currNodeId = invertEventsDictionary[currNode];
                currEventText = eventsDictionary[currNodeId].treeNode.Text;
        
        }
        else if (answer == "n" || answer == "no")
        {
            //currentRoom.currNode = currentRoom.currNode.RightChild;
            DecisionTreeNode rightChildNode = (DecisionTreeNode)currNode.RightChild;
            currNode = rightChildNode;
            currNodeId = invertEventsDictionary[currNode];
            currEventText = eventsDictionary[currNodeId].treeNode.Text;
        }

        controller.LogStringWithReturn(currEventText);
        currentRoom.localCharacter.currentEventNodeID = currNodeId;

        if (currNode.LeftChild == null && currNode.RightChild == null)
        {
            EventOver();
        }

    }

    public void EventOver()
    {
        print("event is over");
        eventIsOver = true;
        currNodeId = 0;
        if (currentRoom.localCharacter != null)
            currentRoom.localCharacter.dialogueIsOver = true;
        controller.DisplayCharacterSprite(transparanetSpriteHolder);
        controller.DisplayInteractionsInRoom();

    }

    public void ClearExits()
    {
        exitDictionary.Clear();
    }

    public void ClearEvents()
    {
        eventsDictionary.Clear();
        decisionTreeCopied.Clear();
        invertEventsDictionary.Clear();
    }
}
