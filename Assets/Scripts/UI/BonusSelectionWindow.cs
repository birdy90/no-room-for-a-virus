using System;
using System.Collections.Generic;
using System.Linq;
using Units;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class BonusSelectionWindow : MonoBehaviour
    {
        [SerializeField] private int NumberOfItemsToChoose = 3;
        [SerializeField] private List<UnitStats> AvailableBonuses;
        [SerializeField] private GameObject BonusList;
        [SerializeField] private BonusButton BonusButtonPrefab;

        private void OnDestroy()
        {
            foreach (Transform childButton in BonusList.transform)
            {
                childButton.GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }

        public void FillBonusButtons()
        {
            List<UnitStats> randomBonuses = GetRandomBonusList();
            
            foreach (Transform childButton in BonusList.transform)
            {
                Destroy(childButton.gameObject);
            }

            foreach (UnitStats bonus in randomBonuses)
            {
                BonusButton bonusButton = Instantiate(BonusButtonPrefab, BonusList.transform);
                bonusButton.SetBonus(bonus);
                Button button = bonusButton.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    OnBonusSelected?.Invoke(bonus);
                    GameFlow.Unpause();
                    gameObject.SetActive(false);
                });
            }
        }
        
        private List<UnitStats> GetRandomBonusList()
        {
            List<int> randomIndexes = new List<int>();
            
            while (randomIndexes.Count < NumberOfItemsToChoose)
            {
                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, AvailableBonuses.Count);
                } while (randomIndexes.Contains(randomIndex));
                
                randomIndexes.Add(randomIndex);
            }

            return randomIndexes.Select(index => AvailableBonuses[index]).ToList();
        }

        public Action<UnitStats> OnBonusSelected;
    }
}