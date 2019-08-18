using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Пулл планет
/// </summary>
public class PlanetPool : MonoBehaviour
{
    public static PlanetPool Instance = null;

    public List <GameObject> PlanetObjects = new List<GameObject>();

    public List <PlanetController> PlanetControllers = new List<PlanetController>();

    private void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    private void Init ()
    {
        Instance = this;
    }

    /// <summary>
    /// Добавляем планету
    /// </summary>
    public void AddPlanet(GameObject newPlanet)
    {
        PlanetObjects.Add(newPlanet);
        PlanetControllers.Add(newPlanet.GetComponent<PlanetController>());
    }
}
