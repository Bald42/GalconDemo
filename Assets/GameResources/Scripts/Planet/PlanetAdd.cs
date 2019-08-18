using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Добавляем планету
/// </summary>
public class PlanetAdd : MonoBehaviour
{
    private void Awake()
    {
        PlanetPool.Instance.AddPlanet(gameObject);
    }
}
