using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour,IDamagable
{


    private GameObject currentTarget;

    [SerializeField]
    private float maxHealthPoints = 100f;

    [SerializeField]
    private int enemyLayer = 10;
    [SerializeField]
    private float damagePerHit = 10f;
    [SerializeField]
    private float minTimeBetwnHits = .5f;

    [SerializeField]
    private float maxAttackRange = 2f;

    private float currentHealthPoints;

    private float lastHitTime = 0f;
    private CameraRaycaster cameraRaycaster;



    private void Start()
    {
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnMouseClicked;
        currentHealthPoints = maxHealthPoints;
    }


    public float healthAsPercentage
    {
        get
        {
            return
                currentHealthPoints / (float)maxHealthPoints;
        }
        
    }

    private void OnMouseClicked(RaycastHit raycastHit, int layerHit)
    {
        if(layerHit == enemyLayer)
        {
            var enemy = raycastHit.collider.gameObject;
           


            // check If enemy is range

            if((enemy.transform.position-transform.position).magnitude > maxAttackRange)
            {
                return; 
            }
            currentTarget = enemy;

            var enemyComponent = enemy.GetComponent<Enemy>();
            if (Time.time - lastHitTime > minTimeBetwnHits)
            {
                enemyComponent.TakeDamage(damagePerHit);
                lastHitTime = Time.time;
            }

           
           



        }
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);

        if(currentHealthPoints <= 0f)
        {
            Destroy(gameObject, .2f);
        }
    }

   
}
