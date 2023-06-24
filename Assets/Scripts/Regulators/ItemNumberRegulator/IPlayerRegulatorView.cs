using System;
using Characters;

namespace Regulators.ItemNumberRegulator
{
    public interface IPlayerRegulatorView : IRegulatorView
    {
        event Action<GameplayItemLinks> OnPlayerUpdated;
    }
}