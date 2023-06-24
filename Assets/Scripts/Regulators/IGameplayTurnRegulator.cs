using System;

namespace Regulators
{
    public interface IGameplayTurnRegulator
    {
        event Action OnStartMatchEvent;
        event Action OnCleanMatchEvent;
        event Action OnStopMatchEvent;
    }
}