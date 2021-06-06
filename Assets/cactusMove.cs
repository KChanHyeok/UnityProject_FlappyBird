using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cactusMove : MonoBehaviour
{
    public float cactusSpeed;
    


    void Start()
    {
        transform.position = new Vector3(6f, Random.Range(-1, 1.5f), 0);    
    }


    void Update()
    {
        transform.Translate(Vector3.left * cactusSpeed * Time.deltaTime);
        if(transform.position.x < -6f)
        {
            Destroy(this.gameObject);
        }
    }
}
