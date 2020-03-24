using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float HP = 400;
    [SerializeField] int scoreValue = 150;

    [Header("Enemy Projectile")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2F;
    [SerializeField] float maxTimeBetweenShots = 3F;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpd = 10F;

    [Header("Enemy SFX")]
    [SerializeField] GameObject expParticlePrefabs;
    [SerializeField] float expDuration = 1F;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVol = 0.5F;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSFXVol = 0.1F;
    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }
    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <=0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }
    private void Fire()
    {
        GameObject laser = Instantiate(projectile,transform.position,Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpd);
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVol);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        HP -= damageDealer.GetDmg();
        damageDealer.Hit();
        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosionParticle = Instantiate(expParticlePrefabs, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosionParticle, expDuration);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position,deathSFXVol);
    }
}
