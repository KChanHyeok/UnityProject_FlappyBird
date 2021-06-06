using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float jumpPower = 5f;
    public GameObject birdImg;
    public Vector3 lookDirection;
    public GameManager gm;

    private Rigidbody rb;
    private AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.end ==false)
        {
            if (Input.GetMouseButtonDown(0) && transform.position.y < 5f)
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(0, jumpPower, 0, ForceMode.VelocityChange);
                audioSource.Play();
            }
            if(gm.ready==false)
            {
                lookDirection.z = rb.velocity.y * 10f + 20f;
            }
        }
        Quaternion R = Quaternion.Euler(lookDirection);
        birdImg.transform.rotation = Quaternion.RotateTowards(
            birdImg.transform.rotation, R, 5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cactus"))
        {
            rb.velocity = new Vector3(0, -3, 0);
            lookDirection = new Vector3(0, 0, -90);
            gm.GameOver();
        }else if(other.CompareTag("Goal")&&gm.end !=true)
        {
            gm.GetScore(); //점수 추가
        }
    }
}
