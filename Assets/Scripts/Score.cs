using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance;
    public int score;
    public int combo;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Update()
    {
        comboText.text = combo.ToString(); scoreText.text = score.ToString();
    }
    public void AddCombo()
    {
        combo++;
        comboText.text = combo.ToString();
    }
    //计算倍率
    private float Magnification()
    {
        return combo / 3 + 1;
    }
    //增加得分
    public void AddScore(int baseScore)
    {
        int addedScore = (int)Magnification() * baseScore;
        score += addedScore;
        scoreText.text = score.ToString();
    }
}
