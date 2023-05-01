using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBase : MonoBehaviour
{
    
    [SerializeField]private GameObject _bombEffect;
    private float _shotLifeTimer = 5.0f;//C#�ŏ������g���ꍇ�́uf�v������
    private GameObject _lockOnObject;
   

    // Update is called once per frame
    void Update()
    {
        _shotLifeTimer -= Time.deltaTime;//��̈�b�ԂɂP�t���[������

        if (_shotLifeTimer <= 0)
        {
            GameObject BombEffects = (GameObject)Instantiate(_bombEffect, transform.localPosition, Quaternion.identity);
            Destroy(this.gameObject);//addComponent�Œǉ����Ă���I�u�W�F�N�g���폜
        }

        if (_lockOnObject != null)
        {
            this.gameObject.transform.LookAt(_lockOnObject.transform.localPosition);
        }
        
        //AddFoce���\�b�h�Œe�����ˁ��ړ�
        //Rigidbody shotRigidBody = this.gameObject.GetComponent<Rigidbody>();//Object�ɓ����Ă���Component���擾
        //shotRigidBody.AddForce(this.transform.forward * 100);

    }

    //�I�u�W�F�N�g�R���|�[�l���g��collider��[is Trigger]���`�F�b�N�ɂ��Ă����ƌĂяo������^��
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)//layer 9 Enemy
        {
            GameObject BombEffects = (GameObject)Instantiate(_bombEffect, transform.localPosition, Quaternion.identity);
            Destroy(this.gameObject);
       }//������̔�r�ł�==���g��
    }

//    public static MissileBase GetInstance()
//    { return _instance; }

    public void SetTarget(string tagName)
    {

        Debug.Log("Target_tagName IN Missile  " + tagName);

        _lockOnObject = GameObject.FindGameObjectWithTag(tagName);
    }
    
}
