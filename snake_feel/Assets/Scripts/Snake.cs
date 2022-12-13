using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 4;
    private int directionUDInt;
    private int directionDGInt = 1;
    public float speed = 7;
    public float speedboost = 12;
    private float _intervalle = 1f;
    private float currentTime = 0f;
    private float Nbsegment = 4f;
    public GameObject _player;
    public ParticleSystem fogVFX;
    private bool isDead;
    private bool canDie;
    public Transform segmentsObject;
    private bool inputGiven;
    public List<Vector2> _basicDirection;
    public List<Vector2> _randomDirection;
    private bool isRandom;
    public int foodmange;
    public int Score = 0;
    private bool isboost;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    public TextMeshProUGUI textscore;
    public TextMeshProUGUI textHighScore;
    public TextMeshProUGUI texteaten;
    private int bestscore = 0;
    public AudioSource explosion1;
    public AudioSource spawn;
    public AudioSource Turn;
    public AudioSource Accelaeration;
    public AudioSource clickButton;
    private bool isShakable = true;
    
    


    private void Awake()
    {
        ResetState();
    }

    private void Start()
    {
        
        directionDGInt = 1;
        Time.timeScale = 1;

    }
    
    
        private void Update()
        {
            textHighScore.text = "Best score: " + bestscore;
            textscore.text = "Score: " + Score;
            texteaten.text = "Eaten: " + foodmange;
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("mainmenu", LoadSceneMode.Single);
            }
            
            if (inputGiven == false)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (directionUDInt != -1)
                    {
                        if (isShakable)
                        {
                            Shake(1f, 0.1f);
                        }
                        directionUDInt = 1;
                        directionDGInt = 0;
                        inputGiven = true;
                        if (isRandom)
                        {
                            _direction = _randomDirection[0];
                            Turn.Play();
                        }
                        else
                        {
                            _direction = _basicDirection[0];
                            Turn.Play();
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (directionUDInt != 1)
                    {
                        if (isShakable)
                        {
                            Shake(1f, 0.1f);
                        }
                        directionUDInt = -1;
                        directionDGInt = 0;
                        inputGiven = true;
                        if (isRandom)
                        {
                            _direction = _randomDirection[1];
                            Turn.Play();
                        }
                        else
                        {
                            _direction = _basicDirection[1];
                            Turn.Play();
                        }
                        
                    }
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (directionDGInt != 1)
                    {
                        if (isShakable)
                        {
                            Shake(1f, 0.1f);
                        }
                        directionDGInt = -1;
                        directionUDInt = 0;
                        inputGiven = true;
                        if (isRandom)
                        {
                            _direction = _randomDirection[2];
                            Turn.Play();
                        }
                        else
                        {
                            _direction = _basicDirection[2];
                            Turn.Play();
                        }
                        
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (directionDGInt != -1)
                    {
                        if (isShakable)
                        {
                            Shake(1f, 0.1f);
                        }
                        directionDGInt = 1;
                        directionUDInt = 0;
                        inputGiven = true;
                        if (isRandom)
                        {
                            _direction = _randomDirection[3];
                            Turn.Play();
                        }
                        else
                        {
                            _direction = _basicDirection[3];
                            Turn.Play();
                        }
                        
                    }
                }
            }

            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                if (shakeTimer <= 0f)
                {
                    //timer over!
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera
                        .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                }
            }
        }

    public void Shake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        shakeTimer = time;
    }

    


    private void FixedUpdate()
    {
        
        if (isboost)
        {
            if (isDead)
            {
                return;
            }
            if (_intervalle < currentTime * speedboost)
            {
                inputGiven = false;
               for (int i = _segments.Count - 1; i > 0; i--)
               { 
                   _segments[i].position = _segments[i - 1].position ;
               }
               
               Score = Score + 1;
               this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + _direction.x,
                Mathf.Round(this.transform.position.y) + _direction.y,
                0.0f
                
                ); 
               currentTime = 0;
            }

            currentTime += Time.fixedDeltaTime;
        }
        else
        {
            if (isDead)
            {
                return;
            }
            if (_intervalle < currentTime * speed)
            {
                inputGiven = false;
                for (int i = _segments.Count - 1; i > 0; i--)
                { 
                    _segments[i].position = _segments[i - 1].position ;
                }
               
                Score = Score + 1;
                this.transform.position = new Vector3(
                    Mathf.Round(this.transform.position.x) + _direction.x,
                    Mathf.Round(this.transform.position.y) + _direction.y,
                    0.0f
                ); 
                currentTime = 0;
            }

            currentTime += Time.fixedDeltaTime;
        }
    }
    private void Grow()
    {
        speed = speed + 1;
        Score = Score + 100;
        foodmange = foodmange + 1;
        Accelaeration.Play();
        Shuffle();
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
        Nbsegment = Nbsegment + 1;
        StartCoroutine(RandomBlink());
        fogVFX.transform.SetParent(segment, false);
        StartCoroutine(SpeedBoost());
    }

    IEnumerator RandomBlink()
    {
        for (int i = 0; i < 5; i++)
        {
            foreach (Transform segment in _segments)
                    {
                        segment.GetComponent<SpriteRenderer>().color = RandomColor();
                    }
                    yield return new WaitForSeconds(0.1f);
        }
        
        foreach (Transform segment in _segments)
        {
            segment.GetComponent<SpriteRenderer>().color = Color.white;
        }
        
    }

    void Shuffle()
    {
        for (int i = 0; i < _randomDirection.Count; i++) {
            Vector2 temp = _randomDirection[i];
            int randomIndex = Random.Range(i, _randomDirection.Count);
            _randomDirection[i] = _randomDirection[randomIndex];
            _randomDirection[randomIndex] = temp;
        }
    }

        IEnumerator SpeedBoost()
        {
            isShakable = false;
        if (foodmange >= 10)
        {
            isRandom = true;
        }
        isboost = true;
        speedboost = speedboost + 2;
        fogVFX.Play();
        Shake(5f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        isShakable = true;
        isboost = false;
        isRandom = false;
        fogVFX.Stop();

        }
    
    private Color RandomColor()
    {
        int rand = Random.Range(1, 4);
        if (rand == 1)
        {
            return Color.blue;
        }else if (rand == 2)
        {
            return Color.red;
        }else
        {
            return Color.green;
        }
    }

    private void ResetState()
    {
        StartCoroutine(CanDie());

        if (segmentsObject.childCount != 0)
        {
            for (int i = 1; i < _segments.Count; i++)
            {
                Destroy(_segments[i].gameObject);
            }
        }
        
        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
            _segments[i].parent = segmentsObject;
        }
        speed = 7;
        speedboost = 12;
        Nbsegment = 4;
        foreach (Transform segment in _segments)
        {
            segment.GetComponent<SpriteRenderer>().color = Color.white;
                
        }
        fogVFX.Stop();
        fogVFX.transform.SetParent(this.transform, false);


        this.transform.position = Vector3.zero;
        isDead = false;
        
        spawn.Play(0);
    }

    IEnumerator EndGame()
    {
        isDead = true;
        yield return new WaitForSeconds(0.05f);
        if (canDie)
        {
            fogVFX.transform.SetParent(transform,false);
            speed = 0;
            for (int i = _segments.Count - 1 ; i >= 1; i--)
            {
                _segments[i].GetComponent<manager>().explosion();

                if (canDie)
                {
                    explosion1.Play(0);
                    Shake(2f, 0.1f);
                }
                
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(1f);
            if (Score >= bestscore)
            {
                bestscore = Score;
            }
            Score = 0;
            ResetState();
        }
        else
        {
            isDead = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if (other.tag == "Obstacle")
        {
            if (canDie)
            {
                
                foodmange = 0;
                isRandom = false;
                StopAllCoroutines();
                Debug.Log("gameouver");
                isboost = false;
                StartCoroutine(EndGame());
            }
        }
        

    }

    

    IEnumerator CanDie()
    {
        canDie = false;
        yield return new WaitForSeconds(1f);
        canDie = true;
    }
    
}
