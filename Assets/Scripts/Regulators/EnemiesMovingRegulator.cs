using System;
using System.Collections.Generic;
using Characters;
using Characters.Moving;
using Regulators.ItemNumberRegulator;
using UnityEngine;

namespace Regulators
{
    public interface IEnemiesMoveRegulator
    {
        void UpdateEnemiesMoves(IMovable[] enemiesMoves);
    }

    public class EnemiesMovingRegulator : MonoBehaviour, IEnemiesMoveRegulator
    {
        private IMovable[] _enemiesMoves;
        private IMovable _player;
        private bool _canMove;

        private void Start()
        {
            PlayerRegulatorView.Instance.PlayerUpdated.AddListener(UpdatePlayerLink);
            GameplayTurnRegulator.Instance.StopMatchEvent.AddListener(DisableMove);
            GameplayTurnRegulator.Instance.StartMatchEvent.AddListener(EnableMove);
        }

        private void Update()
        {
            if (!_canMove) return;

            MoveEnemies();
        }

        private void MoveEnemies()
        {
            if (_enemiesMoves == null || _enemiesMoves.Length == 0)
                return;

            foreach (var enemyMove in _enemiesMoves)
            {
                if (_player != null && enemyMove != null)
                {
                    var speed = enemyMove.MoveSpeed;
                    var direction = _player.Transform.position;
                    enemyMove.MoveTo(Vector3.MoveTowards(enemyMove.Transform.position, direction,
                        speed * Time.deltaTime));
                }
            }
        }

        public void UpdateEnemiesMoves(IMovable[] enemiesMoves)
        {
            _enemiesMoves = enemiesMoves;
        }

        private void UpdatePlayerLink(GameplayItemLinks player)
        {
            _player = player.Moving;
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