using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class ShotControl : MonoBehaviour
{
    [SerializeField]private GameObject _shotOriginal;
    [SerializeField]private GameObject[] shotPositionObject;//= new shotPositionObject[5];//�z��Ő錾
    [SerializeField]private LockOnMark _markerPrefab;
    [SerializeField]private RectTransform _markerTransform;

    private Vector3 shotPosition;
    private float _timerW1 = 0f, _timerW2 = 0f;
    private bool _weaponBoo = true;//����P�̕���Q On/Off ��I��
    //private bool _searchEnemyStatus = true;//�E�N���b�N�ɂ��@���GLockOn���/���ˏ�Ԃ�I��
    private const float maxDistance = 1200;//�C���qconnst�i�l��1200�ɌŒ�jfinalize�Ɏ��Ă�
    private Ray ray;
    private RaycastHit hit;//�^�̓I�u�W�F�N�g�iRay��Hit�����I�u�W�F�N�g)
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
        //(Camera�̍��W�ACamera�̌���)����Ray��錾 tag"MainCamera"���g���Ă���悤�Ȃ̂ŕύX����ƃG���[�ɂȂ�B
        ray = new Ray(Camera.main.transform.localPosition, Camera.main.transform.forward);
        //RaycastHit hit;//�^�̓I�u�W�F�N�g�iRay��Hit�����I�u�W�F�N�g�j

        //MaxDistance���ŃI�u�W�F�N�g��Hit������itrue�j��hit�ɃI�u�W�F�N�g����
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            hitObject = hit.collider.gameObject;

            if (!_weaponBoo && targetNum <= 5)
            {

                if (lockOnObjects[targetNum] != hitObject.tag && hitObject.gameObject.layer == 9)//layer 9 Enemy
                {
                 lockOnObjects[targetNum] = hitObject.tag;
                 LockOnMark marker = (LockOnMark)Instantiate(_markerPrefab, _markerTransform);//_markerTransform�̎q�I�u�W�F�N�g�Ƃ��č����B

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
                    Rigidbody shotRigidBody = shotClone.gameObject.GetComponent<Rigidbody>();//Object�ɓ����Ă���Component���擾
                    //Debug.Log(shotClone[i].transform.forword);
                    shotRigidBody.AddForce(Camera.main.transform.forward * 8000);//shotClone[i�̌����Ă��������8000�{�ŗ͂�������B
                }
            }
            _timerW1 = 0;
        }


        if (Input.GetMouseButtonDown(1))//; 0���N���b�N; 1�E�N���b�N; 2�^�� 
        {

            if (!_weaponBoo)//����Q��LocckOn���
            {
                //if (targetNum == 0) { }
                if (_timerW2 >= 1f && targetNum != 0)
                {
                    PlayerBase.GetInstance().PlayerAttack();
                    //�ˌ��̔����ꏊ��Camera�̈ʒu

                    for (i = 0; i <= targetNum; i++)
                        {
                        shotPosition = Camera.main.transform.localPosition;//Vector3 ���W�̕ϐ��̌^�ix,y,z�j

                        //defoult�̊p�x(��]�Ȃ�)=�iQuaternion.identity�j��_shotClone�𐶐�
                        //��]���������ꍇ��
                        //Quaternion move_q = Quaternion.Euler(0f,0f,1.0f);
                        GameObject shotClone = (GameObject)Instantiate(shotPositionObject[0], shotPosition, Quaternion.identity);

                        //if (lockOnObjects[targetNum] != null)
                        //{
                            //shotClone.gameObject.transform.LookAt(lockOnObjects[targetNum].transform.localPosition);
                        //}

                        //AddFoce���\�b�h�Œe�����ˁ��ړ�
                        Rigidbody shotRigidBody = shotClone.gameObject.GetComponent<Rigidbody>();//Object�ɓ����Ă���Component���擾
                        shotRigidBody.AddForce(Camera.main.transform.forward * 1000);//Camera�̌����Ă��������1�{�ŗ͂�������B
                        }
                    //���̂悤�Ɉꕶ���������Ƃ��ł���
                    //shotClone.gameObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward*000);
                    _timerW2 = 0;
                    targetNum = 0;
                    //_searchEnemyStatus = true;//LockOn��Ԃ����G���
                    
                }
            }
            _weaponBoo = !_weaponBoo;//����1,2�̕ύX
        }

         hitObject = null;
    }
}
