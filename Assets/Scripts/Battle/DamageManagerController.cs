using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Animals;
using Collide;
using Cysharp.Threading.Tasks;
using Spawn;
using UniRx;
using UnityEngine;

namespace Battle
{
    public class DamageManagerController : BaseController<DamageManagerModel>
    {
        private struct LastBattle
        {
            public ICollide[] Collides;
            public float BattleTimeStamp;

            public LastBattle(ICollide[] collides)
            {
                Collides = collides;
                BattleTimeStamp = Time.time;
            }

            public bool Compare(ICollide[] collides)
            {
                foreach (var collide in collides)
                {
                    if (Collides.Contains(collide))
                    {
                        continue;
                    }

                    return false;
                }
                return true;
            }
        }
        
        public ReactiveCommand<IDamageable> Damaged { get; } = new ReactiveCommand<IDamageable>();
        public ReactiveCommand<IAnimalLinks> WinnerAnimal { get; } = new ReactiveCommand<IAnimalLinks>();
        private ISpawner _spawner;

        private readonly List<LastBattle> _battles = new List<LastBattle>();

        public async UniTaskVoid Init(ISpawner spawner, CancellationToken token)
        {
            _spawner = spawner;
            await LoopCheckBattles(token);
        }
        
        public void TryDamageOneOf(ICollide me, ICollide enemy)
        {
            var battle = new[] {me, enemy};
            _battles.Add(new LastBattle(battle));
        }

        private async UniTask LoopCheckBattles(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                for (var i = _battles.Count - 1; i >= 0; i--)
                {
                    if (_battles.Count > i && _battles[i].BattleTimeStamp < Time.time)
                    {
                        JudgeBattle(_battles[i]);
                    }
                }

                await UniTask.Yield();
            }
        }

        private void JudgeBattle(LastBattle battle)
        {
            var animals = _spawner.LiveAnimals.FindAll(c => c.Collide == battle.Collides[0] || c.Collide == battle.Collides[1]);
            
            if (animals.Count < 2) return;
            
            if (animals[0].AnimalSide == EAnimalSide.Predator)
            {
                Damaged.Execute(animals[1].Damageable);
                WinnerAnimal.Execute(animals[0]);
            } 
            else if (animals[1].AnimalSide == EAnimalSide.Predator)
            {
                Damaged.Execute(animals[0].Damageable);
                WinnerAnimal.Execute(animals[1]);
            }

            for (var i = _battles.Count - 1; i >= 0; i--)
            {
                if (_battles[i].Compare(battle.Collides))
                {
                    _battles.RemoveAt(i);
                }
            }
        }
    }
}