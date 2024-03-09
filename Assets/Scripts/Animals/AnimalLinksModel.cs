using Collide;
using Moving;
using UnityEngine;

namespace Animals
{
    [System.Serializable]
    public class AnimalLinksModel : BaseModel
    {
        private EAnimalSide _animalSide;
        public EAnimalSide AnimalSide => _animalSide;
        
        private EAnimalType _animalType;
        public EAnimalType AnimalType => _animalType;

        [SerializeField] private Transform parentableTransform;
        public Transform ParentableTransform => parentableTransform;
        
        [SerializeField] private MoveItemView movable;
        public IMovable Movable => movable;
        [SerializeField] private CollisionParserView collide;

        public ICollide Collide => collide;

        public void SetAnimal(EAnimalSide animalSide, EAnimalType animalType)
        {
            _animalSide = animalSide;
            _animalType = animalType;
        }
    }
}