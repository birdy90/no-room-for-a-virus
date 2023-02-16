using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class BonusButton : MonoBehaviour
    {
        [SerializeField] private Image Sprite;
        [SerializeField] private TextMeshProUGUI Title;
        [SerializeField] private TextMeshProUGUI Description;

        private void OnDestroy()
        {
            Button button = GetComponent<Button>();
            button.onClick.RemoveAllListeners();
        }

        public void SetBonus(UnitStats bonus)
        {
            Sprite.sprite = bonus.StatsSprite;
            Title.text = bonus.Name;

            Dictionary<string, float> values = bonus.GetNonZeroValues();
            StringBuilder sb = new StringBuilder();
            foreach (string key in values.Keys)
            {
                sb.Append($"{StringUtils.SplitCamelCase(key)}: {values[key]}\n");
            }

            Description.text = sb.ToString().Trim('\n');
        }
    }
}