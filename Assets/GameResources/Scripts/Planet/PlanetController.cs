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

    [SerializeField]
    private MeshRenderer materialPlanet = null;

    [SerializeField]
    private InterfacePlanet interfacePlanet = null;

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
        SelectObjects.OnSelectPlanets += OnSelectPlanets;
    }

    /// <summary>Отписки</summary>
    private void UnSubscribe()
    {
        SelectObjects.OnSelectPlanets -= OnSelectPlanets;
    }

    /// <summary>
    /// Обработчик события выделения планет
    /// </summary>
    private void OnSelectPlanets(GameObject currentPlanet, bool isActive)
    {
        if (currentPlanet == gameObject)
        {
            interfacePlanet.ActiveIndicatorSelect(isActive);
        }
    }
    #endregion Subscribes / UnSubscribes 

    /// <summary>
    /// Присваиваем планету игроку
    /// </summary>
    public void AssignToPlayer()
    {
        typeThisPlanet = TypePlanet.Player;
        CheckNewParametrs();
    }

    /// <summary>
    /// Изменяем параметры планеты
    /// </summary>
    private void CheckNewParametrs()
    {
        switch (typeThisPlanet)
        {
            case TypePlanet.Empty:
                {
                    break;
                }
            case TypePlanet.Enemy:
                {
                    break;
                }
            case TypePlanet.Player:
                {
                    materialPlanet.material.color = Color.red;
                    if (!interfacePlanet)
                    {
                        interfacePlanet = InterfacePool.Instance.AddInterface(transform.position);
                    }
                    gameObject.tag = "Player";
                    interfacePlanet.gameObject.SetActive(true);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}