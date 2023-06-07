using Characters;
using Characters.Moving;
using UnityEngine;

namespace Regulators
{
    public class EnemiesMovingRegulatorController : BaseController<EnemiesMovingRegulatorModel>
    {
        public void MoveEnemies()
        {
            if (Model.EnemiesMoves == null || Model.EnemiesMoves.Length == 0)
                return;

            foreach (var enemyMove in Model.EnemiesMoves)
            {
                if (Model.Player != null && enemyMove != null)
                {
                    var speed = enemyMove.MoveSpeed;
                    var direction = Model.Player.Transform.position;
                    enemyMove.MoveTo(Vector3.MoveTowards(enemyMove.Transform.position, direction,
                        speed * Time.deltaTime));
                }
            }
        }

        public void UpdatePlayerLink(GameplayItemLinks player)
        {
            Model.Player = player.Moving;
        }

        public void EnableMove()
        {
            Model.CanMove = true;
        }

        public void DisableMove()
        {
            Model.CanMove = false;
        }
    }
}