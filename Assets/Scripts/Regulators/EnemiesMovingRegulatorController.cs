using Characters;
using Characters.Moving;
using UnityEngine;

namespace Regulators
{
    public class EnemiesMovingRegulatorController : BaseController<EnemiesMovingRegulatorModel>
    {
        public void MoveEnemies()
        {
            if (Model.Enemies == null || Model.Enemies.Length == 0)
                return;

            foreach (var enemy in Model.Enemies)
            {
                if (Model.Player != null && enemy != null && enemy.MovingData != null)
                {
                    var speed = enemy.MovingData.MoveSpeed;
                    var direction = Model.Player.Transform.position;
                    enemy.Moving.MoveTo(Vector3.MoveTowards(enemy.MovingData.Transform.position, direction,
                        speed * Time.deltaTime));
                }
            }
        }

        public void UpdatePlayerLink(GameplayItemLinks player)
        {
            Model.Player = player.MovingData;
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