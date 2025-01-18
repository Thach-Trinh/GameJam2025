using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePath : MonoBehaviour
{
    [SerializeField] private RaceTerrain[] terrains;
    public RaceTerrain this[int index] => terrains[index];
    public int GetTerrainNum() => terrains.Length;
}
