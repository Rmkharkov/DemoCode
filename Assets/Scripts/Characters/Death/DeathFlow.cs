using System;
using System.Threading.Tasks;
using Characters.Fight;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Death
{
    public interface IDeathFlow
    {
        UnityEvent DeathStart { get; }
        UnityEvent DeathEnd { get; }
    }
    public class DeathFlow : MonoBehaviour, IDeathFlow
    {
        [SerializeField] private CharacterGetHitPart getHitPart;
        [SerializeField] private ParticleSystem particlesEffect;
        [SerializeField] private GameObject characterBody;

        private readonly UnityEvent _startEvent = new UnityEvent();
        public UnityEvent DeathStart => _startEvent;
        
        private readonly UnityEvent _endEvent = new UnityEvent();
        public UnityEvent DeathEnd => _endEvent;

        private void Start()
        {
            getHitPart.DeathEvent.AddListener(RunShowDeath);
        }

        private async void RunShowDeath()
        {
            await ShowDeath();
        }

        private async Task ShowDeath()
        {
            _startEvent.Invoke();
            particlesEffect.Play();
            characterBody.SetActive(false);
            await Task.Delay(TimeSpan.FromSeconds(3));
            _endEvent.Invoke();
        }
    }
}