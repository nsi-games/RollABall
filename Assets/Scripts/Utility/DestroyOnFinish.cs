using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Destroys Particles / Sounds when all has finished playing
 */

public class DestroyOnFinish : MonoBehaviour
{
    private ParticleSystem[] particles;
    private AudioSource[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
        sounds = GetComponentsInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // If both Sounds & Particles are Finished
        if(SoundsFinished() && ParticlesFinished())
        {
            // Destroy self
            Destroy(gameObject);
        }
    }

    bool SoundsFinished()
    {
        foreach (var sound in sounds)
        {
            if (sound.isPlaying)
            {
                return false;
            }
        }
        return true;
    }

    bool ParticlesFinished()
    {
        foreach (var particle in particles)
        {
            if(!particle.isStopped)
            {
                return false;
            }
        }
        return true;
    }
}
