using System.Collections.Generic;
using Animals;

namespace Spawn
{
    public interface ISpawner
    {
        List<IAnimalLinks> LiveAnimals { get; }
    }
}