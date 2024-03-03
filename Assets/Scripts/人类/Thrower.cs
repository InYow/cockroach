using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public GameObject icon;
    [Header("提前预警时间")]
    public float time;
    public float coldDown;
    public float _coldDown;
    public Slipper slipper;
    public GameObject hand;
    public float range;
    private void Update()
    {
        Vector2 v = (Vector2)ZhangLang.Instance.transform.position - (Vector2)gameObject.transform.position;
        if (v.magnitude <= range)
        {
            if (_coldDown > 0)
                _coldDown -= Time.deltaTime;
            else
                Throw(v.normalized);
        }
        //预警显示
        if (_coldDown < time)
            icon.SetActive(true);
        else
            icon.SetActive(false);
    }
    private void Throw(Vector2 Dic)
    {
        Vector3 vector3 = hand.transform.position + (Vector3)Dic * 1.0f;
        Slipper a = Instantiate(slipper, vector3, Quaternion.identity);
        a.Init(Dic, GetComponent<Humon>());
        //        Debug.Log($"{gameObject.name}喷射啦");
        _coldDown = coldDown;
    }
}
