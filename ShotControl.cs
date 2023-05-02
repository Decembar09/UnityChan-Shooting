using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class ShotControl : MonoBehaviour
{
    [SerializeField]private GameObject _shotOriginal;
    [SerializeField]private GameObject[] shotPositionObject;//= new shotPositionObject[5];//�z��Ő錾
    [SerializeField]private LockOnMark _markerPrefab;
    [SerializeField]private RectTransform _markerTransform;

    [SerializeField]private MissileBase _missilePrefab;

    private Vector3 shotPosition;
    private const float maxDistance = 1200;//�C���qconnst�i�l��1200�ɌŒ�jfinalize�Ɏ��Ă�
    private Ray ray;
    private RaycastHit hit;//�^�̓I�u�W�F�N�g�iRay��Hit�����I�u�W�F�N�g)
    private GameObject hitObject;
    
    private bool _weaponBoo = true;//����P�̕���Q On/Off ��I��
    private float _timerW1 = 0f, _timerW2 = 0f;

    //[SerializeField]private MissileBase[] _missileObject = new MissileBase[5];
    //private GameObject[] lockOnMarks = new GameObjectk[5];

    private string[] lockOnObjects = new string[5] {"","","","",""};
    private int targetNum = 0;
    
    private Vector3 _shotPositionCorrection = new Vector3(0, 1, 2);

    private bool LockOnOK = false;//LockOn�ς݂�Tag���ǂ���
    private int i = 0;

   
    // Updte is called once per frame

    //void Start() {}

    void Update()
    {
        //(Camera�̍��W�ACamera�̌���)����Ray��錾 tag"MainCamera"���g���Ă���悤�Ȃ̂�tag��ύX����ƃG���[�ɂȂ�B
        ray = new Ray(Camera.main.transform.localPosition, Camera.main.transform.forward);

        //RaycastHit hit;//�^�̓I�u�W�F�N�g�iRay��Hit�����I�u�W�F�N�g�j
        //MaxDistance���ŃI�u�W�F�N�g��Hit������itrue�j��hit�ɃI�u�W�F�N�g����
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            hitObject = hit.collider.gameObject;

            //����2�̍��G���
            if (!_weaponBoo && targetNum < 5)
            {
                LockOnOK = false;//LockOn�ς݂�Tag���ǂ���

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
                    LockOnMark marker = (LockOnMark)Instantiate(_markerPrefab, _markerTransform);//_markerTransform�̎q�I�u�W�F�N�g�Ƃ��č����B

                    //marker.Initialize(hit.transform);
                    marker.SetTarget(hitObject.tag);
                    targetNum++;
                }
            }
            else {//���킪�ύX����Ă����ꍇ
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


        if (hitObject != null && _timerW1 >= 0.3f && _weaponBoo)//����1
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
                    shotPosition = Camera.main.transform.localPosition;//Vector3 ���W�̕ϐ��̌^�ix,y,z�j

                    for (i = 0; i < lockOnObjects.Length; i++)
                        {
                            if (lockOnObjects[i] != "")
                            {
                                //defoult�̊p�x(��]�Ȃ�)=�iQuaternion.identity�j��_shotClone�𐶐�
                                //��]���������ꍇ��Quaternion move_q = Quaternion.Euler(0f,0f,1.0f);
                                MissileBase missile = (MissileBase)Instantiate(_missilePrefab, shotPosition, Quaternion.identity);

                                //shotClone.gameObject.transform.LookAt(lockOnObjects[targetNum].transform.localPosition);

                                missile.SetTarget(lockOnObjects[i]);
                                lockOnObjects[i] = "";

                                //AddFoce���\�b�h�Œe�����ˁ��ړ�
                                Rigidbody shotRigidBody = missile.gameObject.GetComponent<Rigidbody>();//Object�ɓ����Ă���Component���擾
                                shotRigidBody.AddForce(Camera.main.transform.forward * 100);//Camera�̌����Ă��������1�{�ŗ͂�������B
                                //���̂悤�Ɉꕶ���������Ƃ��ł���
                                //shotClone.gameObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward*000);
                            }
                        }
                        targetNum = 0; _timerW2 = 0;
                }
            }
            _weaponBoo = !_weaponBoo;//����1,2�̕ύX
        }
         hitObject = null;
    }
}
