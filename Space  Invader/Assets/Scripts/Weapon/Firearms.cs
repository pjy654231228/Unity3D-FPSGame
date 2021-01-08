using UnityEngine;
using UnityEditor;


public abstract class Firearms : MonoBehaviour, IWeapon
{
    public Transform MuzzlePoint;//枪口位置
    public Transform CasingPoint;//子弹位置

    public ParticleSystem MuzzleParticle; //枪口火焰粒子控制器
    public ParticleSystem CasingParticle;//子弹粒子控制器

    public float FireRate;//射击速度

    //弹夹
    public int AmmoInMag = 30;
    public int MaxAmmoCarride = 120;
    //
    public GameObject BulletPrefab;

    protected int CurrentAmmo;//当前弹夹容量
    protected int CurrentMaxAmmoCarride;

    protected Animator GunAnimator;
    protected float LastFireTime;

    protected virtual void Start() {
        CurrentAmmo = AmmoInMag;
        CurrentMaxAmmoCarride = MaxAmmoCarride;
        GunAnimator = GetComponent<Animator>();
    }

    public void DoAttack()
    {

        Shooting();
       
    }

    protected abstract void Shooting();

    protected abstract void Reload();


    protected bool IsAllowShooting() {

      return Time.time-LastFireTime> 1 / FireRate;
       
    }
  
}