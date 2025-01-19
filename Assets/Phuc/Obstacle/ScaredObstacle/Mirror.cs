using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : ScaredObstacleView
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> _mirrorSprites;
    [SerializeField] private float delay;
    protected override IEnumerator PlayScaredAnimationChoiceCorrectIE()
    {
        float ticker = 0;
        int spriteIndex = 0;
        int spriteRequired = 3;
        _spriteRenderer.sprite = _mirrorSprites[spriteIndex];
        while (true)
        {
            ticker += Time.deltaTime*_globalTimeScale;
            if (ticker >= delay)
            {
                spriteIndex++;
                if (spriteIndex >= spriteRequired)
                {
                    break;
                }
                if (spriteIndex > 0)
                {
                    SpawnSmokeParticle(_spriteRenderer.transform.position);
                }

                if (spriteIndex == 1)
                {
                    AudioController.Instance.PlaySound(SoundName.SCREAM);
                }
                _spriteRenderer.sprite = _mirrorSprites[spriteIndex];
                ticker = 0;
            }
            yield return null;
        }
        SpawnSmokeParticle(_spriteRenderer.transform.position);
        _spriteRenderer.enabled = false;
    }

    protected override IEnumerator PlayScaredAnimationChoiceInCorrectIE()
    {
        float ticker = 0;
        int spriteIndex = 0;
        int spriteRequired = 2;
        _spriteRenderer.sprite = _mirrorSprites[spriteIndex];
        while (true)
        {
            ticker += Time.deltaTime*_globalTimeScale;
            if (ticker >= delay)
            {
                spriteIndex++;
                if (spriteIndex >= spriteRequired)
                {
                    break;
                }
                if (spriteIndex > 0)
                {
                    SpawnSmokeParticle(_spriteRenderer.transform.position);
                }
                _spriteRenderer.sprite = _mirrorSprites[spriteIndex];
                ticker = 0;
            }
            yield return null;
        }
    }

    void SpawnSmokeParticle(Vector2 position)
    {
        if (ParticleManager.Instance != null)
        {
            var particleGO=ParticleManager.Instance.GetParticle(ParticleType.Smoke);
            if (particleGO == null)
            {
                Debug.LogError("Particle is null");
                return;
            }
            particleGO.transform.position = position;
            var particle = particleGO.GetComponent<ParticleSystem>();
            if (particle != null)
            {
                particle.Play();
            }
        }
    }
}
