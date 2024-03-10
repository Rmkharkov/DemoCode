using System;
using Battle;
using Collide;
using Moving;
using UnityEngine;

namespace Animals
{
    public class AnimalLinksView : BaseView<AnimalLinksModel, AnimalLinksController>, IAnimalLinks
    {
        public EAnimalSide AnimalSide => Model.AnimalSide;
        public EAnimalType AnimalType => Model.AnimalType;
        public IMovable Movable => Model.Movable;
        public ICollide Collide => Model.Collide;
        public IDamageable Damageable => Model.Damageable;

        private void Start()
        {
            Movable.SetFeltEvent(Collide.CollideWithFloor);
            Movable.SetDeadEvent(Damageable.GetDamaged);
        }

        public void SetAnimal(EAnimalSide animalSide, EAnimalType animalType) =>
            Model.SetAnimal(animalSide, animalType);

        public void SetMyParent(Transform parent) => Model.ParentableTransform.SetParent(parent);
    }
}