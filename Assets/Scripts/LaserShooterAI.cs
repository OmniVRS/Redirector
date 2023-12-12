using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooterAI : MonoBehaviour
{
    private float downSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveDown());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    IEnumerator MoveDown()
    {
        while (transform.position.z > 50)
        {
            transform.Translate(Vector3.back * downSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
