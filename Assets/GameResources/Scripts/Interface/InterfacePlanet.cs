using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Контроллер интерфейса корабля
/// </summary>
public class InterfacePlanet : MonoBehaviour
{
    [SerializeField]
    private GameObject indicatorSelect = null;

    [SerializeField]
    private Text textShips = null;

    private void Awake()
    {
        indicatorSelect.SetActive(false);
    }

    /// <summary>
    /// Включаем/выключаем индикатор выбора
    /// </summary>
    public void ActiveIndicatorSelect(bool isActive)
    {
        indicatorSelect.SetActive(isActive);
    }

    /// <summary>
    /// Выводим текст
    /// </summary>
    public void ViewText(string newText)
    {
        textShips.text = newText;
    }
}
