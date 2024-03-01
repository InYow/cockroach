using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Spawner : MonoBehaviour
{
    private static Spawner Instance;
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instant(HumonList[0]);
        }
    }
    //TODO 控制怪物的生成
    public void Instant(GameObject humon)
    {
        //计算生成的坐标
        //TODO:优化坐标计算
        Vector2 pos;
        pos.x = Random.Range(-20.0f, 20.0f) * RandomSign();
        pos.y = Random.Range(-14.0f, 14.0f) * RandomSign();
        Instantiate(humon, (Vector3)pos, Quaternion.identity);

        static int RandomSign()
        {
            int a = Random.Range(0, 2);
            if (a == 0)
                return -1;
            else
                return 1;
        }
    }
}
