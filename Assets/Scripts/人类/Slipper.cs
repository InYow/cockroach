using System;
using System.Collections;
using UnityEngine;

public class Slipper : MonoBehaviour
{
    private float _time;
    public float speed;
    private Vector2 dic;
    public float damage;
    private Humon master;
    void Start()
    {
        _time = 10f;
    }
    // Update is called once per frame
    void Update()
    {
        if (_time < 0f)
            Destroy(this.gameObject);
        else
            _time -= Time.deltaTime;
        transform.position += (Vector3)dic * speed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (master != null)
            if (other.gameObject == master.gameObject)
                return;
        //如果碰到了蟑螂
        if (other.gameObject.CompareTag("Player"))
        {
            ZhangLang.Instance._health -= damage;
            Destroy(this.gameObject, 0.1f);
        }
    }
    //初始化
    public void Init(Vector2 dic, Humon master)
    {
        Vector3 v = new(0, 0, Vector2.SignedAngle(Vector2.right, dic));
        transform.Rotate(v);
        this.dic = dic;
        this.master = master;
    }
    public void Init(Vector2 dic)
    {
        Vector3 v = new(0, 0, Vector2.SignedAngle(Vector2.right, dic));
        transform.Rotate(v);
        this.dic = dic;
    }
}
