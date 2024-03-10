using Animals;
using TMPro;
using UnityEngine;

namespace UI
{
    [System.Serializable]
    public class DeathsCounterModel : BaseModel
    {
        [SerializeField] private TextMeshProUGUI counterText;
        public TextMeshProUGUI CounterText => counterText;
        [SerializeField] private EAnimalSide animalSide;
        public EAnimalSide AnimalSide => animalSide;
    }
}