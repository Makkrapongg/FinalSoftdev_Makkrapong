using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopDogEnergy : NormalFood_SG
{
    public int duration = 3;

    protected override void Eat()
    {
        FindObjectOfType<GameManager_SG>().StopDogEnergyEat(this);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cat"))
        {
            Eat();
        }
    }
}
