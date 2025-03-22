using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
    public class Dialogue
    {
        public string characterName;
        [TextArea(10,10)]
        public string[] sentences;
    }

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;   
    private bool Triggered = false; 
    private bool IsEnable = false;

    void Start()
    {
        StartCoroutine(EnableTriggerAfterDelay(0.5f));
    }

    private IEnumerator EnableTriggerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        IsEnable = true;
    }
    public void TriggerDialogue()
    {
        if(!Triggered && IsEnable)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            Triggered = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && IsEnable)
        {
            TriggerDialogue();
        }
    }
}
