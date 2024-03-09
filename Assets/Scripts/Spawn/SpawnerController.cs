using System;
using System.Threading;
using Animals;
using Cysharp.Threading.Tasks;
using Moving;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Spawn
{
    public partial class SpawnerController : BaseController<SpawnerModel>
    {
        private SpawnConfig UsedSpawnConfig => SpawnConfig.Instance;
        private MovesConfigs UsedMovesConfig => MovesConfigs.Instance;

        public async UniTaskVoid StartSpawn(CancellationToken token)
        {
            await LoopSpawn(token);
        }

        private async UniTask LoopSpawn(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                SpawnNextRandomAnimal().Forget();
                await UniTask.Delay(UsedSpawnConfig.RandomSpawnDelay, cancellationToken: token);
            }
        }

        private async UniTaskVoid SpawnNextRandomAnimal()
        {
            var typesLength = Enum.GetNames(typeof(EAnimalType)).Length;
            var type = (EAnimalType)UnityEngine.Random.Range(1, typesLength);

            var usedParameters = UsedSpawnConfig.GetParametersByType(type);

            var animal = await GetAnimal(type);
            if (animal != null)
            {
                animal.SetAnimal(usedParameters.AnimalConfig.AnimalSide, usedParameters.AnimalConfig.AnimalType);

                PlaceAnimal(animal, usedParameters);
            }
        }

        private void PlaceAnimal(IAnimalLinks animalLinks, SpawnParameters parameters)
        {
            var position = Vector3.zero;
            var breakerCount = 100000;
            while (IsAnyForbiddenClose(position) && breakerCount > 0)
            {
                breakerCount--;
                position = RandomizedPosition;
            }
            
            animalLinks.Movable.SetParameters(UsedMovesConfig.GetConfigByAnimal(animalLinks.AnimalType).MoveParameters);
            animalLinks.Movable.SetPositionAndParent(position, Model.AnimalsLiveParent);
            animalLinks.Movable.RandomMove();
        }

        private Vector3 RandomizedPosition
        {
            get
            {
                var x = UnityEngine.Random.Range(-UsedSpawnConfig.ExtremeCoordsForSpawn.x,
                    UsedSpawnConfig.ExtremeCoordsForSpawn.x);                
                var z = UnityEngine.Random.Range(-UsedSpawnConfig.ExtremeCoordsForSpawn.y,
                    UsedSpawnConfig.ExtremeCoordsForSpawn.y);

                return new Vector3(x, 0, z);
            }
        }

        private bool IsAnyForbiddenClose(Vector3 position)
        {
            foreach (var animalLinks in _liveAnimals)
            {
                if (Vector3.Distance(animalLinks.Movable.Position, position) <
                    UsedSpawnConfig.MinDistanceToSpawnFromOtherObstacles)
                {
                    return true;
                }
            }

            return false;
        }
    }
}