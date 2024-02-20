using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public TMP_Text displayText;
    public Image imageHolder;
    
    public Image characterSpriteHolder;

    public InputAction[] inputActions;

    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public GraphOfRooms roomsMap;
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();
    List<string> actionLog = new List<string>();

    // Start is called before the first frame update
    void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
        roomsMap = GetComponent<GraphOfRooms>();
    }

    private void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
        DisplaySprite(roomNavigation.currentRoom.roomPicture);
    }

    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());

        displayText.text = logAsText;
    }

    public void DisplaySprite(Sprite _sprite)
    {
        imageHolder.sprite = _sprite;
    }

    public void DisplayCharacterSprite(Sprite sprite)
    {
        characterSpriteHolder.sprite = sprite;
    }

    public void DisplayRoomText()
    {


        //string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());

        //string joinedInteractionDescriptions = roomNavigation.currentRoom.currentEvent.Text;

        string combinedText = roomNavigation.currentRoom.description ;

        LogStringWithReturn(combinedText);
        //roomNavigation.StartRoomEvent();
        ClearCollectionsForNewRoom();
        UnpackRoom();

    }

    public void DisplayInteractionsInRoom()
    {
        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());
        LogStringWithReturn(joinedInteractionDescriptions);
    }

    void UnpackRoom()
    {
        roomNavigation.UnpackExitsInRoom();
        roomNavigation.UnpackRoomEvent();
    }

    void ClearCollectionsForNewRoom()
    {
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
        roomNavigation.ClearEvents();
    }

    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }
}
