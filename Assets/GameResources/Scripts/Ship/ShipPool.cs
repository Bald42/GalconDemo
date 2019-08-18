using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Пулл кораблей
/// </summary>
public class ShipPool : MonoBehaviour
{
    public static ShipPool Instance = null;

    [SerializeField]
    private GameObject prefabShip = null;

    [SerializeField]
    private Transform parentShips = null;

    [SerializeField]
    private List<ShipParametrs> shipParametrs = new List<ShipParametrs>();

    [SerializeField]
    private List<ShipMove> shipMove = new List<ShipMove>();

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
    private void Init()
    {
        Instance = this;
    }

    /// <summary>
    /// В атаку
    /// </summary>
    public void OnAttack (GameObject target, GameObject who)
    {
        bool isFind = false;
        ShipParametrs currentShipParametrs = new ShipParametrs();
        ShipMove currentShipMove = new ShipMove();

        for (int i = 0; i < shipParametrs.Count; i++)
        {
            if (!shipParametrs[i].gameObject.activeSelf)
            {
                currentShipParametrs = shipParametrs[i];                
                isFind = true;
                break;
            }
        }

        if (!isFind)
        {
            currentShipParametrs = Instantiate(prefabShip, who.transform.position, Quaternion.identity, parentShips).GetComponent<ShipParametrs>();
            currentShipMove = currentShipParametrs.GetComponent<ShipMove>();
            shipParametrs.Add(currentShipParametrs);
            shipMove.Add(currentShipMove);
        }

        currentShipParametrs.transform.position = who.transform.position;
        currentShipParametrs.gameObject.SetActive(true);
        currentShipMove.MoveTo(target.transform.position);        
    }
}