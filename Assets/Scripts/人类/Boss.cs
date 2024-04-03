using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEditor;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("与受伤相关")]
    public ParticleSystem particle;
    public float health;
    public float invinTime;
    private float _invinTime;
    public float _health;
    [Header("状态")]
    public float stateTime;
    public float _stateTime;
    public State state;
    [Header("子弹")]
    public GameObject feidao;
    [Header("冲撞")]
    public float speed;
    [Header("震地")]
    public GameObject collider2D;
    private Animator _animator;
    private Rigidbody2D _rb;
    private ZhangLang Player => ZhangLang.Instance;
    private bool b;
    public enum State
    {
        idle = 0,
        feipu,
        zhendi,
        feidao
    };
    // Start is called before the first frame update
    void Start()
    {
        b = false;
        _health = health;
        _invinTime = -1f;
        _stateTime = stateTime;
        state = State.idle;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }
    public void IntoIdle()
    {
        state = State.idle;
    }
    // Update is called once per frame
    void Update()
    {
        if (b)
            return;
        if (_stateTime < 0f)
        {
            //TODO 切换状态
            Change();
            _stateTime = stateTime;
        }
        else
        {
            _stateTime -= Time.deltaTime;
        }
        if (_invinTime > 0f)
            _invinTime -= Time.deltaTime;
    }
    public void Change()
    {
        state++;
        if ((int)state == 4)
            state = (State)1;
        if (state == State.feipu)
            _animator.Play("feipuxuli");
        else if (state == State.zhendi)
            _animator.Play("zhendixuli");
        else if (state == State.feidao)
            _animator.Play("feidaoxuli");
    }
    public void Shot()
    {
        Instantiate(feidao, transform.position, Quaternion.identity).GetComponent<Slipper>().Init((Vector2)((Player.transform.position - transform.position).normalized));
    }
    public void Crash()
    {
        Vector3 pos = Player.transform.position - transform.position;
        transform.position += pos * speed;
    }
    public void Zhendi()
    {
        collider2D.SetActive(true);
    }
    public void ZhendiFin()
    {
        collider2D.SetActive(false);
    }
    public void Die()
    {
        _animator.Play("die");
        _rb.simulated = false;
        ZhangLang.Instance.shengli.SetActive(true);
        b = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_invinTime > 0)
                return;
            this._health--;
            particle.Play();
            _invinTime = invinTime;
            Score.Instance.AddCombo();
            Score.Instance.AddScore(10);
            if (_health == 0)
                Die();
        }
    }
}
