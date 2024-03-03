using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player;
    public GameObject LeftDown;
    public GameObject RightUp;
    private void Update()
    {
        Vector3 v = player.transform.position;
        v.z = -10;
        v.x = Mathf.Clamp(v.x, LeftDown.transform.position.x, RightUp.transform.position.x);
        v.y = Mathf.Clamp(v.y, LeftDown.transform.position.y, RightUp.transform.position.y);
        transform.position = v;
    }
}
