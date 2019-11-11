using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  
    public float projectileSpeed = 5f;
    private float damageCaused;

    public void SetDamage(float damage)
    {
        damageCaused = damage;
    }




    private void OnTriggerEnter(Collider collider )
    {
        Component damagableComponent  = collider.gameObject.GetComponent(typeof(IDamagable));

        //print("damagable component" + damagableComponent);

        if(damagableComponent)
        {
            (damagableComponent as IDamagable).TakeDamage(damageCaused);
        }
    }
}
