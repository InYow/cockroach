using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zhendi : MonoBehaviour
{
    public float damage;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = boss.transform.position;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ZhangLang.Instance._health -= damage;
            gameObject.SetActive(false);
            boss.GetComponent<Animator>().Play("idle");
        }
    }
}
