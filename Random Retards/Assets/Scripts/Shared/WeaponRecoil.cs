using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class WeaponRecoil : MonoBehaviour
{
    [System.Serializable] public struct Layer
    {
        public AnimationCurve curve;
        public Vector3 direction;
    }

    [SerializeField] Layer[] layers;

    [SerializeField] float recoilSpeed;

    [SerializeField] float recoilCooldown;

    [SerializeField] float strength;

    float nextRecoilCooldown;
    float recoilActiveTime;

    Shooter m_shooter;
    Shooter Shooter
    {
        get
        {
            if (m_shooter == null)
                m_shooter = GetComponent<Shooter>();
            return m_shooter;
        }
    }

    Crosshair m_crosshair;
    Crosshair Crosshair
    {
        get
        {           
                if (m_crosshair == null)
                    m_crosshair = GameManager.Instance.LocalPlayer.GetComponentInChildren<Crosshair>();           
            return m_crosshair;
        }
    }

    public void Activate()
    {
        nextRecoilCooldown = Time.time + recoilCooldown;
    }

    private void Update()
    {
        if (nextRecoilCooldown > Time.time)
        {
            // holdiong the fire button
            recoilActiveTime += Time.deltaTime;
            float percentage = recoilActiveTime / recoilSpeed;
            percentage = Mathf.Clamp01(percentage);

            Vector3 recoilAmount = Vector3.zero;
            for (int i = 0; i < layers.Length; i++)
                recoilAmount += layers[i].direction * layers[i].curve.Evaluate(percentage);

            Shooter.aimTargetOffset = Vector3.Lerp(Shooter.aimTargetOffset, Shooter.aimTargetOffset + recoilAmount, strength * Time.deltaTime);
            Crosshair.ApplayScale(percentage * Random.Range(strength * 7, strength * 9));
        }
        else
        {
            // not holding the fire button
            recoilActiveTime -= Time.deltaTime;
            if (recoilActiveTime < 0)
                recoilActiveTime = 0;

            Crosshair.ApplayScale(getPercentage());

            if (recoilActiveTime == 0)
                Shooter.aimTargetOffset = Vector3.zero;
            Crosshair.ApplayScale(0);
        }
    }

    float getPercentage()
    {
        float percentage = recoilActiveTime / recoilSpeed;
        return Mathf.Clamp01(percentage);
    }
}
