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
        while (spriteIndex < spriteRequired)
        {
            ticker += Time.deltaTime*_globalTimeScale;
            if (ticker >= delay)
            {
                _spriteRenderer.sprite = _mirrorSprites[spriteIndex];
                spriteIndex++;
                ticker = 0;
            }
            yield return null;
        }
        _spriteRenderer.enabled = false;
    }

    protected override IEnumerator PlayScaredAnimationChoiceInCorrectIE()
    {
        float ticker = 0;
        int spriteIndex = 0;
        int spriteRequired = 2;
        while (spriteIndex < spriteRequired)
        {
            ticker += Time.deltaTime*_globalTimeScale;
            if (ticker >= delay)
            {
                _spriteRenderer.sprite = _mirrorSprites[spriteIndex];
                spriteIndex++;
                ticker = 0;
            }
            yield return null;
        }
    }
}
