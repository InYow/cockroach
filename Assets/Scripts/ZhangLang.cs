using UnityEngine;
using UnityEngine.UI;

public class ZhangLang : MonoBehaviour
{
    [Header("血量")]
    public float health;
    [HideInInspector]
    public float _health;
    public Image healthBar;
    [Header("伤害")]
    public float damage;
    [Header("与离散长按相关")]
    public float coldDown;
    private float _coldDown;
    public Dot dot_1;
    public float force_1;
    public Dot dot_2;
    public float force_2;
    public Dot dot_3;
    public float force_3;
    public float holdLV_2;
    public float holdLV_3;
    [Header("与连续长按相关")]
    public Vector2 rangeTime;
    public float holdTime_Fly;
    public float timeFactor;
    [Header("旋转的速度")]
    public float rotateSpeed;
    [Header("冲刺的力度")]
    public float dashForce;
    [Header("飞行的力度")]
    public float flyForce;
    [Header("小于该速度则冲刺结束")]
    public float stopSpeed;
    [Header("调试用")]
    public float _holdTime;
    private Rigidbody2D _rb;
    private Animator _animator;
    private bool _freezeRotation;
    public static ZhangLang Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        Init();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void Init()
    {
        _health = health;
        _holdTime = 0;
        _freezeRotation = false;
        _coldDown = coldDown;
    }
    private void Update()
    {
        //传递血量百分比
        healthBar.fillAmount = _health / health;
        _freezeRotation = false;
        if (_health <= 0f)
        {
            Die();
            return;
        }
        //冲刺的冷却
        if (_coldDown > 0f)
        {
            _coldDown -= Time.deltaTime;
        }
        //冲刺
        #region 按键
        if (_coldDown <= 0f && !CheckStop())
        {
            _animator.Play("idle");
            //TODO 手柄输入。
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _holdTime = 0;
                _freezeRotation = true;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                _holdTime += Time.deltaTime;
                dot_1.Fill();
                if (_holdTime >= holdLV_2)
                    dot_2.Fill();
                if (_holdTime >= holdLV_3)
                    dot_3.Fill();
                _freezeRotation = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Move_Discrete(_holdTime);
                dot_1.Empty(); dot_2.Empty(); dot_3.Empty();
                _holdTime = 0;
            }
        }
        #endregion
        if (CheckStop())
            _freezeRotation = true;
        //旋转
        if (!_freezeRotation)
            Rotate();
    }
    public void Die()
    {
        _animator.Play("die");
    }
    /// <summary>
    /// 停下返回false
    /// </summary>
    /// <returns></returns>
    public bool CheckStop()
    {
        if (_rb.velocity.magnitude <= stopSpeed)
            return false;
        else
            return true;
    }
    public void Rotate()
    {
        Vector3 angle = new(0, 0, rotateSpeed * Time.deltaTime);
        transform.Rotate(angle);
    }
    public void Move(float holdtime)
    {
        holdtime = Mathf.Clamp(holdtime, rangeTime.x, rangeTime.y);
        if (holdtime < holdTime_Fly)
            Dash(holdtime);
        else
            Fly(holdtime);
    }
    public void Move_Discrete(float holdtime)
    {
        if (holdtime < holdLV_2)
        {
            Dash(force_1);
            return;
        }
        if (holdtime < holdLV_3)
        {
            Dash(force_2);
            return;
        }
        Dash(force_3);
        return;
    }
    public void Dash(float holdtime)
    {
        //计算方向
        float angle = transform.rotation.eulerAngles.z;
        Vector2 dic = new(-Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        dic = dic.normalized;
        //计算时间
        float time = holdtime * timeFactor;
        //计算力
        Vector2 force = dic * dashForce * time;
        _rb.AddForce(force, ForceMode2D.Impulse);
        //播放动画
        _animator.Play("dash");
        //进入冷却
        _coldDown = coldDown;
    }
    public void Fly(float holdtime)
    {
        //计算方向
        float angle = transform.rotation.eulerAngles.z;
        Vector2 dic = new(-Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        dic = dic.normalized;
        //计算时间
        float time = holdtime * timeFactor;
        //计算力
        Vector2 force = dic * flyForce * time;
        _rb.AddForce(force, ForceMode2D.Impulse);
    }
}
