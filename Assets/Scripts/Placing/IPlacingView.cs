using Characters;
using UnityEngine;

namespace Placing
{
    public interface IPlacingView
    {
        GameplayItemLinks Place(EGameItem gameItem, Vector3 position);
        void Remove(GameplayItemLinks item);
        void Remove(Transform itemTransform);
    }
}