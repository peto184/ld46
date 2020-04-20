using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoronaJitter : MonoBehaviour
{

    private RectTransform rt;

    // Range over which height varies.
    float heightScale = 0.02f;

    // Distance covered per second along X axis of Perlin plane.
    float xScale = 1.0f;
    
    float f;
    void Start() {
        f = Random.Range(0f, 10f);
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 pos = rt.position;
        pos.x += heightScale * (Mathf.PerlinNoise(f + 2 * Time.time * xScale, 0.0f) - 0.499f);
        pos.y += heightScale * (Mathf.PerlinNoise(f + Time.time * xScale, 0.0f) - 0.499f);

        rt.position = pos;
    }
}
