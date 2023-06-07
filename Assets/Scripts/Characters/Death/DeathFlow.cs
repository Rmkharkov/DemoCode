using System;
using System.Threading.Tasks;
using Characters.Fight;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Death
{
    public interface IDeathFlow
    {
        event Action OnDeathStart;
        event Action OnDeathEnd;
    }

    public class DeathFlow : BaseView<DeathFlowModel, DeathFlowController>, IDeathFlow
    {
        [SerializeField] private CharacterGetHitPart getHitPart;
        [SerializeField] private ParticleSystem particlesEffect;
        [SerializeField] private GameObject characterBody;

        public event Action OnDeathStart;
        public event Action OnDeathEnd;

        private void Start()
        {
            getHitPart.OnDeathEvent += RunShowDeath;
        }

        private async void RunShowDeath()
        {
            await ShowDeath();
        }

        private async Task ShowDeath()
        {
            OnDeathStart?.Invoke();
            particlesEffect.Play();
            characterBody.SetActive(false);
            await Task.Delay(TimeSpan.FromSeconds(3));
            OnDeathEnd?.Invoke();
        }
    }
}