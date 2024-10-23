using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;



    private Player player;
    private Animator anim;

    private bool isHitting;
    private float recoveryTime = 1.5f;
    private float timeCount;

    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (!player.isDead) // Só executa as animações se o player não estiver morto
        {
            OnMove();
            OnRun();

            timeCount += Time.deltaTime;

            if (isHitting)
            {
                if (timeCount >= recoveryTime)
                {
                    isHitting = false;
                    timeCount = 0;
                }
            }
        }
    }
    #region Moviment

    void OnMove()
        {

            if (player.Direction.sqrMagnitude > 0)
            {
            if (player.IsRolling)
            {
                anim.SetTrigger("isRoll");
            }
            else
            {
                anim.SetInteger("transition", 1);
            }
            
            }
            else
            {
                anim.SetInteger("transition", 0);
            }

            if (player.Direction.x > 0)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }

            if (player.Direction.x < 0)
            {
                transform.eulerAngles = new Vector2(0, 180);
            }

        if (player.IsAttacking)
        {
            anim.SetInteger("transition", 3);
        }

    }

    void OnRun()
    {
        if (player.IsRunning)
        {
            anim.SetInteger("transition",2);
        }
    }
    #endregion
    #region Attack

    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if (hit != null)
        {
            // atacou o inimigo
            hit.GetComponentInChildren<AnimationControl>().OnHit();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }


    #endregion
    public void OnDeath()
    {
        anim.SetTrigger("death");
        Destroy(player.gameObject, 1f);
        // Você pode adicionar aqui outras ações que devem acontecer na morte

    }
    public void OnHit()
    {
        if (!isHitting)
        {
            anim.SetTrigger("hit");
            isHitting = true;
            timeCount = 0;
        }

    }
    
}
