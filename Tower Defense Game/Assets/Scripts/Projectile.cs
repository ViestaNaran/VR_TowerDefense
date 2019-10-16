using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* The purpose of this clas is to different some behaivours to our projectiles fired from the towers
*/

public class Projectile : MonoBehaviour
{
    private GameObject target;
    private float speed;
    public int damageAmount; // read only
    public string damageType; // read only

    void Update()
    {
        Movement movement = GetComponent<Movement>();
        movement.SetSpeed(speed);
        gotTarget();

        // checks if the projectile will hit the target in the next frame, if true it will damage the target and despawn.
        if (gotTarget() == true)
        {
            if (movement.Reached(target))
                Damage(target);
            movement.MoveTo(target);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // check if the target is still alive else it despawns the projectile.
    bool gotTarget()
    {
        if (target == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // damage target when hitting it
    void Damage(GameObject target)
    {
        target.GetComponent<Health>().TakeDamage(damageAmount, damageType, target.tag);
        Destroy(this.gameObject);
    }

    //setters
    public void setTarget(GameObject target)
    {
        this.target = target;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void setDamageAmount(int damageAmount)
    {
        this.damageAmount = damageAmount;
    }

    public void setType(string damageType)
    {
        this.damageType = damageType;
    }
}