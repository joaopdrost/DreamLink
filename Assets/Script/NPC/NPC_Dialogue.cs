using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Npc_Dialogue : MonoBehaviour
{

    public float dialogueRange;
    public LayerMask playerLayer;

    public DialogueSettings dialogue;

    bool playerHit;

    private List <string> sentence = new List<string>();

    private void Start()
    {
        GetText();
    }
    // chamado a cada frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            DialogueControl.instance.Speech(sentence.ToArray());
            
        }
    }

    void GetText()
    { 
        for(int i = 0; i < dialogue.dialogues.Count; i++)
        {
            switch (DialogueControl.instance.language)
            {
                case DialogueControl.idiom.pt:
                    sentence.Add(dialogue.dialogues[i].setence.portugese);

                    break;
                case DialogueControl.idiom.eng:
                    sentence.Add(dialogue.dialogues[i].setence.english);

                    break;
                case DialogueControl.idiom.spa:
                    sentence.Add(dialogue.dialogues[i].setence.spanish);

                    break;
                    
            }
            
        }
    }
   
    // usado pela fisica
    void FixedUpdate()
    {
        ShowDialogue();
    }

    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position,dialogueRange,playerLayer);

        if (hit != null) 
        {
            playerHit = true;
        }
        else
        {
            playerHit = false;
            
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }

}
