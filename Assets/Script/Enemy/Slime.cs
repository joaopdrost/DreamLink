using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Slime : MonoBehaviour
{
    [Header("Status")]
    public float totalHealth;
    public float currentHealth;
    public Image healthBar;
    public bool isDead;

    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationControl animContol;

    [Header("Attack")]
    [SerializeField] private float attackDistance = 1.5f; // Distância para começar o ataque
    [SerializeField] private float attackRate = 2f; // Taxa de ataque
    private float nextAttackTime;

    private Player player;

    void Start()
    {
        currentHealth = totalHealth;
        player = FindObjectOfType<Player>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        nextAttackTime = 0f;
        UpdateHealthBar();
    }

    void Update()
    {
        if (!isDead && player != null && !player.isDead)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= agent.stoppingDistance)
            {
                // Dentro da distância de ataque
                animContol.PlayerAnim(2); // Animação parado
                if (Time.time >= nextAttackTime)
                {
                    animContol.Attack();
                    nextAttackTime = Time.time + attackRate;
                }
            }
            else
            {
                // Perseguindo o player
                agent.SetDestination(player.transform.position);
                animContol.PlayerAnim(1); // Animação de movimento
            }

            float posx = player.transform.position.x - transform.position.x;
            if (posx > 0)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
            }

        }

        else if (isDead)
        {
            agent.SetDestination(transform.position);
            animContol.PlayerAnim(0); // Desativa a navegação quando morto
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            UpdateHealthBar();

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
            else
            {
                animContol.OnHit(); // Animação de hit
            }
        }
    }

    private void Die()
    {
        isDead = true;
        agent.enabled = false;
        
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / totalHealth;
        }
    }

    // Para ajudar na depuração
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, agent != null ? agent.stoppingDistance : attackDistance);
    }
}
