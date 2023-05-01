using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //[SerializeField]private GameObject _weakness;
    [SerializeField]private GameObject _shotOriginal;
    [SerializeField]private GameObject _destroyEffect;

    public int Identification_Num;//prehab���Ő�������Ƃ��ɔԍ�������B
    public int LifePoint = 1;//Life of Enemy
    private float _timer = 0f;
    private Vector3 _shotPositionCorrection = new Vector3(0,10,0);
 
    // Start is called before the first frame update
    //void Start(){
    //}

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer <= 10f){
            return;
        }
        _timer = 0;

        //defoult�̊p�x(��]�Ȃ�)=�iQuaternion.identity�j��_shotClone�𐶐�
        //��]���������ꍇ��
        //Quaternion move_q = Quaternion.Euler(0f,0f,1.0f);

        //defoult�̊p�x�iQuaternion.identity�j��_shotClone�𐶐�
        GameObject shotClone = (GameObject)Instantiate(_shotOriginal, transform.localPosition + _shotPositionCorrection, Quaternion.identity);

        shotClone.transform.LookAt(Camera.main.transform.localPosition);

        //AddFoce���\�b�h�Œe�����ˁ��ړ�
        Rigidbody shotRigidBody = shotClone.gameObject.GetComponent<Rigidbody>();//Object�ɓ����Ă���Component���擾
        shotRigidBody.AddForce(shotClone.transform.forward * 250);//250�{�ŗ͂�������B

    }

    private void OnTriggerEnter(Collider other){

        //Debug.Log("layer      " + this.gameObject.layer);
        //Debug.Log("layer Name " + LayerMask.LayerToName(this.gameObject.layer));
        //�t�ɖ��O���烌�C���[�ɕϊ�����ꍇ��LayerMask.NameToLayer("layer_name")

        if (other.gameObject.layer == 9) { return; }//layer 9 Enemy

        LifePoint--;

            if (LifePoint == 0) {

                _timer = 0;

                PlayerBase.GetInstance().AddScore(10);

                GameObject destroyEffects = (GameObject)Instantiate(_destroyEffect, transform.localPosition + _shotPositionCorrection, Quaternion.identity);
                //destroyEffects.transform.LookAt(Camera.main.transform.localPosition);

                Destroy(this.gameObject);
            }
    }

    public void SetTag(string newTag)
    {
        this.tag = newTag;
    }

}
