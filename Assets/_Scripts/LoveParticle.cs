using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveParticle : MonoBehaviour
{
    private ParticleSystem love;


    // Start is called before the first frame update
    void Start()
    {
        love = gameObject.GetComponent<ParticleSystem>();
    }

}
