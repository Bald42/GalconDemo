using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Параметры корабля
/// </summary>
public class ShipParametrs : MonoBehaviour
{
    public GameObject Target = null;

    private void OnTriggerEnter(Collider other)
    {
        if (Target && Target == other.gameObject)
        {
            other.GetComponent<PlanetController>().MinusShip();
            gameObject.SetActive(false);
        }
    }
}
