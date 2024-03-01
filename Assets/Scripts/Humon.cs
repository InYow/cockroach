using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class Humon : MonoBehaviour
{
    public float health;
    public float invinTime;
    private float _invinTime;
    private float _health;
    private Animator animator;
    private Rigidbody2D _rb;
    private ZhangLang Player => ZhangLang.Instance;
    enum State
    {
        walk,
        idle,
        escape
    };
    private State _state;
    // Start is called before the first frame update
    void Start()
    {
        _health = health;
        _invinTime = -1f;
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_invinTime > 0)
        {
            _invinTime -= Time.deltaTime;
        }
        //状态机控制
        if (_state == State.walk)
        {

        }
        else if (_state == State.idle)
        {

        }
        else if (_state == State.escape)
        {

        }
    }
    public void GetHurt()
    {
        if (_invinTime > 0)
            return;
        //TODO 编写受伤逻辑
        animator.Play("getHurt");
        _health -= Player.damage;
        Debug.Log($"{gameObject.name}的血量为{_health}");
        _invinTime = invinTime;
        if (_health < 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        //TODO 编写死亡逻辑
        animator.Play("die");
        GetComponent<Rigidbody2D>().simulated = false;
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetHurt();
        }
    }
    public void Walk()
    {
        // _rb.AddForce();
    }
    public void Idle()
    {

    }
    public void Escape()
    {

    }
}
