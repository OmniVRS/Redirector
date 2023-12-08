using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class RocketShooterAI : MonoBehaviour
{
    private float downSpeed = 50;
    private bool isMovingDown = true;
    private bool isStrafing;
    private float speed = 30;
    private int randomXDirection;
    private float randomMoveTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveDown()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveDown()
    {
        StartCoroutine(MovementDuration());
        while (isMovingDown)
        {
            transform.Translate(Vector3.back * downSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        
    }

    IEnumerator MovementDuration()
    {
        //Debug.Log("I have a duration!");
        yield return new WaitForSeconds(randomMoveTime);
        isMovingDown = false;
        isStrafing = false;
        StartCoroutine(RandomHorizMove());
    }

    IEnumerator RandomHorizMove()
    {
        yield return new WaitForSeconds(1);
        MoveGenerator();
        isStrafing = true;
        StartCoroutine(MovementDuration());
            if(randomXDirection == 0)
            {
                while (isStrafing)
                {
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
            }
            if (randomXDirection == 1)
            {
                while (isStrafing)
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
            }
    }

    private void MoveGenerator()
    {
        randomXDirection = Random.Range(0, 2);
        randomMoveTime = Random.Range(0f, 3f);
        //Debug.Log($"Move time is: {randomMoveTime} seconds. Random direction is: {randomXDirection}");
    }
}
