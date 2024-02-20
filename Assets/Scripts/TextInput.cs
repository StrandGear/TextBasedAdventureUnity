using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextInput : MonoBehaviour
{
    GameController controller;
    public TMP_InputField inputField;

    private void Awake()
    {
        controller = GetComponent<GameController>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToLower();
        controller.LogStringWithReturn(userInput);

        char[] delimiterCharacters = { ' ' };
        string[] separatedInputWords = userInput.Split(delimiterCharacters);

        for (int i = 0; i < controller.inputActions.Length; i++)
        {
            InputAction inputAction = controller.inputActions[i];
            foreach (string keyWord in inputAction.keyWords)
            {
                if (keyWord == separatedInputWords[0] || keyWord == userInput)
                    inputAction.RespondToInput(controller, separatedInputWords);
            }
            /*            if (inputAction.keyWord == separatedInputWords[0] || inputAction.keyWord == userInput)
                        {
                            inputAction.RespondToInput(controller, separatedInputWords);
                        }*/
        }

        InputComplete();
    }

    void InputComplete()
    {
        controller.DisplayLoggedText();
        inputField.ActivateInputField(); //to put cursor back into input field
        inputField.text = null;
    }
}
