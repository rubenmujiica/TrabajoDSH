using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueReyHieloFinal : MonoBehaviour
{
    public GameObject dialogoObject;

    public TextMeshProUGUI dialogueText;

    public string[] lines;

    public float textSpeed = 0.1f;

    int index;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(dialogueText.text == lines[index])
            {
                NextLine();
            }
            else{
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;

        StartCoroutine(WriteLine());

    }

    IEnumerator WriteLine()
    {
        foreach(char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if(index < lines.Length - 1)
        {
            dialogueText.text = string.Empty;
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            dialogoObject.SetActive(false);
            SceneManager.LoadScene("Creditos");
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")  && !other.isTrigger)
        {
            dialogoObject.SetActive(true);
            dialogueText.text = string.Empty;
            StartDialogue();

        }
    }

}
