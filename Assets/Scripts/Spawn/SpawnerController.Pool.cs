using System.Collections.Generic;
using Animals;
using Battle;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Spawn
{
    public partial class SpawnerController
    {
        public readonly List<IAnimalLinks> liveAnimals = new List<IAnimalLinks>();
        private readonly List<IAnimalLinks> _poolAnimals = new List<IAnimalLinks>();
        private DiContainer _container;

        public void Init(DiContainer container, IDamageManager damageManager, GameObject disposable)
        {
            _container = container;
            damageManager.Damaged.Subscribe(OnDamagedAnimal).AddTo(disposable);
        }

        private async UniTask<IAnimalLinks> GetAnimal(EAnimalType type)
        {
            var toReturn = _poolAnimals.Find(c => c.AnimalType == type);
            if (toReturn == null)
            {
                toReturn = await GetNewAnimal(type);
            }
            else
            {
                RevealAnimal(toReturn);
            }
            return toReturn;
        }

        private void OnDamagedAnimal(IDamageable damageable)
        {
            var animal = liveAnimals.Find(c => c.Damageable == damageable);
            HideAnimal(animal);
        }

        private void HideAnimal(IAnimalLinks animalLinks)
        {
            liveAnimals.Remove(animalLinks);
            _poolAnimals.Add(animalLinks);
            animalLinks.SetMyParent(Model.AnimalsPoolParent);
        }

        private void RevealAnimal(IAnimalLinks animalLinks)
        {
            liveAnimals.Add(animalLinks);
            _poolAnimals.Remove(animalLinks);
            animalLinks.SetMyParent(Model.AnimalsLiveParent);
        }
        
        private async UniTask<IAnimalLinks> GetNewAnimal(EAnimalType type)
        {
            var prefab = await Addressables.LoadAssetAsync<GameObject>(type.ToString());
            var toReturn = _container.InstantiatePrefabForComponent<AnimalLinksView>(prefab);
            var cachedObject = toReturn.GetComponent<IAnimalLinks>();
            liveAnimals.Add(cachedObject);
            return cachedObject;
        }
    }
}