using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Sound : MonoBehaviour
{
    AudioSource[] mySounds;

    AudioSource hurtNoise;

    AudioSource gemNoise;

    void Start()
    {
        mySounds = GetComponents<AudioSource>();

        hurtNoise = mySounds[0];
        gemNoise = mySounds[1];
    }


    public void HurtNoise()
    {
        hurtNoise.Play();
        
    }

    public void GemNoise()
    {
        gemNoise.Play();
        
    }

} 
