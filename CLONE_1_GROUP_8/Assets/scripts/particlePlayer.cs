using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlePlayer : MonoBehaviour
{
    public ParticleSystem fireParticle;
    public ParticleSystem waterParticle;
    public ParticleSystem rockParticle;

    public float destroyTime = 2f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireProjectile")
        {
            fireParticle.Play();
            Destroy(gameObject);
        }

        if (other.tag == "WaterShield")
        {
            waterParticle.Play();
            Destroy(gameObject);
        }

        if(other.tag == "RockProjectile")
        {
            rockParticle.Play();
            Destroy(gameObject);
        }
    }
}
