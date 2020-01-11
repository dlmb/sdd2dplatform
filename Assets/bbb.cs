using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bbb : MonoBehaviour
{
    Rigidbody r;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        r.velocity = new Vector3(0, 14.14f, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
