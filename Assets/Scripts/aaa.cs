using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class aaa : MonoBehaviour
{
    public float a;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        a = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        a -= Time.deltaTime;
        if (a < 0)
            gameObject.SetActive(false);
    }
}
