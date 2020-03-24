using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpd = 10F;
    [SerializeField] float paddle = 1F;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVol = 0.7F;
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpd = 10F;
    [SerializeField] float projectileFiringPeriod = 0.1F;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSFXVol = 0.1F;

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
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVol);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer dmgDealer = collision.GetComponent<DamageDealer>();
        if (!dmgDealer) { return; }
        ProcessHit(dmgDealer);
    }
    private void ProcessHit(DamageDealer dmgDealer)
    {
        health -= dmgDealer.GetDmg();
        dmgDealer.Hit();
        if(health <=0)
        {
            Die();
        }
    }
    public int GetHealth()
    {
        return health;
    }
    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVol);
    }
}
