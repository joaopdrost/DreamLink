using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }

    public idiom language; 

    [Header("Components")]
    public GameObject dialogueObj; // janela do dialogo
    public Image profileSprite; // sprite do npc
    public Text speechText; // fala do npc
    public Text actorNameText; // nome do npc

    [Header("Settings")]
    public float typingSpeed; // velocidade da fala

    // variaveis de controle
    public bool isShowing;// se a janela esta visivel
    private int index; // index das sentenças
    private string[] sentences;

    public static DialogueControl instance;

    
    
    
    // awake é chamado antes de todos os Start() na hieraquia de execução dos scritps
    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    IEnumerator TypeSentence() // Corrotina // codigo para executar fala por fala 
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    // pular para proxima fala 
    public void NextSentence()
    {

        if (speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
                
            }
            else
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false;
            }
        }
    }
    // chamar a fala do npc
    public void Speech(string[] txt)
    {
        if (!isShowing) {
        
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        
    }
}

}
