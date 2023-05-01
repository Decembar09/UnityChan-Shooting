using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //[SerializeField]private GameObject _weakness;
    [SerializeField]private GameObject _shotOriginal;
    [SerializeField]private GameObject _destroyEffect;

    public int Identification_Num;//prehab化で生成するときに番号をつける。
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

        //defoultの角度(回転なし)=（Quaternion.identity）で_shotCloneを生成
        //回転させたい場合は
        //Quaternion move_q = Quaternion.Euler(0f,0f,1.0f);

        //defoultの角度（Quaternion.identity）で_shotCloneを生成
        GameObject shotClone = (GameObject)Instantiate(_shotOriginal, transform.localPosition + _shotPositionCorrection, Quaternion.identity);

        shotClone.transform.LookAt(Camera.main.transform.localPosition);

        //AddFoceメソッドで弾が発射＆移動
        Rigidbody shotRigidBody = shotClone.gameObject.GetComponent<Rigidbody>();//Objectに入っているComponentを取得
        shotRigidBody.AddForce(shotClone.transform.forward * 250);//250倍で力を加える。

    }

    private void OnTriggerEnter(Collider other){

        //Debug.Log("layer      " + this.gameObject.layer);
        //Debug.Log("layer Name " + LayerMask.LayerToName(this.gameObject.layer));
        //逆に名前からレイヤーに変換する場合はLayerMask.NameToLayer("layer_name")

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
