using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour,IDamagable
{
    
    [SerializeField]
    private float maxHealthPoints = 100f;
    [SerializeField]
    private float attackRadius = 4f;

    [SerializeField]
    private float chaseRadius = 6f;

    private GameObject player;

    AICharacterControl aICharacterControl;

    private float currentHealthPoints = 100f;
    public float healthAsPercentage 
    {
        get
        {
            return
                currentHealthPoints / (float)maxHealthPoints;
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aICharacterControl = GetComponent<AICharacterControl>();
    }


    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // Enemy following playwe
        if (distanceToPlayer <=chaseRadius)
        {
            aICharacterControl.SetTarget(player.transform);
        }
        else
        {
            aICharacterControl.SetTarget(transform);
        }

        if (distanceToPlayer <= attackRadius)
        {
            print(gameObject.name+ "Attacking  plaer");

            //spawing projectile

        }
    }


    private void OnDrawGizmos()
    {
        //Draw attack sphere
        Gizmos.color = new Color(255f, 0, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        //Draw move sphere
        Gizmos.color = new Color(0f, 0, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
