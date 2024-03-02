using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUI : MonoBehaviour
{
    public GameObject player;
    private void Update()
    {
        Vector3 v = player.transform.position;
        v.z = 0;
        transform.position = v;

    }
}
