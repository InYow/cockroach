using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class Spawner : MonoBehaviour
{
    public GameObject aaa;
    [Header("当前场上的人类数量")]
    public int currentNumber;
    public TextMeshProUGUI text;
    public Image image;
    public List<int> levelList;
    public int level;
    public int exp;
    public int totalExp;
    public float radius;
    public static Spawner Instance;
    [Header("人类们的预制件")]
    public List<GameObject> HumonList;
    private ZhangLang Player => ZhangLang.Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Update()
    {
        text.text = (level + 1).ToString();
        image.fillAmount = (float)exp / levelList[level];
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instant(HumonList[totalExp]);
        }
        //游戏结束

    }
    //TODO 控制怪物的生成
    public void Instant(GameObject humon)
    {
        //计算生成的坐标
        int angle = Random.Range(0, 360);
        Vector2 pos = new(Mathf.Cos(angle), Mathf.Sin(angle));
        pos *= radius;
        Instantiate(humon, (Vector3)pos, Quaternion.identity);
        //  经验值与升级
        exp++;
        totalExp++;
        //增加当前人口数量
        currentNumber++;
        if (exp > levelList[level])
        {
            exp = 0;
            level++;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //TODO 编写胜利条件，boss死亡时调出
        if (totalExp == 16)
        {

        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (exp != levelList[level])
            {
                Instant(HumonList[totalExp]);
                Score.Instance.AddCombo();
                Score.Instance.AddScore(10);
            }
            else if (currentNumber == 0)
            {
                Instant(HumonList[totalExp]);
                Score.Instance.AddCombo();
                Score.Instance.AddScore(10);
            }
            else
            {
                aaa.SetActive(true);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
