using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform senceTransform;
    void Start()
    {
        senceTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 s = senceTransform.position;
        s.z += 0.03f;
        senceTransform.position = s;
    }
}
