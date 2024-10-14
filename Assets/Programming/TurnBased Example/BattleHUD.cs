using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace TurnBased
{
    public class BattleHUD : MonoBehaviour
    {
        public Text nameText;
        public Text levelText;
        public Image healthBar;
        public Image icon;
        public void SetHUD(Unit unit)
        {
            nameText.text = unit.unitName;
            levelText.text = $"level: {unit.unitLevel}";
            icon.sprite = unit.unitIcon;
            SetHealth(unit);
        }
        public void SetHealth(Unit unit)
        {
            healthBar.fillAmount = Mathf.Clamp01(unit.currentHealth/unit.maxHealth);
        }
    }
}
