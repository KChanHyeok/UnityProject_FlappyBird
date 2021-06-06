using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrolMove : MonoBehaviour
{

    public float scrollspeed;
    float targetOffset;
    Renderer rd;
    void Start()
    {
        rd = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        targetOffset += Time.deltaTime * scrollspeed;
        rd.material.mainTextureOffset = new Vector2(targetOffset, 0);

    }
}
