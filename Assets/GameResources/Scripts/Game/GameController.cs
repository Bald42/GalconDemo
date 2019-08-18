using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер игры
/// </summary>
public class GameController : MonoBehaviour
{
    #region Subscribes / UnSubscribes
    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        UnSubscribe();
    }

    /// <summary>Подписки</summary>
    private void Subscribe()
    {
        GeneratePlanets.OnSpawnPlanets += OnSpawnPlanets;
    }

    /// <summary>Отписки</summary>
    private void UnSubscribe()
    {
        GeneratePlanets.OnSpawnPlanets -= OnSpawnPlanets;
    }

    /// <summary>
    /// Обработчик события спавна планет
    /// </summary>
    private void OnSpawnPlanets()
    {
        CheckStartPlanet();
    }
    #endregion Subscribes / UnSubscribes 

    /// <summary>
    /// Находим стартовую планету
    /// </summary>
    private void CheckStartPlanet()
    {
        while (true)
        {
            int randomPlanet = Random.RandomRange(0, PlanetPool.Instance.PlanetControllers.Count);
            if (PlanetPool.Instance.PlanetControllers[randomPlanet].TypeThisPlanet == PlanetController.TypePlanet.Empty)
            {
                PlanetPool.Instance.PlanetControllers[randomPlanet].AssignToPlayerOnStart();
                break;
            }
        }
    }
}