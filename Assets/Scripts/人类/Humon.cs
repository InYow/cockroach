using UnityEngine;
using UnityEngine.PlayerLoop;

public class Humon : MonoBehaviour
{
    [Header("与受伤相关")]
    public ParticleSystem particle;
    public float health;
    public float invinTime;
    private float _invinTime;
    public float _health;
    [Header("状态持续时间")]
    public Vector2 stateTimeRange;
    private float _stateTime;
    [Header("与闲逛相关")]
    public float walkForce;
    public Vector2 angleRange;//角度变换
    [Header("与窜逃相关")]
    public float escapeSpeed;
    private Vector2 _Dic;//移动方向
    public float escapeTime;//窜逃时长
    private Animator _animator;
    private Rigidbody2D _rb;
    private ZhangLang Player => ZhangLang.Instance;
    enum State
    {
        idle_stop = 0,
        idle_walk,
        escape
    };
    private State _state;
    // Start is called before the first frame update
    void Start()
    {
        _health = health;
        _invinTime = -1f;
        _Dic = Vector2.right;
        _stateTime = -1;
        _state = State.idle_stop;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //如果闲着且状态时间结束
        if (_stateTime < 0 && _animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            RandomIdle();
        }
        //执行状态
        RunState();
        //状态持续时间计时
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            _stateTime -= Time.deltaTime;
        }
        //无敌时间计时
        if (_invinTime > 0)
        {
            _invinTime -= Time.deltaTime;
        }
    }
    public void GetHurt()
    {
        if (_invinTime > 0)
            return;
        //TODO 编写受伤逻辑
        _animator.Play("getHurt");
        _health -= Player.damage;
        Debug.Log($"{gameObject.name}的血量为{_health}");
        particle.Play();
        _invinTime = invinTime;
        if (_health < 0f)
        {
            Die();
        }
        else
        {
            IntoEscape();
        }
        Score.Instance.AddCombo();
        Score.Instance.AddScore(10);
        //杀虫剂
        Pesticide pesticide = GetComponent<Pesticide>();
        if (pesticide != null)
            pesticide._coldDown = Mathf.Clamp(pesticide._coldDown, pesticide.coldDown * 0.5f, pesticide.coldDown);
        //拖鞋
        Thrower thrower = GetComponent<Thrower>();
        if (thrower != null)
            thrower._coldDown = Mathf.Clamp(thrower._coldDown, thrower.coldDown * 0.5f, thrower.coldDown);
        //水枪
        WaterGun waterGun = GetComponent<WaterGun>();
        if (waterGun != null)
            waterGun._coldDown = Mathf.Clamp(waterGun._coldDown, waterGun.coldDown * 0.5f, waterGun.coldDown);
    }
    public void Die()
    {
        //FIN 编写死亡逻辑
        _animator.Play("die");
        GetComponent<Rigidbody2D>().simulated = false;
        Score.Instance.AddScore(30);
        Spawner.Instance.currentNumber--;
        //FIN 消息传递
        //杀虫剂
        Pesticide pesticide = GetComponent<Pesticide>();
        if (pesticide != null)
            pesticide.enabled = false;
        //拖鞋
        Thrower thrower = GetComponent<Thrower>();
        if (thrower != null)
        {
            thrower.enabled = false;
            thrower.icon.SetActive(false);
        }
        //水枪
        WaterGun waterGun = GetComponent<WaterGun>();
        if (waterGun != null)
            waterGun.enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetHurt();
        }
    }
    private void RunState()
    {
        //FIN 原地停留，四处游逛，受伤窜逃，
        if (_state == State.idle_stop)
        {
            return;
        }
        else if (_state == State.idle_walk)
        {
            _rb.AddForce(_Dic * walkForce);
        }
        else if (_state == State.escape)
        {
            _rb.AddForce(_rb.drag * _rb.mass * _rb.velocity.magnitude * _Dic);
        }
    }
    private void IntoEscape()
    {
        _state = State.escape;
        _stateTime = escapeTime;
        _Dic = ((Vector2)transform.position - (Vector2)Player.transform.position).normalized;
        Debug.Log(_Dic);
        _rb.velocity = _Dic * escapeSpeed;
        Debug.Log(_rb.velocity);
    }
    private void RandomIdle()
    {

        int a = Random.Range(0, 2);
        switch (a)
        {
            case 0:
                {
                    Idle_Stop();
                    SetStateTime();
                    break;
                }
            case 1:
                {
                    Idle_Walk();
                    SetStateTime();
                    break;
                }
            default:
                {
                    Idle_Stop();
                    SetStateTime();
                    break;
                }
                //包含了状态的初始化
                void Idle_Stop()
                {
                    _state = State.idle_stop;
                }
                void Idle_Walk()
                {
                    //计算旋转四元数
                    float angle = Random.Range(angleRange.x, angleRange.y);
                    Vector3 v = new(0, 0, angle);
                    Quaternion rotation = Quaternion.Euler(v);
                    //应用旋转
                    _Dic = rotation * _Dic;
                    _state = State.idle_walk;
                }
                void SetStateTime()
                {
                    _stateTime = Random.Range(stateTimeRange.x, stateTimeRange.y);
                }
        }
    }
}
