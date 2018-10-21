using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_1 : MonoBehaviour {

    public bool down;
    public float speed;
    private Vector3 startPosition;
    private float d;

    private void Start()
    {
        d = down ? -1.0f : 1.0f;
        startPosition = transform.position;
    }

    private void Update () {
        transform.position = startPosition + new Vector3(0.0f, d*2.5f * Mathf.Sin(Time.time*speed/10.0f), 0.0f);
    }
}
