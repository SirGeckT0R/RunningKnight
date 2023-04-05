using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private int maxScore;
    [SerializeField] private TextMeshProUGUI bestScore;
    [SerializeField] private DataManager dataManager;
    private TextMeshProUGUI text;
    public int score;
    private int previousPosition;

    private void Awake()
    {
        text= GetComponent<TextMeshProUGUI>();
        dataManager.Load();
        bestScore.text = dataManager.GetGameData().bestScore.ToString();
    }

    private void Update()
    {
        int distanceTraveled = Mathf.FloorToInt(player.position.x) - previousPosition;
        previousPosition = Mathf.FloorToInt(player.position.x);
        score += distanceTraveled;
        if(score >=maxScore)
        {
            score = maxScore;
        }
        text.text = score.ToString();
    }

    public void SaveBestScore()
    {
        dataManager.Load();
        if (dataManager.GetGameData().bestScore < score)
        {
            dataManager.SetBestScore(score);
            dataManager.Save();
        }
    }

    public void addScore(int scorePoints)
    {
        score += scorePoints > 0 ? scorePoints : 0;
    }
}
