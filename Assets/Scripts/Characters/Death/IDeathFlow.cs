using System;

namespace Characters.Death
{
    public interface IDeathFlow
    {
        event Action OnDeathStart;
        event Action OnDeathEnd;
    }
}