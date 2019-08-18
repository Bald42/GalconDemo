using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlanetScriptableObject", order = 1)]
public class PlanetScriptableObject : ScriptableObject
{
    public int NumberPlanets = 0;
    public float DistanceToNearestPlanet = 0f;
    public Vector2 RandomScale = Vector2.one;
    public float TimerCreateShip = 0f;
    public int NumberCreateShipForTime = 0;
    public int StartNumberShip = 0;
    public Vector2 RandomShipEmptyPlanet = Vector2.zero;
}
