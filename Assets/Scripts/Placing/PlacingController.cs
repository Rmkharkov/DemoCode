using System.Collections.Generic;
using System.Linq;
using Characters;
using Characters.Death;
using Characters.Fight;
using Characters.Moving;
using UnityEngine;

namespace Placing
{
    public class PlacingController : BaseController<PlacingModule>
    {
        private readonly List<GameplayItemLinks> _pool = new List<GameplayItemLinks>();

        public GameplayItemLinks CreateGameplayItem(EGameItem itemType, Transform parent, Vector3 position)
        {
            var item = ItemFromPool(itemType) ?? InstantiateItem(itemType, parent);
            item.ItemTransform.parent = parent;
            item.ItemTransform.localPosition = new Vector3(position.x, item.ItemTransform.position.y, position.z);
            item.ItemTransform.gameObject.SetActive(true);
            return item;
        }

        public void RemoveItem(GameplayItemLinks item, Transform poolParent)
        {
            item.ItemTransform.gameObject.SetActive(false);
            item.ItemTransform.parent = poolParent;
            _pool.Add(item);
        }

        public void RemoveItem(Transform itemTransform, Transform poolParent)
        {
            var item = _pool.FirstOrDefault(c => c.ItemTransform == itemTransform);
            if (item != null)
            {
                item.ItemTransform.gameObject.SetActive(false);
                item.ItemTransform.parent = poolParent;
                _pool.Add(item);
            }
        }

        private GameplayItemLinks InstantiateItem(EGameItem itemType, Transform parent)
        {
            var itemPrefab = GameplayPrefabs.Instance.GetItemPrefab(itemType);
            var item = Object.Instantiate(itemPrefab, parent);
            item.transform.position = new Vector3(0f, itemPrefab.transform.position.y, 0f);
            var toReturn = new GameplayItemLinks()
            {
                ItemTransform = item.transform,
                Moving = item.GetComponent<IMovable>(),
                GetHit = item.GetComponentInChildren<IDamageable>(),
                MakeHit = item.GetComponentInChildren<IMakeHit>(),
                DeathFlow = item.GetComponent<IDeathFlow>()
            };
            return toReturn;
        }

        private GameplayItemLinks ItemFromPool(EGameItem itemType)
        {
            var toReturn = _pool.FirstOrDefault(c => c.ItemType == itemType);
            if (toReturn != null)
            {
                _pool.Remove(toReturn);
            }

            return toReturn;
        }
    }
}