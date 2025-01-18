using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShockWave : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _shockwaveRenderers;
    [SerializeField] private float _eachShockwaveDuration = 0.5f;
    [ContextMenu("play")]
    public void OnEnable()
    {
        StartCoroutine(ShockWaveAnimation());
    }
    
    IEnumerator ShockWaveAnimation()
    {
        ResetShockwave();
        int spriteIndex = 0;
        float ticker = 0;
        while (true)
        {
            ticker += Time.deltaTime;
            if (ticker >= _eachShockwaveDuration)
            {
                ticker = 0;
                _shockwaveRenderers[spriteIndex].enabled = true;
                spriteIndex++;
                if (spriteIndex >= _shockwaveRenderers.Count)
                {
                    spriteIndex = 0;
                    ResetShockwave();
                }
            }
            yield return null;
        }
    }

    void ResetShockwave()
    {
        for (int i = 0; i < _shockwaveRenderers.Count; i++)
        {
            _shockwaveRenderers[i].enabled = false;
        }
    }
}
