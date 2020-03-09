using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpd = 10F;
    [SerializeField] float paddle = 1F;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpd = 10F;
    [SerializeField] float projectileFiringPeriod = 0.1F;

    Coroutine firingCoroutine;
    float xMin,yMin;
    float xMax,yMax;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }
    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpd);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal")* Time.deltaTime * moveSpd;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpd;


        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + paddle;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - paddle;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + paddle;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - paddle;
    }
}
