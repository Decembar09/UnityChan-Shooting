using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���[�_�[�p�̃A�C�R���p�x����
/// </summary>

//�R���p�C�����ł��Ȃ���Inspector��Component��Serialize�ϐ��͔��f����Ȃ��B
/// <summary>
/// ���̂悤�ɃI�u�W�F�N�g�̊e�����𒼐ڕύX���邱�Ƃ͂ł��Ȃ��B
/// transform.position.x = 3.0f;
/// transform.position.y = 4.0f;
/// transform.position.z = 5.0f;
/// �������͉��̂悤�ɏ����B
///transform.position = new Vector3(3.0f,4.0f,5.0f);
/// </summary>
/// 
public class RaderIcon : MonoBehaviour
{
    //[SerializeField]private GameObject _raderBack;
    //[SerializeField]
	//private float _raderBackY=0f;

    //private Transform _this = this.transform;
    //private Vector3 RaderBackV3 = _this.position;


	void Start(){
       //RaderBackV3.x = transform.parent.transform.localposition.x;
       //RaderBackV3.y = _raderBack.transform.localposition.y;
       //RaderBackV3.z = transform.parent.localposition.z;
	}

    // Update is called once per frame
    void Update()
    {
        //�e�I�u�W�F�N�g�̊p�x
        Vector3 parentAngle = transform.parent.transform.localRotation.eulerAngles;
        //�p�x�C��
        gameObject.transform.rotation = Quaternion.Euler(90, parentAngle.y, parentAngle.z);
        //gameObject.transform.position = new Vector3(0f,0f,0f);

        //gameObject.transform.position = RaderBackV3;
    }
}