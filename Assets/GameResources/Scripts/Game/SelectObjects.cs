using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectObjects : MonoBehaviour
{
    public delegate void SelectPlanetsEventHandler(GameObject planet, bool isActive);
    public static event SelectPlanetsEventHandler OnSelectPlanets = delegate { };

    private static List<GameObject> unitSelected; // массив выделенных юнитов

    [SerializeField]
    private GUISkin skin;

    private Rect rect;
    private bool draw;
    private Vector2 startPos;
    private Vector2 endPos;

    private void Awake()
    {
        unitSelected = new List<GameObject>();
    }

    // проверка, добавлен объект или нет
    private bool CheckUnit(GameObject unit)
    {
        bool result = false;
        foreach (GameObject u in unitSelected)
        {
            if (u == unit) result = true;
        }
        return result;
    }

    private void Select()
    {
        if (unitSelected.Count > 0)
        {
            for (int j = 0; j < unitSelected.Count; j++)
            {
                // делаем что-либо с выделенными объектами
                // unitSelected[j].GetComponentInChildren<MeshRenderer>().material.color = Color.red;
                OnSelectPlanets(unitSelected[j], true);
            }
        }
    }

    private void Deselect()
    {
        if (unitSelected.Count > 0)
        {
            for (int j = 0; j < unitSelected.Count; j++)
            {
                // отменяем то, что делали с объектоми
                //unitSelected[j].GetComponentInChildren<MeshRenderer>().material.color = Color.white;
                OnSelectPlanets(unitSelected[j], false);
            }
        }
    }

    private void OnGUI()
    {
        GUI.skin = skin;
        GUI.depth = 99;

        if (Input.GetMouseButtonDown(0))
        {
            Deselect();
            startPos = Input.mousePosition;
            draw = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            draw = false;
            Select();
        }

        if (draw)
        {
            unitSelected.Clear();
            endPos = Input.mousePosition;
            if (startPos == endPos) return;

            rect = new Rect(Mathf.Min(endPos.x, startPos.x),
                            Screen.height - Mathf.Max(endPos.y, startPos.y),
                            Mathf.Max(endPos.x, startPos.x) - Mathf.Min(endPos.x, startPos.x),
                            Mathf.Max(endPos.y, startPos.y) - Mathf.Min(endPos.y, startPos.y)
                            );

            GUI.Box(rect, "");

            for (int j = 0; j < PlanetPool.Instance.PlanetObjects.Count; j++)
            {
                // трансформируем позицию объекта из мирового пространства, в пространство экрана
                Vector2 tmp = new Vector2(Camera.main.WorldToScreenPoint(PlanetPool.Instance.PlanetObjects[j].transform.position).x, Screen.height - 
                    Camera.main.WorldToScreenPoint(PlanetPool.Instance.PlanetObjects[j].transform.position).y);

                if (rect.Contains(tmp) && PlanetPool.Instance.PlanetObjects[j].tag == "Player") // проверка, находится-ли текущий объект в рамке
                {
                    if (unitSelected.Count == 0)
                    {
                        unitSelected.Add(PlanetPool.Instance.PlanetObjects[j]);
                    }
                    else if (!CheckUnit(PlanetPool.Instance.PlanetObjects[j]))
                    {
                        unitSelected.Add(PlanetPool.Instance.PlanetObjects[j]);
                    }
                }
            }
        }
    }
}