using System;
using System.Collections.Generic;
using Animals;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Spawn
{
    public partial class SpawnerController
    {
        private List<IAnimalLinks> _liveAnimals = new List<IAnimalLinks>();
        private readonly List<IAnimalLinks> _poolAnimals = new List<IAnimalLinks>();

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

        private void HideAnimal(IAnimalLinks animalLinks)
        {
            _liveAnimals.Remove(animalLinks);
            _poolAnimals.Add(animalLinks);
            animalLinks.SetMyParent(Model.AnimalsPoolParent);
        }

        private void RevealAnimal(IAnimalLinks animalLinks)
        {
            _liveAnimals.Remove(animalLinks);
            _poolAnimals.Add(animalLinks);
            animalLinks.SetMyParent(Model.AnimalsPoolParent);
        }
        
        public async UniTask<IAnimalLinks> GetNewAnimal(EAnimalType type)
        {
            var toReturn = Addressables.InstantiateAsync(type.ToString());
            var cachedObject = (await toReturn.Task).GetComponent<IAnimalLinks>();
            _liveAnimals.Add(cachedObject);
            return cachedObject;
        }
    }
}