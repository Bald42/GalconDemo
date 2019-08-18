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

    [SerializeField]
    private bool isSelect = false;

    [SerializeField]
    private int numberShips = 0;

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
        SelectObjects.OnAttackPlanet += OnAttack;
    }

    /// <summary>Отписки</summary>
    private void UnSubscribe()
    {
        SelectObjects.OnSelectPlanets -= OnSelectPlanets;
        SelectObjects.OnAttackPlanet -= OnAttack;
    }

    /// <summary>
    /// Обработчик события выделения планет
    /// </summary>
    private void OnSelectPlanets(GameObject currentPlanet, bool isActive)
    {
        if (currentPlanet == gameObject)
        {
            interfacePlanet.ActiveIndicatorSelect(isActive);
            isSelect = isActive;
        }
    }

    /// <summary>
    /// Обработчик события атаки
    /// </summary>
    private void OnAttack (GameObject _targetPlanet)
    {
        if (isSelect && numberShips > 1)
        {
            StartCoroutine(Attack((int)(numberShips * 0.5f), _targetPlanet));
            numberShips = (int)(numberShips * 0.5f);
        }
    }
    #endregion Subscribes / UnSubscribes 

    /// <summary>
    /// Корутина аттаки
    /// </summary>
    private IEnumerator Attack(int _numberShips, GameObject _targetPlanet)
    {
        Debug.LogError("_numberShips = " + _numberShips);
        while (_numberShips > 0)
        {
            ShipPool.Instance.OnAttack(_targetPlanet, gameObject);
            _numberShips--;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    private void Init ()
    {
        if (typeThisPlanet != TypePlanet.Player)
        {
            numberShips = Random.Range(50, 100);
        }
        StartCoroutine(AddShips());
    }

    /// <summary>
    /// Присваиваем планету игроку на старте
    /// </summary>
    public void AssignToPlayerOnStart()
    {
        numberShips = 50;
        AssignToPlayer();
    }

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

    /// <summary>
    /// Корутина добавления кораблей
    /// </summary>
    private IEnumerator AddShips()
    {
        while (true)
        {
            if (typeThisPlanet != TypePlanet.Empty)
            {
                numberShips += 1;
                if (interfacePlanet)
                {
                    interfacePlanet.ViewText(numberShips.ToString());
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}