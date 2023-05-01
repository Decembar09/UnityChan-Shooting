using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBase : MonoBehaviour
{
    
    [SerializeField]private GameObject _bombEffect;
    private float _shotLifeTimer = 5.0f;//C#で少数を使う場合は「f」を入れる
    private GameObject _lockOnObject;
   

    // Update is called once per frame
    void Update()
    {
        _shotLifeTimer -= Time.deltaTime;//大体一秒間に１フレーム減る

        if (_shotLifeTimer <= 0)
        {
            GameObject BombEffects = (GameObject)Instantiate(_bombEffect, transform.localPosition, Quaternion.identity);
            Destroy(this.gameObject);//addComponentで追加しているオブジェクトを削除
        }

        if (_lockOnObject != null)
        {
            this.gameObject.transform.LookAt(_lockOnObject.transform.localPosition);
        }
        
        //AddFoceメソッドで弾が発射＆移動
        //Rigidbody shotRigidBody = this.gameObject.GetComponent<Rigidbody>();//Objectに入っているComponentを取得
        //shotRigidBody.AddForce(this.transform.forward * 100);

    }

    //オブジェクトコンポーネントのcolliderの[is Trigger]をチェックにしておくと呼び出される定型文
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)//layer 9 Enemy
        {
            GameObject BombEffects = (GameObject)Instantiate(_bombEffect, transform.localPosition, Quaternion.identity);
            Destroy(this.gameObject);
       }//文字列の比較では==を使う
    }

//    public static MissileBase GetInstance()
//    { return _instance; }

    public void SetTarget(string tagName)
    {

        Debug.Log("Target_tagName IN Missile  " + tagName);

        _lockOnObject = GameObject.FindGameObjectWithTag(tagName);
    }
    
}
