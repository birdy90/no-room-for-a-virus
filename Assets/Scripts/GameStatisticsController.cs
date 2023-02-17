using System.Text;
using TMPro;
using Units;
using UnityEngine;
using Utils;

public class GameStatisticsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimerText;

    private int _secondsPassed = 0;
    private int _enemiesKilled = 0;
    private int _eliteEnemiesKilled = 0;

    public void Update()
    {
        int nextSecondsPassed = Mathf.FloorToInt(Time.timeSinceLevelLoad);
        if (nextSecondsPassed != _secondsPassed)
        {
            _secondsPassed = nextSecondsPassed;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (TimerText)
        {
            TimerText.text = StringUtils.TimeToString(_secondsPassed);
        }
    }

    public void EnemyKilled(bool isElite)
    {
        _enemiesKilled += 1;
        _eliteEnemiesKilled += isElite ? 1 : 0;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"Time alive: {StringUtils.TimeToString(_secondsPassed)}\n");
        sb.Append($"Enemies killed: {_enemiesKilled}\n");
        sb.Append($"Elite enemies: {_eliteEnemiesKilled}\n");
        sb.Append($"Level reached: {GetComponent<ExperienceController>().Level}\n");
        return sb.ToString();
    }
}
