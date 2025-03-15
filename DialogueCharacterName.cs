using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class DialogueCharacterName : MonoBehaviour
{
    public TextMeshProUGUI DialogueName;
    public  List<string> CharacterName = new List<string>();
    private int index = 0;
    void Start()
    {
        if (CharacterName.Count > 0)
        {
            DisplayCharacterName(0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DisplayCharacterName(int index)
    {
        if(index >= 0 && index < CharacterName.Count)
        {
            DialogueName.text = CharacterName[index];
        }
    }

    public void DisplayRandomCharacterName()
    {
        if(CharacterName.Count > 0)
        {
            int randomIndex = Random.Range(0, CharacterName.Count);
            DisplayCharacterName(randomIndex);
        }
    }
}
