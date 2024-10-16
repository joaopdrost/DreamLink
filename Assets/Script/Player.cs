using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float runSpeed;
    private Rigidbody2D rig;

    
    private float initialSpeed;
    private bool _IsRolling;
    private bool _IsRunning;
    private bool _IsAttacking;

    private PlayerItems playerItems;
    private HUDController hudController;


    private Vector2 _direction;

    private int handlingObj;

    public Vector2 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }
    public bool IsRunning
    {
        get { return _IsRunning; }
        set { _IsRunning = value; }
    }
    public bool IsRolling
    {
        get { return _IsRolling; }
        set { _IsRolling = value; }

    }
    
    public bool IsAttacking
    {
        get { return _IsAttacking; }
        set { _IsAttacking = value; }
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
        playerItems = FindObjectOfType<PlayerItems>();
        hudController = FindObjectOfType<HUDController>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            handlingObj = 1;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            handlingObj = 2;
        }

        OnInput();
        OnRun();
        OnRolling();
        OnAttacking();

    }

    private void FixedUpdate()
    {
        OnMove();
    }
    #region Movement

    private void OnAttacking()
    {
        if (handlingObj == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                IsAttacking = true;
                speed = 0f;
            }
            if (Input.GetMouseButtonUp  (0))
            {
                IsAttacking = false;
                speed = initialSpeed;
            }
        }

    }

    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {
        rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime);
    }


    void OnRun() 
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed; 
            _IsRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initialSpeed;
            _IsRunning = false;
        }

        // Verifica se o jogador pressionou a tecla "Shift Esquerdo" e se tem stamina suficiente
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerItems.TotalStamina > 0)
        {
            // Altera a velocidade do jogador para a velocidade de corrida
            speed = runSpeed;

            // Define a variável _IsRunning como true, indicando que o jogador está correndo
            _IsRunning = true;

            // Chama o método SetPlayerRunning do HUDController com o parâmetro true
            // para atualizar a interface do usuário (HUD) e indicar que o jogador está correndo
            hudController.SetPlayerRunning(true);
        }


        // Verifica se o jogador soltou a tecla "Shift Esquerdo"
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // Altera a velocidade do jogador para a velocidade inicial
            speed = initialSpeed;

            // Define a variável _IsRunning como false, indicando que o jogador não está correndo
            _IsRunning = false;

            // Chama o método SetPlayerRunning do HUDController com o parâmetro false
            // para atualizar a interface do usuário (HUD) e indicar que o jogador não está correndo
            hudController.SetPlayerRunning(false);
        }
        // Verifica se o jogador pressionou a tecla "Shift Esquerdo" e se tem stamina suficiente
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerItems.TotalStamina > 0)
        {
            // Altera a velocidade do jogador para a velocidade de corrida
            speed = runSpeed;

            // Define a variável _IsRunning como true, indicando que o jogador está correndo
            _IsRunning = true;

            // Chama o método SetPlayerRunning do HUDController com o parâmetro true
            // para atualizar a interface do usuário (HUD) e indicar que o jogador está correndo
            hudController.SetPlayerRunning(true);
        }

        // Verifica se o jogador soltou a tecla "Shift Esquerdo" ou se a stamina é zero
        if (Input.GetKeyUp(KeyCode.LeftShift) || playerItems.TotalStamina <= 0)
        {
            // Altera a velocidade do jogador para a velocidade inicial
            speed = initialSpeed;

            // Define a variável _IsRunning como false, indicando que o jogador não está correndo
            _IsRunning = false;

            // Chama o método SetPlayerRunning do HUDController com o parâmetro false
            // para atualizar a interface do usuário (HUD) e indicar que o jogador não está correndo
            hudController.SetPlayerRunning(false);
        }

    }

    private void OnRolling()
    {
        if (Input.GetMouseButtonDown(1))
        {
            
            _IsRolling = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            
            _IsRolling = false; 
        }
       
    }

    

    #endregion

}
