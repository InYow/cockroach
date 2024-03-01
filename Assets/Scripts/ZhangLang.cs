using UnityEngine;

public class ZhangLang : MonoBehaviour
{
    [Header("伤害")]
    public float damage;
    [Header("与长按相关")]
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
        _holdTime = 0;
        _freezeRotation = false;
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _freezeRotation = false;
        //移动
        //  //FIN:按下开始蓄力，计时；松开根据时长给予冲量
        //  //FIN:按下则认为在蓄力，冲刺则认为在冲刺
        //  //TODO:分阶段蓄力，分为三阶段，各一秒共三秒
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _holdTime = 0;
            _freezeRotation = true;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _holdTime += Time.deltaTime;
            _freezeRotation = true;
        }
        //  //松开按钮
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Move(_holdTime);
            _holdTime = 0;
        }
        if (CheckStop())
            _freezeRotation = true;
        //旋转
        if (!_freezeRotation)
            Rotate();
    }
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
