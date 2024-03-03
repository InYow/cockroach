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
        if (other.gameObject == master.gameObject)
            return;
        //如果碰到了蟑螂
        if (other.gameObject.CompareTag("Player"))
        {
            ZhangLang.Instance._health -= damage;
            Destroy(this.gameObject, 0.1f);
        }
        //与人碰撞
        // if (other.gameObject.CompareTag("Humon"))
        // {
        //     Debug.Log(gameObject.name);
        //     Debug.Log(other.gameObject.name);
        //     Humon humon = other.gameObject.GetComponent<Humon>();
        //     humon?.GetHurt();
        //     Destroy(this.gameObject, 0f);
        // }
        //与墙碰撞
        // if (other.gameObject.CompareTag("Wall"))
        // {
        //     Destroy(this.gameObject, 0f);
        // }
    }
    //初始化
    public void Init(Vector2 dic, Humon master)
    {
        Vector3 v = new(0, 0, Vector2.SignedAngle(Vector2.right, dic));
        transform.Rotate(v);
        this.dic = dic;
        this.master = master;
    }
}
