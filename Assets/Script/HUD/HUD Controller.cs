using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    [SerializeField] private Image lifeUIBar;
    [SerializeField] private Image noLifeUIBar;
    [SerializeField] private Image staminaUIBar;


    private PlayerItems playerItems;

    public Image LifeUIBar
    {
        get
        {
            return lifeUIBar;
        }

        set
        {
            lifeUIBar = value;
        }
    }

    public Image NoLifeUIBar
    {
        get
        {
            return noLifeUIBar;
        }

        set
        {
            noLifeUIBar = value;
        }
    }

    public Image StaminaUIBar
    {
        get
        {
            return staminaUIBar;
        }

        set
        {
            staminaUIBar = value;
        }
    }

    public void SetPlayerRunning(bool isRunning)
    {
        // Adicione a lógica aqui para atualizar a barra de stamina quando o jogador está correndo
        playerItems.IsRunning = isRunning;
    }

    private void Awake()
    {
        playerItems = FindObjectOfType<PlayerItems>();
    }
    void Start()
    {
        LifeUIBar.fillAmount = 0.25f;
        NoLifeUIBar.fillAmount = 0.25f;
    }

    
    void Update()
    {
        if (playerItems.IsRunning)
        {
            playerItems.DrainStamina();
        }
        else
        {
            playerItems.RecoverStamina();
        }

        // Atualize a barra de stamina
        StaminaUIBar.fillAmount = playerItems.TotalStamina;
    }
}
