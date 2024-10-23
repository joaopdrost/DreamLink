using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour

{
    [Header("Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackDamage = 5f; // Ajuste o dano para um valor apropriado
    [SerializeField] private float attackCooldown = 2f; // Tempo entre ataques



    private PlayerAnim player;
    private Animator anim;
    private Slime slime;
    private float nextAttackTime;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerAnim>();
        slime = GetComponentInParent<Slime>();
    }


    public void PlayerAnim(int value)
    {
        anim.SetInteger("transition", value);
    }
    public void Attack()
    {
        if (!slime.isDead && Time.time >= nextAttackTime)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);
            if (hit != null)
            {
                Player hitPlayer = hit.GetComponent<Player>();
                if (hitPlayer != null && !hitPlayer.isDead)
                {
                    hitPlayer.TakeDamage(attackDamage);
                    nextAttackTime = Time.time + attackCooldown; // Define o próximo momento que pode atacar
                    anim.SetInteger("transition", 0); // Trigger para animação de ataque do slime
                }
            }
        }

    }
    public void OnHit()
    {

        if (slime.currentHealth <= 0)
        {
            slime.isDead = true;
            anim.SetTrigger("death");

            Destroy(slime.gameObject, 1f);
        }
        else
        {
            anim.SetTrigger("hit");
            slime.currentHealth--;

            slime.healthBar.fillAmount = slime.currentHealth / slime.totalHealth;
        }
    } 
    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            
            Gizmos.DrawWireSphere(attackPoint.position, radius);
        }
    }
}
