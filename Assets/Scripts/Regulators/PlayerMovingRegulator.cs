using Characters;
using Characters.Moving;
using Misc;
using Regulators.ItemNumberRegulator;
using UnityEngine;

namespace Regulators
{
    public class PlayerMovingRegulator : BaseView<PlayerMovingRegulatorModel, PlayerMovingRegulatorController>
    {
        [SerializeField] private PlayerInputReader playerInputReader;
        private IPlayerInputReader _playerInputReader;

        private void Start()
        {
            playerInputReader.TryGetComponent(out _playerInputReader);
            PlayerRegulatorView.Instance.OnPlayerUpdated += Controller.UpdatePlayerLink;
            GameplayTurnRegulator.Instance.OnStopMatchEvent += Controller.DisableMove;
            GameplayTurnRegulator.Instance.OnStartMatchEvent += Controller.EnableMove;
        }

        private void LateUpdate()
        {
            if (!Model.CanMove) return;
            
            if (_playerInputReader.JoystickShift != Vector2.zero)
            {
                var speed = Model.PlayerMoveData.MoveSpeed;
                var direction = Model.PlayerMoveData.Transform.position + new Vector3(_playerInputReader.JoystickShift.x, 0, _playerInputReader.JoystickShift.y);
                Model.PlayerMove.MoveTo(Vector3.MoveTowards(Model.PlayerMoveData.Transform.position, direction, speed*Time.deltaTime)); 
            }
        }
    }
}