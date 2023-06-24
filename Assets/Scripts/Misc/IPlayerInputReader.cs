using UnityEngine;

namespace Misc
{
    public interface IPlayerInputReader
    {
        Vector2 JoystickShift { get; }
    }
}