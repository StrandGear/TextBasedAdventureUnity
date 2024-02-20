using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tree;

[CreateAssetMenu(menuName = "TextAdventure/Character")]
public class Character : ScriptableObject
{
    public Sprite sprite;
    public DecisionTreeDictValue[] decisionTree;
    public int currentEventNodeID;
    public bool dialogueIsOver;

    private void OnEnable()
    {
        dialogueIsOver = false;
        currentEventNodeID = 0;
    }
}
