using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour

{
    
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;
    
    

    private PlayerAnim player;
    private Animator anim;
    private Slime slime;

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
        if (!slime.isDead)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);
            if (hit != null)
            {
                player.OnHit();
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
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
