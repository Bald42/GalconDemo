using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Пул интерфейсов
/// </summary>
public class InterfacePool : MonoBehaviour
{
    public static InterfacePool Instance = null;

    [SerializeField]
    private GameObject prefabInterface = null;

    [SerializeField]
    private Transform parentInterface = null;

    [SerializeField]
    private List<InterfacePlanet> interfacesPlanets = new List<InterfacePlanet>();

    [SerializeField]
    private Camera mainCamera = null;

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
    /// Добавляем интерфейс
    /// </summary>
    public InterfacePlanet AddInterface (Vector3 targetPosition)
    {
        bool isFind = false;
        InterfacePlanet currentInterface = new InterfacePlanet();

        for (int i=0; i < interfacesPlanets.Count; i++)
        {
            if (!interfacesPlanets[i].gameObject.activeSelf)
            {
                currentInterface = interfacesPlanets[i];
                isFind = true;
                break;
            }
        }

        if (!isFind)
        {
            currentInterface = Instantiate(prefabInterface, Vector3.zero, Quaternion.identity, parentInterface).GetComponent<InterfacePlanet>();
            interfacesPlanets.Add(currentInterface);
        }

        currentInterface.transform.position = NewPosition(targetPosition);

        return currentInterface;
    }

    /// <summary>
    /// Передаём новую позицию
    /// </summary>
    private Vector3 NewPosition(Vector3 targetPosition)
    {
        Vector3 point = mainCamera.WorldToViewportPoint(targetPosition);
        Vector3 drawPositionVector;

        drawPositionVector.x = point.x * Screen.width;
        drawPositionVector.y = point.y * Screen.height;
        drawPositionVector.z = 0;

        return drawPositionVector;
    }     
}