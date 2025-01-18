using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticleDataSO", menuName = "ScriptableObjects/ParticleDataSO", order = 1)]
public class ParticleDataSO : ScriptableObject
{
    public List<ParticleItem> particleItems;
    public ParticleItem GetParticleItem(ParticleType particleType)
    {
        return particleItems.Find(x => x.particleType == particleType);
    }
}
