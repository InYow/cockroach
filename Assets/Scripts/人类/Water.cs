using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public Vector2 dic;
    public float damage;
    public float speed;
    public float fade;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        //移动
        Vector2 pos = transform.position;
        pos += speed * Time.deltaTime * dic;
        transform.position = (Vector3)pos;
        //消逝
        Color color = spriteRenderer.color;
        color.a -= fade * Time.deltaTime;
        spriteRenderer.color = color;
        if (spriteRenderer.color.a < 0.1)
        {
            Destroy(gameObject);
        }
    }
    public void Init(Vector2 dic, Humon master)
    {
        Vector3 v = new(0, 0, Vector2.SignedAngle(Vector2.right, dic));
        transform.Rotate(v);
        this.dic = dic;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (spriteRenderer.color.a > 0.1)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<ZhangLang>()._health -= damage * Time.deltaTime;
            }
            // if (other.gameObject.CompareTag("Humon"))
            // {
            //     //FIXME 水枪对人类也有伤害，且没有反馈
            //     other.GetComponent<Humon>()._health -= damage * Time.deltaTime;
            // }
        }
    }
}
