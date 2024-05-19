using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueTaifa : MonoBehaviour
{

    public string sceneToLoad;

    public TextMeshProUGUI dialogueText;

    public string[] lines;

    public AudioClip[] audioClips;

    public float textSpeed = 0.1f;

    int index;

    public AudioSource audioSource; // AudioSource para reproducir los audios


    void Start()
    {
        dialogueText.text = string.Empty;
        audioSource = gameObject.AddComponent<AudioSource>();
        StartDialogue();
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
                StopAudio();
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
        PlayAudio(); // Reproducir el audio correspondiente a la línea actual
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
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void PlayAudio()
    {
        StopAudio(); // Detener el audio actual antes de reproducir el nuevo

        if (audioClips != null && audioClips.Length > index && audioClips[index] != null)
        {
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }
    }

    private void StopAudio()
    {
    if (audioSource.isPlaying)
    {
        audioSource.Stop();
    }
    }

}
