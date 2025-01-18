using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : ScaredObstacleView
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _mirrorNormal;
    [SerializeField] private Sprite _mirrorAppearSelf;
    [SerializeField] private Sprite _mirrorBroken;
    [SerializeField] private float delay;
    protected override IEnumerator PlayScaredAnimationChoiceCorrectIE()
    {
        Debug.Log("PlayScaredAnimationChoiceCorrectIE");
        _spriteRenderer.sprite = _mirrorNormal;
        yield return new WaitForSeconds(delay);
        _spriteRenderer.sprite = _mirrorAppearSelf;
        yield return new WaitForSeconds(delay);
        _spriteRenderer.sprite = _mirrorBroken;
        yield return new WaitForSeconds(delay);
        _spriteRenderer.enabled = false;
    }

    protected override IEnumerator PlayScaredAnimationChoiceInCorrectIE()
    {
        _spriteRenderer.sprite = _mirrorNormal;
        yield return new WaitForSeconds(delay);
        _spriteRenderer.sprite = _mirrorAppearSelf;
    }
}
