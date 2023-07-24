using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootComponent : MonoBehaviour, IShootComponent
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform outBullet;
    [SerializeField] private Text text;
    [SerializeField] private ParticleSystem gunExitParticle;//система частиц
    public bool IsModeBull = false;
    //соберем в лист стороние скрипты
    public List<MonoBehaviour> CollisionAction = new List<MonoBehaviour>();
    public float ShootDelay;
    private float shootTime = float.MinValue;
    private BullComponent scrBullet;
    private void Start()
    {
        scrBullet = bullet.GetComponent<BullComponent>();
        StartCoroutine(Example());
    }

    private IEnumerator Example()
    {
        int i = 0;
        while (i < 3)
        {
            yield return new WaitForSeconds(0.2f);
            i++;
        }
        DataStart();
    }

    private void DataStart()
    {
        text.text = $"Bullet = {Statistic.ShootCount}";
    }

    public void Shoot()
    {
        if (Time.time < shootTime + ShootDelay)
        {
            return;
        }
        else
        {
            shootTime = Time.time;
        }

        Instantiate(bullet, outBullet.position, outBullet.rotation);
        gunExitParticle.Play();
        //static
        Statistic.ShootCount++;
        text.text = $"Bullet = {Statistic.ShootCount}";

        if (IsModeBull)
        {
            scrBullet.IsMode=true;
        }
        else
        {
            scrBullet.IsMode = false;
        }
        
       // currentBulletVelocity.AddForce(outBullet.up * bulletSpeed, ForceMode.Force);

    }
    
}
