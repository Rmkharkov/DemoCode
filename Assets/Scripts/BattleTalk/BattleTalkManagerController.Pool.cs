using System.Collections.Generic;
using System.Linq;
using Animals;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BattleTalk
{
    public partial class BattleTalkManagerController
    {
        private List<Transform> _liveTalks = new List<Transform>();
        private List<Transform> _poolTalks = new List<Transform>();

        private async UniTask<Transform> GetTalk()
        {
            var toReturn = _poolTalks.LastOrDefault();
            if (toReturn == null)
            {
                toReturn = await GetNewTalk();
            }
            else
            {
                RevealTalk(toReturn);
            }
            return toReturn;
        }

        private void HideTalk(Transform talkTransform)
        {
            if (talkTransform == null) return;
            _liveTalks.Remove(talkTransform);
            _poolTalks.Add(talkTransform);
            talkTransform.SetParent(Model.BattleTalksPoolParent);
        }

        private void RevealTalk(Transform talkTransform)
        {
            _liveTalks.Add(talkTransform);
            _poolTalks.Remove(talkTransform);
            talkTransform.SetParent(Model.BattleTalksLiveParent);
        }
        
        private async UniTask<Transform> GetNewTalk()
        {
            var prefab = await Addressables.LoadAssetAsync<GameObject>("Talk_Tasty");
            var toReturn = _container.InstantiatePrefab(prefab);
            var cachedObject = toReturn.transform;
            _liveTalks.Add(cachedObject);
            return cachedObject;
        }
    }
}