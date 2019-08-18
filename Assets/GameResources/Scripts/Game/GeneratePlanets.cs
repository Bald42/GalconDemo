using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Генерация планет
/// </summary>
public class GeneratePlanets : MonoBehaviour
{
    public delegate void SpawnPlanetsEventHandler();
    public static event SpawnPlanetsEventHandler OnSpawnPlanets = delegate { };

    [SerializeField]
    private PlanetScriptableObject planetParametrs = null;

    [SerializeField]
    private Transform platform = null;

    [SerializeField]
    private Transform poolPlanets = null;

    [SerializeField]
    private GameObject prefabPlanet = null;

    /*
    [SerializeField]
    private Vector2 randomScale = Vector2.zero;

    [SerializeField]
    private int numberPlanets = 10;

    [SerializeField]
    private float distanceToNearestPlanet = 2f;
    */

    private List<GameObject> planets = new List<GameObject>();

    private List<NearbyPlanet> nearbyPlanets = new List<NearbyPlanet>();

    private int currentNumber = 0;
    private GameObject newPlanet = null;

    private void Awake()
    {
        ScalePlatform();
        SpawnPlanets();
    }

    /// <summary>
    /// Скейлим платформу под размер экрана
    /// </summary>
    private void ScalePlatform()
    {
        Vector3 newScale = Vector3.one;
        newScale.x *= Screen.width * 0.001f;
        newScale.z *= Screen.height * 0.001f;
        platform.transform.localScale = newScale;
    }

    /// <summary>
    /// Спавним планеты
    /// </summary>
    private void SpawnPlanets()
    {
        for (int i=0; i < planetParametrs.NumberPlanets; i++)
        {
            newPlanet = Instantiate(prefabPlanet, Vector3.zero, Quaternion.identity);
            planets.Add(newPlanet);
            newPlanet.transform.localScale = Vector3.one * Random.RandomRange(planetParametrs.RandomScale.x, planetParametrs.RandomScale.y);
            currentNumber++;
            newPlanet.transform.localPosition = FindNewPosition();
            newPlanet.name = newPlanet.name.Replace("(Clone)", " " + i.ToString());            
            newPlanet.transform.SetParent(poolPlanets.transform, true);            
        }
        OnSpawnPlanets();
    }

    /// <summary>
    /// Случайная позиция
    /// </summary>
    private Vector3 RandomtPosition()
    {
        Vector3 randomPosition = Vector3.zero;
        randomPosition.x = Random.Range(-platform.localScale.x, platform.localScale.x) * 5f;
        randomPosition.z = Random.Range(-platform.localScale.z, platform.localScale.z) * 5f;
        return randomPosition;
    }
    
    /// <summary>
    /// Находим позицию для планеты выполняя условия
    /// </summary>
    private Vector3 FindNewPosition ()
    {
        Vector3 _newPosition = Vector3.zero;
        NearbyPlanet newNearbyPlanets = new NearbyPlanet();
        nearbyPlanets.Add(newNearbyPlanets);

        while (true)
        {
            float distance = 0f;
            float nearbyDistance = 0f;
            _newPosition = RandomtPosition();            

            //заполняем лист соседних планет
            for (int i = 0; i < planets.Count; i++)
            {
                distance = (_newPosition - planets[i].transform.position).magnitude -
                    (newPlanet.transform.localScale.x + planets[i].transform.localScale.x) * 0.5f;

                if (distance <= planetParametrs.DistanceToNearestPlanet)
                {
                    nearbyDistance += planets[i].transform.localScale.x * 0.5f;
                    newNearbyPlanets.NearbyPlanets.Add(planets[i]);
                    newNearbyPlanets.NearbyDistance += planets[i].transform.localScale.x * 0.5f;
                }
            }

            if (nearbyPlanets.Count == 1 || nearbyPlanets[currentNumber-1].NearbyPlanets.Count == 0)
            {
                break;
            }

            bool isSatisfy = true;
            //проверям удовлетворяет ли эта координата условиям cпавна
            for (int i=0; i < nearbyPlanets[currentNumber - 1].NearbyPlanets.Count; i++)
            {                
                distance = (_newPosition - nearbyPlanets[currentNumber - 1].NearbyPlanets[i].transform.position).magnitude -
                    (newPlanet.transform.localScale.x + nearbyPlanets[currentNumber - 1].NearbyPlanets[i].transform.localScale.x) * 0.5f;
                
                if (distance < nearbyDistance)
                {
                    isSatisfy = false;
                    break;
                }
            }

            if (isSatisfy)
            {
                break;
            }
        }
        return _newPosition;
    }

    [System.Serializable]
    public class NearbyPlanet
    {
        public List<GameObject> NearbyPlanets = new List<GameObject>();
        public float NearbyDistance = 0f;
    }
}