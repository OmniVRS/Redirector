using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class GeneralShooterAI : MonoBehaviour
{
    private float downSpeed = 50;
    private bool isMovingDown = true;
    private float speed = 1;
    private bool isRunning = false;
    private bool isStrafing = false;

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
        yield return new WaitForSeconds(1);
        isMovingDown = false;
        StartCoroutine(RandomHorizMove());
    }

    IEnumerator RandomHorizMove()
    {
        while (!isMovingDown && !isRunning)
        {
            isRunning = true;
            int randomXDirection = Random.Range(0, 2);
            float randomMoveTime = Random.Range(0f, 3f);

            if(randomXDirection == 0 && !isStrafing)
            {
                isStrafing = true;
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                yield return new WaitForSeconds(randomMoveTime);
            }
        }
        yield return null;
    }
}
