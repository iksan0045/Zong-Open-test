using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationType : MonoBehaviour
{
    public string boxType;
    
    [SerializeField]
    private ParticleSystem vfx;
    
    public void PlayVFX()
    {
        if (vfx != null)
        {
            vfx.Play();
        }
    }
}
