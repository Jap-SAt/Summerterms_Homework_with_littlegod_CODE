using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI nameText;               
    public TextMeshProUGUI dialogueText;          
    public GameObject dialogueBox;       
    
    private Queue<string> sentences;     
    private bool isDialogueActive;       
    void Start()
    {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false);
        isDialogueActive = false;

        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(DisplayNextSentence);
    }
    
    
    void Update()
    {

        if (isDialogueActive && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            DisplayNextSentence();
        }
    }

    
    public void StartDialogue(Dialogue dialogue)
    {
        
        dialogueBox.SetActive(true);
        isDialogueActive = true;
        
        nameText.text = dialogue.characterName;
        
        sentences.Clear();
        
        foreach (string sentence in dialogue.sentences)
    {
        // แบ่งข้อความที่ยาวเกินกว่าจำนวนตัวอักษรที่กำหนด
        List<string> splitSentences = SplitLongSentence(sentence, 200); // แบ่งทุก 100 ตัวอักษร
        foreach (string split in splitSentences)
        {
            sentences.Enqueue(split);
        }
    }
        
        DisplayNextSentence();
    }

    private List<string> SplitLongSentence(string sentence, int maxChars)
    {
        List<string> result = new List<string>();
    
            if (sentence.Length <= maxChars)
                {
                    result.Add(sentence);
                    return result;
                }
    
    // แบ่งข้อความตามจำนวนตัวอักษรสูงสุดที่กำหนด
        int startIndex = 0;

            while (startIndex < sentence.Length)
                {
                    int length = Mathf.Min(maxChars, sentence.Length - startIndex);
        
                // พยายามหาช่องว่างเพื่อตัดคำให้สวยงาม
                    if (startIndex + length < sentence.Length && length == maxChars)
                        {
                            int spaceIndex = sentence.LastIndexOf(' ', startIndex + length - 1, length);
                            if (spaceIndex > startIndex)
                                {
                                    length = spaceIndex - startIndex + 1;
                                }
                        }
        
        result.Add(sentence.Substring(startIndex, length).Trim());
        startIndex += length;
                }
    
        return result;
    }
    
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        string sentence = sentences.Dequeue();
        
        StopAllCoroutines();
        
        StartCoroutine(TypeSentence(sentence));
    }
    
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            
            yield return new WaitForSeconds(0.03f);
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        isDialogueActive = false;
        
        Debug.Log("End of conversation");
    }
}
