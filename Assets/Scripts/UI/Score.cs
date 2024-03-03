using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score Instance;
    public int score;
    public int combo;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public Image comboImage;
    public float comboTime;
    private float _comboTime;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        _comboTime = comboTime;
        comboImage.gameObject.SetActive(false);
    }
    private void Update()
    {
        comboText.text = combo.ToString(); scoreText.text = score.ToString();
        if (_comboTime >= 0)
        {
            _comboTime -= Time.deltaTime;
            comboImage.fillAmount = _comboTime / comboTime;
            if (_comboTime < 0)
                FailCombo();
        }

    }
    public void FailCombo()
    {
        SetCombo(-combo);
        comboImage.gameObject.SetActive(false);
    }
    public void SetCombo(int value)
    {
        combo += value;
        comboText.text = combo.ToString();
        _comboTime = comboTime;
        comboImage.gameObject.SetActive(true);
    }
    public void AddCombo()
    {
        SetCombo(1);
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
