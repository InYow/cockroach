using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance;
    public int score;
    public int combo;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    //计算倍率
    private float Magnification()
    {
        return combo / 5;
    }
    //增加得分
    public void AddScore(int baseScore)
    {
        int addedScore = (int)Magnification() * baseScore;
        score += addedScore;
    }
}
