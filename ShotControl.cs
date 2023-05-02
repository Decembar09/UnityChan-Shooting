using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class ShotControl : MonoBehaviour
{
    [SerializeField]private GameObject _shotOriginal;
    [SerializeField]private GameObject[] shotPositionObject;//= new shotPositionObject[5];//配列で宣言
    [SerializeField]private LockOnMark _markerPrefab;
    [SerializeField]private RectTransform _markerTransform;

    [SerializeField]private MissileBase _missilePrefab;

    private Vector3 shotPosition;
    private const float maxDistance = 1200;//修飾子connst（値を1200に固定）finalizeに似てる
    private Ray ray;
    private RaycastHit hit;//型はオブジェクト（RayにHitしたオブジェクト)
    private GameObject hitObject;
    
    private bool _weaponBoo = true;//武器１の武器２ On/Off を選択
    private float _timerW1 = 0f, _timerW2 = 0f;

    //[SerializeField]private MissileBase[] _missileObject = new MissileBase[5];
    //private GameObject[] lockOnMarks = new GameObjectk[5];

    private string[] lockOnObjects = new string[5] {"","","","",""};
    private int targetNum = 0;
    
    private Vector3 _shotPositionCorrection = new Vector3(0, 1, 2);

    private bool LockOnOK = false;//LockOn済みのTagかどうか
    private int i = 0;

   
    // Updte is called once per frame

    //void Start() {}

    void Update()
    {
        //(Cameraの座標、Cameraの向き)からRayを宣言 tag"MainCamera"を使っているようなのでtagを変更するとエラーになる。
        ray = new Ray(Camera.main.transform.localPosition, Camera.main.transform.forward);

        //RaycastHit hit;//型はオブジェクト（RayにHitしたオブジェクト）
        //MaxDistance内でオブジェクトにHitしたら（true）でhitにオブジェクトを代入
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            hitObject = hit.collider.gameObject;

            //武器2の索敵状態
            if (!_weaponBoo && targetNum < 5)
            {
                LockOnOK = false;//LockOn済みのTagかどうか

                for (i = 0; i < lockOnObjects.Length; i++)
                {
                    if (lockOnObjects[i] == hitObject.tag)
                    {
                        LockOnOK = true;
                        Debug.Log("ShotControl.cs lockOnObjects[targetNum]=" + i + "  hitObject.tag=" + hitObject.tag);
                        return;
                    }
                }

                if (!LockOnOK && hitObject.gameObject.layer == 9)//layer 9 Enemy
                {
                    lockOnObjects[targetNum] = hitObject.tag;
                    LockOnMark marker = (LockOnMark)Instantiate(_markerPrefab, _markerTransform);//_markerTransformの子オブジェクトとして作られる。

                    //marker.Initialize(hit.transform);
                    marker.SetTarget(hitObject.tag);
                    targetNum++;
                }
            }
            else {//武器が変更されていた場合
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
        }

        _timerW1 += Time.deltaTime;
        _timerW2 += Time.deltaTime;


        if (hitObject != null && _timerW1 >= 0.3f && _weaponBoo)//武器1
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
                    shotPosition = Camera.main.transform.localPosition;//Vector3 座標の変数の型（x,y,z）

                    for (i = 0; i < lockOnObjects.Length; i++)
                        {
                            if (lockOnObjects[i] != "")
                            {
                                //defoultの角度(回転なし)=（Quaternion.identity）で_shotCloneを生成
                                //回転させたい場合はQuaternion move_q = Quaternion.Euler(0f,0f,1.0f);
                                MissileBase missile = (MissileBase)Instantiate(_missilePrefab, shotPosition, Quaternion.identity);

                                //shotClone.gameObject.transform.LookAt(lockOnObjects[targetNum].transform.localPosition);

                                missile.SetTarget(lockOnObjects[i]);
                                lockOnObjects[i] = "";

                                //AddFoceメソッドで弾が発射＆移動
                                Rigidbody shotRigidBody = missile.gameObject.GetComponent<Rigidbody>();//Objectに入っているComponentを取得
                                shotRigidBody.AddForce(Camera.main.transform.forward * 100);//Cameraの向いている方向に1倍で力を加える。
                                //下のように一文が書くこともできる
                                //shotClone.gameObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward*000);
                            }
                        }
                        targetNum = 0; _timerW2 = 0;
                }
            }
            _weaponBoo = !_weaponBoo;//武器1,2の変更
        }
         hitObject = null;
    }
}
