using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrucable : MonoBehaviour {

    [SerializeField] float hitPoints;

    public event System.Action OnDeath;
    public event System.Action OnDamageReceived;

    float damageTaken;

    public float HitpointsRemaining {
        get {
           return hitPoints - damageTaken;
        }
    }

    public bool IsAlive {
        get {
            return HitpointsRemaining > 0;
        }
    }

    public virtual void Die() {
        if (!IsAlive)
            return;

        if (OnDeath != null)
            OnDeath();
    }

    public virtual void TakeDamage(float amount) {
        damageTaken += amount;

        if (OnDamageReceived != null)
            OnDamageReceived();

        if (HitpointsRemaining <= 0)
        {
            Die();
        }
    }

    public void Reset() {
        damageTaken = 0;
    }
}
