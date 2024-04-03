using UnityEngine;
using UnityEngine.PlayerLoop;

public class Pesticide : MonoBehaviour
{
    public float coldDown;
    public float _coldDown;
    public Aerosol aerosol;
    public GameObject spout;
    public float range;
    private void Update()
    {
        if (_coldDown > 0)
        {
            _coldDown -= Time.deltaTime;
            return;
        }
        Vector2 v = (Vector2)ZhangLang.Instance.transform.position - (Vector2)gameObject.transform.position;
        if (v.magnitude <= range)
            Inject(v.normalized);
    }
    private void Inject(Vector2 Dic)
    {
        Vector3 vector3 = spout.transform.position + (Vector3)Dic * 1.0f;
        Aerosol a = Instantiate(aerosol, vector3, Quaternion.identity);
        a.dic = Dic;
        Debug.Log($"{gameObject.name}喷射啦");
        _coldDown = coldDown;
        GetComponent<AudioSource>().Play();
    }
}
