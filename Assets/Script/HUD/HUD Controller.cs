using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Stamina e Life Bar")]
    [SerializeField] private Image lifeUIBar;
    [SerializeField] private Image noLifeUIBar;
    [SerializeField] private Image staminaUIBar;

    [Header("Tools")]
    [SerializeField] private Image[] itemSlots; // Array de slots de itens no HUD
    [SerializeField] private Color selectedColor = Color.red; // Cor para o item selecionado
    [SerializeField] private Color defaultColor = Color.white; // Cor padrão dos itens

    private int currentSelectedItem = -1;

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

    // referente a seleçao de intens do Hud tools
    public void SelectItem(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < itemSlots.Length)
        {
            // Desseleciona o item anterior
            if (currentSelectedItem != -1 && currentSelectedItem < itemSlots.Length)
            {
                itemSlots[currentSelectedItem].color = defaultColor;
            }

            // Seleciona o novo item
            currentSelectedItem = itemIndex;
            itemSlots[currentSelectedItem].color = selectedColor;
        }
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
