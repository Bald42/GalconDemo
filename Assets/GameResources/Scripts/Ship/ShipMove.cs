using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Движение кораблика
/// </summary>
public class ShipMove : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent = new NavMeshAgent ();
    
    /// <summary>
    /// Говорим куда двигатся кораблю
    /// </summary>
    public void MoveTo (Vector3 pointToMove)
    {
        agent.SetDestination(pointToMove);
    }
}