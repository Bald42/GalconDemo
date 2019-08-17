using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Генерация планет
/// </summary>
public class GeneratePlanets : MonoBehaviour
{
    [SerializeField]
    private Transform platform = null;

    [SerializeField]
    private Transform poolPlanets = null;

    [SerializeField]
    private GameObject prefabPlanet = null;

    [SerializeField]
    private Vector2 randomScale = Vector2.zero;

    [SerializeField]
    private int numberPlanets = 10;

    [SerializeField]
    private float distanceToNearestPlanet = 2f;

    [SerializeField]
    private List<GameObject> planets = new List<GameObject>();

    [SerializeField]
    private List<Vector3> nearbyPlanets = new List<Vector3>();

    private void Awake()
    {
        Debug.LogError("000");
        ScalePlatform();
        SpawnPlanets();
    }

    /// <summary>
    /// Скейлим платформу под размер экрана
    /// </summary>
    private void ScalePlatform()
    {
        Debug.LogError(Screen.width + "/" + Screen.height);
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
        for (int i=0; i < numberPlanets; i++)
        {
            Debug.LogError("00");
            //TODO добавить рандом выше
            GameObject newPlanet = Instantiate(prefabPlanet, RandomtPosition(), Quaternion.identity);
            newPlanet.transform.localScale = Vector3.one * Random.RandomRange(randomScale.x, randomScale.y);
            newPlanet.transform.localPosition = SelectPosition();
            newPlanet.name = newPlanet.name.Replace("(Clone)", " " + i.ToString());            
            newPlanet.transform.SetParent(poolPlanets.transform, true);
            planets.Add(newPlanet);
        }
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
    /// Выбираем случайную позицию
    /// </summary>
    private Vector3 SelectPosition ()
    {
        //лешняя 
        Vector3 _planetPosition = Vector3.zero;
        FindNewPosition(_planetPosition);
        Debug.LogError("_planetPosition = " + _planetPosition);
        return _planetPosition;
    }

    /// <summary>
    /// Находим позицию для планеты выполняя условия
    /// </summary>
    private void FindNewPosition (Vector3 _newPosition)
    {
        if (planets.Count == 0)
        {
            return;
        }

        Debug.LogError("0");
        bool isFind = false;
        while (!isFind)
        {
            Debug.LogError("1");
            nearbyPlanets.Clear();
            float distance = 0f;
            float nearbyDistance = 0f;
            _newPosition.x = Random.Range(-platform.localScale.x, platform.localScale.x) * 5f;
            _newPosition.z = Random.Range(-platform.localScale.z, platform.localScale.z) * 5f;

            Debug.LogError("_newPosition = " + _newPosition);
            //заполняем лист соседних планет
            for (int i=0; i < planets.Count; i++)
            {
                distance = (_newPosition - planets[i].transform.position).sqrMagnitude;
                Debug.LogError("distance = " + distance);
                if (distance <= distanceToNearestPlanet)
                {
                    Debug.LogError("2");
                    nearbyDistance += planets[i].transform.localScale.x * 0.5f;
                    nearbyPlanets.Add(planets[i].transform.position);
                }
            }

            if (nearbyPlanets.Count == 0)
            {
                isFind = true;
            }

            //проверям удовлетворяет ли эта координата условиям cпавна
            for (int i=0; i < nearbyPlanets.Count; i++)
            {
                distance = (_newPosition - nearbyPlanets[i]).sqrMagnitude;
                if (distance > nearbyDistance)
                {
                    Debug.LogError("3");
                    isFind = true;
                }
            }
        }
    }
}