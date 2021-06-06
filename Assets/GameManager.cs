using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float waitingTime = 1.5f;
    public bool ready = true;
    public bool end = false;
    public GameObject cactus;
    public GameObject bird;
    public AudioClip deathSound;
    public AudioClip goalSound;
    public int score;
    public TextMesh scoreText;
    public TextMesh finishScoreText;
    public TextMesh bestScoreText;
    public GameObject getReadyImg;
    public GameObject readyTapImg;
    public GameObject gameoverImg;
    public GameObject finishWindow;
    public GameObject newImg;

    private Rigidbody rb;
    private AudioSource audioSource;
    void Start()
    {
        rb = bird.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ready == true)
        {
            ready = false;
            InvokeRepeating("MakeCactus", 1f, waitingTime); //1.5초를 기다리고 1초 마다 생성
            rb.useGravity = true;
            iTween.FadeTo(getReadyImg, iTween.Hash("alpha", 0, "Time", 0.5f));
            iTween.FadeTo(readyTapImg, iTween.Hash("alpha", 0, "Time", 0.5f));
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

    }

    public void SoundPlay(AudioClip ac)
    {
        audioSource.clip = ac;
        audioSource.Play();
    }
    public void GetScore()  //점수 획득
    {
       SoundPlay(goalSound);
        score += 1;
        scoreText.text = score.ToString();
       // AudioSource.PlayClipAtPoint(goalSound, transform.position);
    }
  public void GameOver()
    {
        if (end == true) return;
        end = true;
        CancelInvoke("MakeCactus");   //MakeCactus 를 캔슬 하며 선인장이 나오지 않게 된다.
        iTween.ShakePosition(Camera.main.gameObject,
            iTween.Hash('x', 0.2, 'y', 0.2, "time", 0.5f));
        iTween.FadeTo(gameoverImg,
            iTween.Hash("alpha", 255, "delay", 1f, "Timw", 0.5f));
        iTween.MoveTo(finishWindow, 
            iTween.Hash("y", 1, "delay", 1.3f, "Time", 0.5f));

        SoundPlay(deathSound);

        if(score>PlayerPrefs.GetInt("BestScore"))
        {
            newImg.SetActive(true);
            PlayerPrefs.SetInt("BestScore", score);  //점수를 갱신 베스트 점수로

        }else if(score <= PlayerPrefs.GetInt("BestScore"))
        {
            newImg.SetActive(false);
        }
        finishScoreText.text = score.ToString();
        bestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
    }
    void MakeCactus()
    {
        Instantiate(cactus);
    }

 
}
