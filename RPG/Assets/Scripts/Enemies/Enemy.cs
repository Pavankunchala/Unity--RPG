using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour,IDamagable
{
    
    [SerializeField]
    private float maxHealthPoints = 100f;
    

    [SerializeField]
    private float chaseRadius = 6f;

    [SerializeField]
    private float attackRadius = 4f;

    [SerializeField]
    private GameObject projectileToUse;

    [SerializeField]
    private GameObject projectileSocket;

    [SerializeField]
    private float damagePerShot = 9f;

    [SerializeField]
    private float projectileSpeed = 5f;

    [SerializeField]
    private float timeBetnProjectiles = .5f;

    [SerializeField]
    private Vector3 aimOffset = new Vector3(0, 1f, 0);

    private bool isAttacking = false;

   

    private GameObject player;

    AICharacterControl aICharacterControl;

    private float currentHealthPoints;
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
        if(currentHealthPoints <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aICharacterControl = GetComponent<AICharacterControl>();
        currentHealthPoints = maxHealthPoints;
    }


    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= attackRadius && !isAttacking)
        {
            isAttacking = true;

            //Debug.Log("Projectile spwaned");
            InvokeRepeating("SpawnProjectiles", 0f, timeBetnProjectiles);



        }
        if(distanceToPlayer > attackRadius)
        {
            isAttacking = false;
            CancelInvoke();
        }

        // Enemy following playwe
        if (distanceToPlayer <=chaseRadius)
        {
            aICharacterControl.SetTarget(player.transform);
        }
        else
        {
            aICharacterControl.SetTarget(transform);
        }

        
    }

    private void SpawnProjectiles()
    {
        GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
        // the damage caused by the projectile
        Projectile projectileComponet = newProjectile.GetComponent<Projectile>();

        projectileComponet.SetDamage(damagePerShot);

        

        // to get the postion of player
        Vector3 unitToPlayer = (player.transform.position+ aimOffset-projectileSocket.transform.position ).normalized;


        projectileSpeed = projectileComponet.projectileSpeed;
        //newProjectile.transform.position = Vector3.MoveTowards(projectileSocket.transform.position, player.transform.position, projectileSpeed* Time.deltaTime);

        newProjectile.GetComponent<Rigidbody>().velocity = unitToPlayer * projectileSpeed;

     
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
