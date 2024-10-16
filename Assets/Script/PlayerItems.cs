using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [Header("Amouts")]
     public float totalLife;
     public float totalNoLife;

    [Header("Limits")]

    public float liveLimit = 0.25f;
    public float noLivelLimit = 0.25f;
    [SerializeField] private float totalStamina = 1f; // 1 representa 100% de stamina
    [SerializeField] private float staminaDrainRate = 0.5f; // quanto a stamina diminui por segundo
    [SerializeField] private float staminaRecoveryRate = 0.09f; // quanto a stamina se recupera por segundo

    public bool IsRunning { get; set; }
    public float TotalStamina
    {
        get { return totalStamina; }
        set { totalStamina = Mathf.Clamp(value, 0f, 1f); } // garante que a stamina esteja entre 0 e 1
    }

    public void DrainStamina()
    {
        TotalStamina -= staminaDrainRate * Time.deltaTime;
    }

    public void RecoverStamina()
    {
        TotalStamina += staminaRecoveryRate * Time.deltaTime;
    }

    public void LiveLimit(float life)
    {
        if (totalLife <= liveLimit )
        {
            totalLife += life;
        }
    }
    public void NoLivelLimit(float nolife)
    {
        if (totalNoLife <= noLivelLimit )
        {
            totalNoLife += nolife;
        }
    }
}
