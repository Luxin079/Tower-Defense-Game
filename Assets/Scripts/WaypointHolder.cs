using UnityEngine;
using System.Collections.Generic;

public class WaypointHolder : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPoints = new List<Transform>();    
    public List<Transform> GetWayPoints()
    {
        return wayPoints;
    }
}
