using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Destrucable {

    [SerializeField] float inSeconds;

    public override void Die()
    {
        base.Die();

        GameManager.Instance.Respawner.Despawm(gameObject, inSeconds);
    }

    void OnEnable()
    {
        Reset();
    }

    public override void TakeDamage(float amount)
    {       
        base.TakeDamage(amount);
        print("Remaining: " + HitpointsRemaining);
    }
}
