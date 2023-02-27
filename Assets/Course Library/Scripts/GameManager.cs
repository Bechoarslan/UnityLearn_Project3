using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPosition = new Vector3(25, 0, 0);
    private float startDelay = 2.0f;
    private float repeatRate = 2.0f;
    private PlayerController stopMove;
    private int randomObstacle;

    public Transform startingPoint;
    public float lerpSpeed;




    // Start is called before the first frame update
    void Start()
    {
        
        stopMove = GameObject.Find("Player").GetComponent<PlayerController>();
        
            InvokeRepeating("SpawnObstacle", startDelay, repeatRate);

        stopMove.gameOver = true;
        StartCoroutine(PlayIntro());
      

    }
    // Update is called once per frame
    void Update()
    {
      
    }
    IEnumerator PlayIntro()
    {
        Vector3 startPos = stopMove.transform.position;
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;
        stopMove.GetComponent<Animator>().SetFloat("Speed_f",
         0.5f);
        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            stopMove.transform.position = Vector3.Lerp(startPos, endPos,
            fractionOfJourney);
            yield return null;
        }
        stopMove.GetComponent<Animator>().SetFloat("Speed_f",
                 1.0f);
        stopMove.gameOver = false;
    }



        private void SpawnObstacle()
    {
        randomObstacle = Random.Range(0, 3);

        if (stopMove.gameOver == false)
        {
            Instantiate(obstaclePrefab[randomObstacle], spawnPosition, obstaclePrefab[randomObstacle].transform.rotation);
        }
        
    }
}
