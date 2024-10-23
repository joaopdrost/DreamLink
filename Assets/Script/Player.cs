using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Status")]
    public float totalHealth;
    public float currentHealth;
    public Image healthBar; 
    public bool isDead;



    [SerializeField]private float speed;
    [SerializeField]private float runSpeed;
    private Rigidbody2D rig;

    [Header("Moviments")]
    private float initialSpeed;
    private bool _IsRolling;
    private bool _IsRunning;
    private bool _IsAttacking;

    private PlayerAnim playerAnim;
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
        playerAnim = GetComponent<PlayerAnim>();
        currentHealth = totalHealth;
        UpdateHealthUI();
        isDead = false;
    }


    private void Update()
    {
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                handlingObj = 1;
                hudController.SelectItem(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                handlingObj = 2;
                hudController.SelectItem(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                handlingObj = 3;
                hudController.SelectItem(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                handlingObj = 4;
                hudController.SelectItem(3);
            }

            OnInput();
            OnRun();
            OnRolling();
            OnAttacking();
        }
        else
        {
            // Zera o movimento quando morto
            _direction = Vector2.zero;
            speed = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            OnMove();
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
           
            UpdateHealthUI();

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                playerAnim.OnDeath();
                // Adicione aqui l�gica de morte do player se necess�rio

            }
            else
            {
                playerAnim.OnHit(); // Executa a anima��o de hit apenas se n�o morrer
            }

        }
    }
     private void UpdateHealthUI()
    {
        healthBar.fillAmount = currentHealth / totalHealth;
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

            // Define a vari�vel _IsRunning como true, indicando que o jogador est� correndo
            _IsRunning = true;

            // Chama o m�todo SetPlayerRunning do HUDController com o par�metro true
            // para atualizar a interface do usu�rio (HUD) e indicar que o jogador est� correndo
            hudController.SetPlayerRunning(true);
        }


        // Verifica se o jogador soltou a tecla "Shift Esquerdo"
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // Altera a velocidade do jogador para a velocidade inicial
            speed = initialSpeed;

            // Define a vari�vel _IsRunning como false, indicando que o jogador n�o est� correndo
            _IsRunning = false;

            // Chama o m�todo SetPlayerRunning do HUDController com o par�metro false
            // para atualizar a interface do usu�rio (HUD) e indicar que o jogador n�o est� correndo
            hudController.SetPlayerRunning(false);
        }

        // Verifica se o jogador pressionou a tecla "Shift Esquerdo" e se tem stamina suficiente
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerItems.TotalStamina > 0)
        {
            // Altera a velocidade do jogador para a velocidade de corrida
            speed = runSpeed;

            // Define a vari�vel _IsRunning como true, indicando que o jogador est� correndo
            _IsRunning = true;

            // Chama o m�todo SetPlayerRunning do HUDController com o par�metro true
            // para atualizar a interface do usu�rio (HUD) e indicar que o jogador est� correndo
            hudController.SetPlayerRunning(true);
        }

        // Verifica se o jogador soltou a tecla "Shift Esquerdo" ou se a stamina � zero
        if (Input.GetKeyUp(KeyCode.LeftShift) || playerItems.TotalStamina <= 0)
        {
            // Altera a velocidade do jogador para a velocidade inicial
            speed = initialSpeed;

            // Define a vari�vel _IsRunning como false, indicando que o jogador n�o est� correndo
            _IsRunning = false;

            // Chama o m�todo SetPlayerRunning do HUDController com o par�metro false
            // para atualizar a interface do usu�rio (HUD) e indicar que o jogador n�o est� correndo
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
