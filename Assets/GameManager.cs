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
            InvokeRepeating("MakeCactus", 1f, waitingTime); //1.5�ʸ� ��ٸ��� 1�� ���� ����
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
    public void GetScore()  //���� ȹ��
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
        CancelInvoke("MakeCactus");   //MakeCactus �� ĵ�� �ϸ� �������� ������ �ʰ� �ȴ�.
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
            PlayerPrefs.SetInt("BestScore", score);  //������ ���� ����Ʈ ������

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
