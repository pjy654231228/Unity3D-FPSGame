using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

public class AssualtRifle : Firearms
{
    private AnimatorStateInfo GunStateInfo;
    private IEnumerator checkReloadAmmoAnimationEnd_Coroutine;

    public Text pointText;

    protected override void Start()
    {
        base.Start();
        checkReloadAmmoAnimationEnd_Coroutine = CheckReloadAmmoAnimationEnd();
        
    }



    protected override void Reload()
    {
        GunAnimator.SetLayerWeight(1, 1);
        GunAnimator.SetTrigger(CurrentAmmo > 0 ? "ReloadLeft" : "ReloadOutOf");
        //通过赋值协程给变量来实现反复执行
        if (checkReloadAmmoAnimationEnd_Coroutine == null)
        {
            checkReloadAmmoAnimationEnd_Coroutine = CheckReloadAmmoAnimationEnd();
            StartCoroutine(checkReloadAmmoAnimationEnd_Coroutine);
        }
        else
        {
            StopCoroutine(checkReloadAmmoAnimationEnd_Coroutine);
            checkReloadAmmoAnimationEnd_Coroutine = null;
            checkReloadAmmoAnimationEnd_Coroutine = CheckReloadAmmoAnimationEnd();
            StartCoroutine(checkReloadAmmoAnimationEnd_Coroutine);
        }



    }

    protected override void Shooting()
    {
        if (CurrentAmmo <= 0) return;
        if (!IsAllowShooting()) return;
        MuzzleParticle.Play();
        CurrentAmmo -= 1;
        GunAnimator.Play(stateName: "Fire", layer: 0, normalizedTime: 0);
        CreateBullet();
        CasingParticle.Play();
        LastFireTime = Time.time;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            DoAttack();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {

            Reload();
        }
    }
    private void CreateBullet()
    {
        //new Vector3  = (MuzzlePoint.rotation.x, MuzzlePoint.rotation.y + 90, MuzzlePoint.rotation.x);
        GameObject tmp_Bullet = Instantiate(BulletPrefab, MuzzlePoint.position, MuzzlePoint.rotation);
       // var tmp_BulletRigidbody = tmp_Bullet.AddComponent<Rigidbody>();
        //tmp_BulletRigidbody.velocity = tmp_Bullet.transform.right * 200f;
        var tmp_BulletScript=tmp_Bullet.AddComponent<Bullet>();
        tmp_BulletScript.scoreText = pointText;
        tmp_BulletScript.BulletSpeed = 200;

    }

    
    private IEnumerator CheckReloadAmmoAnimationEnd()
    {
        while (true)
        {
            yield return null;
            GunStateInfo = GunAnimator.GetCurrentAnimatorStateInfo(1);
            if (GunStateInfo.IsTag("ReloadAmmo"))
            {
                if (GunStateInfo.normalizedTime >= 0.7f)
                {
                    int tem_NeedAmmoCount = AmmoInMag - CurrentAmmo;
                    int tem_RemainingCount = CurrentMaxAmmoCarride - tem_NeedAmmoCount;
                    if (tem_RemainingCount <= 0)
                    {
                        CurrentAmmo = CurrentMaxAmmoCarride + CurrentAmmo;
                    }
                    else
                    {
                        CurrentAmmo = AmmoInMag;
                    }
                    CurrentMaxAmmoCarride = tem_RemainingCount <= 0 ? 0 : tem_RemainingCount;

                    yield break;
                }

            }

        }

    }
}