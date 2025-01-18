using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private ParticleDataSO _particleDataSO;
    public GameObject GetParticle(ParticleType particleType)
    {
        ParticleItem particleItem = _particleDataSO.GetParticleItem(particleType);
        if (particleItem == null)
        {
            Debug.LogError("ParticleItem is null");
            return null;
        }
        GameObject particleObject = Instantiate(particleItem.particleObject);
        return particleObject;
    }

    [ContextMenu("Test ShockWave Particle")]
    public void TestSmokeParticle()
    {
        GameObject particleObject = GetParticle(ParticleType.Smoke);
        particleObject.transform.position = Vector3.zero;
    }
}

[System.Serializable]
public class ParticleItem
{
    public ParticleType particleType;
    public GameObject particleObject;
}

public enum ParticleType
{
    None,
    ShockWave,
    Smoke,
}
