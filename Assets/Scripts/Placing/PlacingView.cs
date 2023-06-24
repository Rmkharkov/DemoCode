using Characters;
using UnityEngine;

namespace Placing
{
    public class PlacingView : BaseView<PlacingModule, PlacingController>, IPlacingView
    {
        [SerializeField] private Transform gameplayParent;
        [SerializeField] private Transform poolParent;
        public GameplayItemLinks Place(EGameItem gameItem, Vector3 position)
        {
            return Controller.CreateGameplayItem(gameItem, gameplayParent, position);
        }

        public void Remove(GameplayItemLinks item)
        {
            Controller.RemoveItem(item, poolParent);
        }

        public void Remove(Transform itemTransform)
        {
            Controller.RemoveItem(itemTransform, poolParent);
        }
    }
}