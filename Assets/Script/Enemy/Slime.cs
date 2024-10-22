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

    
    private Player player;

    void Start()
    {
        currentHealth = totalHealth;
        player = FindObjectOfType<Player>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (!isDead)
        {

            agent.SetDestination(player.transform.position);

            if (Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                // chegou no limite (slime parado)
                animContol.PlayerAnim(2);
            }

            else
            {
                // slime segue player
                animContol.PlayerAnim(1);
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
       
    }
}
