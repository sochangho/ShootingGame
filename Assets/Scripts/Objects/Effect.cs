using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : BaseObject
{
    private ParticleSystem particleSystem;

    private ObjectPooling objectPooling;

    public override void Created(){

        particleSystem = GetComponent<ParticleSystem>();
    
    }

    public override void Destroyed(){}

    public override void Active(){
        
        particleSystem.Play();

        StartCoroutine(DurationEffect());

    }

    public override void InActive(){}


    IEnumerator DurationEffect()
    {
        yield return new WaitForSeconds(particleSystem.main.duration);

        if (objectPooling == null)
        {
            objectPooling = GetComponent<ObjectPooling>();

        }

        objectPooling.Destroy();
    }

    



}
