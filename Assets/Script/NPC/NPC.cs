using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed ;
    private float initialSpeed;

    private int index;
    private Animator anim;

    


    private void Start()
    {
        initialSpeed = speed;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (DialogueControl.instance.isShowing)
        {
            speed = 0f;
            
        }
        else
        {
            speed = initialSpeed;
           
        }

        
    }
}
