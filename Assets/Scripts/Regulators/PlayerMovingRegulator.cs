using Characters;
using Characters.Moving;
using Misc;
using Regulators.ItemNumberRegulator;
using UnityEngine;

namespace Regulators
{
    public class PlayerMovingRegulator : MonoBehaviour
    {
        [SerializeField] private PlayerInputReader playerInputReader;
        private IPlayerInputReader _playerInputReader;
        
        private IMovable _playerMove;
        private bool _canMove;

        private void Start()
        {
            playerInputReader.TryGetComponent(out _playerInputReader);
            PlayerRegulatorView.Instance.PlayerUpdated.AddListener(UpdatePlayerLink);
            GameplayTurnRegulator.Instance.StopMatchEvent.AddListener(DisableMove);
            GameplayTurnRegulator.Instance.StartMatchEvent.AddListener(EnableMove);
        }

        private void LateUpdate()
        {
            if (!_canMove) return;
            
            if (_playerInputReader.JoystickShift != Vector2.zero)
            {
                var speed = _playerMove.MoveSpeed;
                var direction = _playerMove.Transform.position + new Vector3(_playerInputReader.JoystickShift.x, 0, _playerInputReader.JoystickShift.y);
                _playerMove.MoveTo(Vector3.MoveTowards(_playerMove.Transform.position, direction, speed*Time.deltaTime)); 
            }
        }

        private void UpdatePlayerLink(GameplayItemLinks player)
        {
            _playerMove = player.Moving;
        }

        private void EnableMove()
        {
            _canMove = true;
        }

        private void DisableMove()
        {
            _canMove = false;
        }
    }
}