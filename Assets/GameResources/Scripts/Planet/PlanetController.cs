using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер планеты
/// </summary>
public class PlanetController : MonoBehaviour
{
    public enum TypePlanet
    {
        Empty,
        Player,
        Enemy
    }

    [SerializeField]
    private TypePlanet typeThisPlanet = TypePlanet.Empty;

    public TypePlanet TypeThisPlanet
    {
        get
        {
            return typeThisPlanet;
        }
    }

}
