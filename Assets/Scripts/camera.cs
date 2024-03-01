using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player;
    private void Update()
    {
        Vector3 v = player.transform.position;
        v.z = -10;
        transform.position = v;
    }
}
