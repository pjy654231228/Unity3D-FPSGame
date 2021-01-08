using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed;
    public GameObject ImpactPrefab;
   
    private Transform bulletTransform;
    private Vector3 prePosition;

    public Text scoreText;
    private int scoreCount;
   



    // Start is called before the first frame update
    void Start()
    {
        //bulletRigidbody = GetComponent<Rigidbody>();
        bulletTransform = transform;
        prePosition = bulletTransform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        prePosition = bulletTransform.position;//同步上一帧的位置
        bulletTransform.Translate(0,0, BulletSpeed * Time.deltaTime);
        if (Physics.Raycast(prePosition,
            (bulletTransform.position-prePosition).normalized,out RaycastHit tmp_Hit,
            (bulletTransform.position - prePosition).magnitude))
        {
            // Debug.DrawRay(bulletTransform.position,bulletTransform.forward,Color.red,1);
            // Debug.Log(tmp_Hit.collider.name);
            if (tmp_Hit.collider.gameObject.tag=="Enemy") {
                ScorePoint();
                tmp_Hit.collider.gameObject.SetActive(false);
            }
           //
            
            
        }

    }

    private void ScorePoint() {
        scoreCount = scoreCount+100;
        //print(scoreCount);
       scoreText.text = "POINT:" + scoreCount;
    }



    /*#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawCube(transform.position,new Vector3(0.1f,0.1f,0.25f));
        }

    #endif*/


}
