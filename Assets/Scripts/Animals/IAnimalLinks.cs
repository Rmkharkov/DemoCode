using Collide;
using Moving;

namespace Animals
{
    public interface IAnimalLinks : IParentable
    {
        EAnimalSide AnimalSide { get; }
        EAnimalType AnimalType { get; }
        IMovable Movable { get; }
        ICollide Collide { get; }

        void SetAnimal(EAnimalSide animalSide, EAnimalType animalType);
    }
}