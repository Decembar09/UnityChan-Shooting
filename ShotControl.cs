using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class ShotControl : MonoBehaviour
{
    [SerializeField]private GameObject _shotOriginal;
    [SerializeField]private GameObject[] shotPositionObject;//= new shotPositionObject[5];//配列で宣言
    [SerializeField]private LockOnMark _markerPrefab;
    [SerializeField]private RectTransform _markerTransform;

    private Vector3 shotPosition;
    private float _timerW1 = 0f, _timerW2 = 0f;
    private bool _weaponBoo = true;//武器１の武器２ On/Off を選択
    //private bool _searchEnemyStatus = true;//右クリックによる　索敵LockOn状態/発射状態を選択
    private const float maxDistance = 1200;//修飾子connst（値を1200に固定）finalizeに似てる
    private Ray ray;
    private RaycastHit hit;//型はオブジェクト（RayにHitしたオブジェクト)
    private GameObject hitObject;
    private GameObject[] lockOnMarks = new GameObjectk[5];
    private string[] lockOnObjects = new string[5] {"","","","",""};
    //[SerializeField]private MissileBase[] _missileObject = new MissileBase[5];
    private Vector3 _shotPositionCorrection = new Vector3(0, 1, 2);
    private int i = 0; int targetNum = 0;

   
    // Updte is called once per frame

    void Start() {
    }

    void Update()
    {
        //(Cameraの座標、Cameraの向き)からRayを宣言 tag"MainCamera"を使っているようなので変更するとエラーになる。
        ray = new Ray(Camera.main.transform.localPosition, Camera.main.transform.forward);
        //RaycastHit hit;//型はオブジェクト（RayにHitしたオブジェクト）

        //MaxDistance内でオブジェクトにHitしたら（true）でhitにオブジェクトを代入
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            hitObject = hit.collider.gameObject;

            if (!_weaponBoo && targetNum <= 5)
            {

                if (lockOnObjects[targetNum] != hitObject.tag && hitObject.gameObject.layer == 9)//layer 9 Enemy
                {
                 lockOnObjects[targetNum] = hitObject.tag;
                 LockOnMark marker = (LockOnMark)Instantiate(_markerPrefab, _markerTransform);//_markerTransformの子オブジェクトとして作られる。

                    //marker.Initialize(hit.transform);
                    marker.SetTarget(hitObject.tag);
                    targetNum++;
               }
            }
            else {
                if (targetNum != 0)
                {
                    //Destroy(lockOnMarks);
                    /*_markerTransform;
                    GameObject lockOnMarks = GameObject.FindGameObjectsWithTag("LockOn");
                    foreach (GameObject lockOnMarks_Soccer in lockOnMarks)
                    {
                        //Destroy(lockOnMarks_Soccer);
                    }
                    targetNum=0;
                  */
                }
            }
            //_targetMarkBase.SetLockOn(hitObject);
        }

        _timerW1 += Time.deltaTime;
        _timerW2 += Time.deltaTime;


        if (hitObject != null && _timerW1 >= 0.3f && _weaponBoo)
        {

            if (hitObject.gameObject.layer != 7) //layer 7"Ally"
            {
                PlayerBase.GetInstance().PlayerAttack();

                for (i = 0; i < 2; i++)
                {
                    shotPosition = Camera.main.transform.localPosition;// +shotPositionObject[i].transform.position;// +new Vector3(3 + i * 3, 0, 8);
                    GameObject shotClone = (GameObject)Instantiate(_shotOriginal, shotPosition + new Vector3(-i * 2, 0, 0), Quaternion.identity);

                    //shotClone.transform.LookAt(hitObject.transform.localPosition);
                    Rigidbody shotRigidBody = shotClone.gameObject.GetComponent<Rigidbody>();//Objectに入っているComponentを取得
                    //Debug.Log(shotClone[i].transform.forword);
                    shotRigidBody.AddForce(Camera.main.transform.forward * 8000);//shotClone[iの向いている方向に8000倍で力を加える。
                }
            }
            _timerW1 = 0;
        }


        if (Input.GetMouseButtonDown(1))//; 0左クリック; 1右クリック; 2真ん中 
        {

            if (!_weaponBoo)//武器２でLocckOn状態
            {
                //if (targetNum == 0) { }
                if (_timerW2 >= 1f && targetNum != 0)
                {
                    PlayerBase.GetInstance().PlayerAttack();
                    //射撃の発生場所＝Cameraの位置

                    for (i = 0; i <= targetNum; i++)
                        {
                        shotPosition = Camera.main.transform.localPosition;//Vector3 座標の変数の型（x,y,z）

                        //defoultの角度(回転なし)=（Quaternion.identity）で_shotCloneを生成
                        //回転させたい場合は
                        //Quaternion move_q = Quaternion.Euler(0f,0f,1.0f);
                        GameObject shotClone = (GameObject)Instantiate(shotPositionObject[0], shotPosition, Quaternion.identity);

                        //if (lockOnObjects[targetNum] != null)
                        //{
                            //shotClone.gameObject.transform.LookAt(lockOnObjects[targetNum].transform.localPosition);
                        //}

                        //AddFoceメソッドで弾が発射＆移動
                        Rigidbody shotRigidBody = shotClone.gameObject.GetComponent<Rigidbody>();//Objectに入っているComponentを取得
                        shotRigidBody.AddForce(Camera.main.transform.forward * 1000);//Cameraの向いている方向に1倍で力を加える。
                        }
                    //下のように一文が書くこともできる
                    //shotClone.gameObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward*000);
                    _timerW2 = 0;
                    targetNum = 0;
                    //_searchEnemyStatus = true;//LockOn状態を索敵状態
                    
                }
            }
            _weaponBoo = !_weaponBoo;//武器1,2の変更
        }

         hitObject = null;
    }
}
