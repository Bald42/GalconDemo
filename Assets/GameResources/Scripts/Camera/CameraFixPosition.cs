using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Правим положение камеры в зависимости от разрешения экрана
/// </summary>
public class CameraFixPosition : MonoBehaviour
{
    private Vector3 newPosition = Vector3.zero;

    private const float horizontalСoefficient = 0.15f;
    private const float verticalСoefficient = 0.3f;

    private void Awake()
    {
        FixPosition();
    }

    private void FixPosition()
    {
        //TODO я это доделаю
        /*
        float ratio = Screen.width / Screen.height;

        if (ratio > 1)
        {
            newPosition.y = ratio / horizontalСoefficient;
        }
        else
        {
            newPosition.y = ratio / verticalСoefficient;
        }

        transform.localPosition = newPosition;
        */
    }
}