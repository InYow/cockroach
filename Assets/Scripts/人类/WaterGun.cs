using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    public GameObject area;
    [Header("提前预警时间")]
    public float time;
    public float coldDown;
    public float _coldDown;
    public Water water;
    public GameObject gun;
    public float range;
    private Vector2 Dic;
    private void Update()
    {
        Vector2 v = (Vector2)ZhangLang.Instance.transform.position - (Vector2)gameObject.transform.position;
        if (v.magnitude <= range)
        {
            if (_coldDown > 0)
                _coldDown -= Time.deltaTime;
            else
            //预警区域,记录方向,等待一段时间后发射
            {
                area.SetActive(true);
                area.transform.rotation = Quaternion.identity;
                area.transform.Rotate(new(0, 0, Vector2.SignedAngle(Vector2.right, v.normalized)));
                Dic = v.normalized;
                Invoke(nameof(Shot), time);
                _coldDown = coldDown;
            }
        }
    }
    private void Shot()
    {
        area.SetActive(false);
        Vector3 vector3 = gun.transform.position + (Vector3)Dic * 1.0f;
        Water a = Instantiate(water, vector3, Quaternion.identity, transform);
        a.Init(Dic, GetComponent<Humon>());
        GetComponent<AudioSource>().Play();
    }
}
