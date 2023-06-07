using Characters;
using Characters.Moving;
using Regulators.ItemNumberRegulator;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Regulators
{
    public interface IEnemiesMoveRegulator
    {
        void UpdateEnemiesMoves(IMovable[] enemiesMoves);
    }

    public class EnemiesMovingRegulator : BaseView<EnemiesMovingRegulatorModel, EnemiesMovingRegulatorController>, IEnemiesMoveRegulator
    {
        private void Start()
        {
            PlayerRegulatorView.Instance.OnPlayerUpdated += Controller.UpdatePlayerLink;
            GameplayTurnRegulator.Instance.OnStopMatchEvent += Controller.DisableMove;
            GameplayTurnRegulator.Instance.OnStartMatchEvent += Controller.EnableMove;
        }

        private void Update()
        {
            if (!Model.CanMove) return;

            Controller.MoveEnemies();
        }

        public void UpdateEnemiesMoves(IMovable[] enemiesMoves)
        {
            Model.EnemiesMoves = enemiesMoves;
        }
    }
}