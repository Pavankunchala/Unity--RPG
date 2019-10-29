using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{
    
    [SerializeField]
    private float maxHealthPoints = 100f;
    [SerializeField]
    private float attackRadius = 4f;

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



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aICharacterControl = GetComponent<AICharacterControl>();
    }


    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if(distanceToPlayer <= attackRadius)
        {
            aICharacterControl.SetTarget(player.transform);
        }
        else
        {
            aICharacterControl.SetTarget(transform);
        }
    }
}
