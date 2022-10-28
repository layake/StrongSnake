using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    private ParticleSystem explosionVFX;

    private void Awake()
    {
        explosionVFX = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    public void explosion()
    {
        Debug.Log("explosion");
        explosionVFX.Play();
        Destroy(this.gameObject, 0.15f); 
    }
}
