﻿using Characters;

namespace Regulators
{
    public class PlayerMovingRegulatorController : BaseController<PlayerMovingRegulatorModel>
    {
        public void UpdatePlayerLink(GameplayItemLinks player)
        {
            Model.PlayerMove = player.Moving;
            Model.PlayerMoveData = player.MovingData;
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