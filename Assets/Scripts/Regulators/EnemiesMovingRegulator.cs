using Characters;
using Regulators.ItemNumberRegulator;

namespace Regulators
{
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

        public void UpdateEnemiesMoves(GameplayItemLinks[] enemies)
        {
            Model.Enemies = enemies;
        }
    }
}