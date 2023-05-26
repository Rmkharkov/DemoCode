using System.Collections.Generic;
using Characters;

namespace Regulators.ItemNumberRegulator
{
    public interface IRegulatorView
    {
        List<GameplayItemLinks> SpawnedItems { get; }
    }
}