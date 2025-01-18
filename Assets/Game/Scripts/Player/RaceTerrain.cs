using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RaceTerrain : MonoBehaviour
{
    //[SerializeField] private MovementType type;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    //public MovementType Type => type;
    public Transform StartPoint => startPoint;
    public Transform EndPoint => endPoint;
    //public void OnCharacterStart(Character character)
    //{
    //    character.EnterTerrain(this);
    //}

    private void OnDrawGizmos()
    {
        Debug.DrawLine(startPoint.position, endPoint.position, Color.green);
    }
}
