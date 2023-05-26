using UnityEngine;
using UnityEngine.InputSystem;

namespace Misc
{
    public interface IPlayerInputReader
    {
        Vector2 JoystickShift { get; }
    }
    public class PlayerInputReader : MonoBehaviour, IPlayerInputReader
    {
        [SerializeField] private PlayerInput playerInput;

        public Vector2 JoystickShift => _joystickShift;
        private Vector2 _joystickShift;

        private void Update()
        {
            _joystickShift = playerInput.actions["Move"].ReadValue<Vector2>();
        }
    }
}