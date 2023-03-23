using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDogEnergy : NormalFood_SG
{
    public int duration = 6;

    protected override void Eat()
    {
        FindObjectOfType<GameManager_SG>().DeleteDogEnergyEat(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cat"))
        {
            Eat();
        }
    }
}
