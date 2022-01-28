using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherBarrel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BarrelController.instance.AddBarrelCount();
        Debug.Log("Barrel Destroy");
        Destroy(this.gameObject);
    }
}
