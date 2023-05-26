using Characters;
using UnityEngine;
using Placing;

namespace Regulators.ItemNumberRegulator
{
    public class PlayerRegulatorController : BaseController<PlayerRegulatorModel>
    {
        public GameplayItemLinks SpawnPlayer(IPlacingView placingView)
        {
            var setPosition = Vector3.zero;
            return placingView.Place(EGameItem.Player, setPosition);
        }
    }
}