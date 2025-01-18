using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryObstacleView : ObstacleView
{
    [SerializeField] private Animator _animator;
    private static readonly int TriggeredHash = Animator.StringToHash(Triggered);
    private const string Triggered = "Triggered";
    
    private static readonly int DisappearHash = Animator.StringToHash(Disappear);
    private const string Disappear = "Disappear";
    
    public override void PlayObstacleEnterTriggerBoxAnimation()
    {
        if (_animator != null)
        {
            TriggerAnimation(TriggeredHash);
        }
    }
    
    public override void OnPlayerSuccessInteract()
    {
        PlayParticleSmoke();
        AudioController.Instance.PlaySound(SoundName.THUD);
        TriggerAnimation(DisappearHash);
    }

    void PlayParticleSmoke()
    {
        var particleObject = ParticleManager.Instance.GetParticle(ParticleType.Smoke);
        if (particleObject == null)
        {
            Debug.LogError("Particle smoke is null");
            return;
        }
        particleObject.transform.position = _animator.transform.position;
        var particle = particleObject.GetComponent<ParticleSystem>();
        if (particle != null)
        {
            particle.Play();
        }
    }
    
    void TriggerAnimation(int hash)
    {
        _animator.SetTrigger(hash);
    }
}
