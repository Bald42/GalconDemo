using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Вращаем планету
/// </summary>
public class PlanetRotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 vectorRotation = Vector3.zero;

    [SerializeField]
    private bool isRandom = false;

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    private void Init()
    {
        if (isRandom)
        {
            vectorRotation.x = Random.Range(-vectorRotation.x, vectorRotation.x);
            vectorRotation.y = Random.Range(-vectorRotation.y, vectorRotation.y);
            vectorRotation.z = Random.Range(-vectorRotation.z, vectorRotation.z);
        }
    }

    private void Update()
    {
        Rotation();
    }

    /// <summary>
    /// Вращаем
    /// </summary>
    private void Rotation()
    {
        transform.Rotate(vectorRotation);
    }
}

