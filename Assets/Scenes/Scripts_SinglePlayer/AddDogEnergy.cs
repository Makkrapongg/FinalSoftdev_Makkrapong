using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDogEnergy : NormalFood_SG
{
    //public int duration = 8;

    protected override void Eat()
    {
        FindObjectOfType<GameManager_SG>().AddDogEnergyEat(this);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cat"))
        {
            Eat();
        }
    }
}
