using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BonusGameUIIcon : MonoBehaviour
    {
        [SerializeField] private Image Sprite;
        [SerializeField] private TextMeshProUGUI Number;

        public void SetBonus(UnitStats bonus, int number = 1)
        {
            Sprite.sprite = bonus.StatsSprite;
            if (number == 1)
            {
                Number.gameObject.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                Number.text = number.ToString();
            }
        }
    }
}
