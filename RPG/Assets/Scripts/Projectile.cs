using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private float damageCaused = 10f;

    private void OnTriggerEnter(Collider collider )
    {
        Component damagableComponent  = collider.gameObject.GetComponent(typeof(IDamagable));

        print("damagable component" + damagableComponent);

        if(damagableComponent)
        {
            (damagableComponent as IDamagable).TakeDamage(damageCaused);
        }
    }
}
