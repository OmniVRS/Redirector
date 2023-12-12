using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    public GameObject missilePrefab;
    public List<GameObject> rocketChildren;
    public GameObject spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveDown()); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        clearList();
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
        ShootRocket();
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

    private void ShootRocket()
    {
        if (rocketChildren.Count == 0)
        {
            GameObject thisMissile = Instantiate(missilePrefab, spawnPoint.transform.position, missilePrefab.transform.rotation);
            thisMissile.GetComponent<MissileBehavior>().sender = this.gameObject;
            rocketChildren.Add(thisMissile.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Kills Player"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void clearList()
    {
        if (rocketChildren.Count > 0 && rocketChildren != null)
        {
            for (int i = 0; i < rocketChildren.Count; i++)
            {
                if (rocketChildren[i] == null) 
                {
                    rocketChildren.RemoveAt(i);
                }
            }
        }
    }
}
