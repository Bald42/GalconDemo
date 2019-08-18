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
    public NavMeshAgent agent = null;
    
    /// <summary>
    /// Говорим куда двигатся кораблю
    /// </summary>
    public void MoveTo (Vector3 pointToMove)
    {
        Debug.LogError("agent = " + agent);
        Debug.LogError("pointToMove = " + pointToMove);
        agent.SetDestination(pointToMove);
        StartCoroutine(TempSetActive());
    }

    private IEnumerator TempSetActive()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);       
    }
}
