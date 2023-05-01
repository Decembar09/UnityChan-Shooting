using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Utityのインップトシステムをダウンロードしてジョイスティックが使える。
//Assetstoreにもあるらしい。

/// <summary>
/// ジョイスティックの動きを受け取ってプレイヤーカメラを動かす。
/// </summary>
public class PlayerCameraMove : MonoBehaviour
{
    //プレイヤーのオブジェクト（この場合カメラ）
    [SerializeField]private GameObject _player;
    [SerializeField]private InputAction _moveAction;

    private Vector2 _joyStickPosition;
    private bool _isJoyStickMove = false;

    // Start is called before the first frame update
    void Start()
    {
        _moveAction.Enable();//有効化する

        //ジョイスティックを動かしたとき呼び出される。
        _moveAction.performed += _ =>
        {
            Vector2 value = _moveAction.ReadValue<Vector2>();
            _joyStickPosition = value;
            _isJoyStickMove = true;
        };

        //ジョイスティックが止まったときとき呼び出される。
        _moveAction.canceled += _ =>
        {
            _isJoyStickMove = false;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (_isJoyStickMove) {
            //プレイヤーのアングルを受け取る。
            Vector3 angle = _player.transform.localEulerAngles;

            //大体一秒で一度動くの*５０で調整
            angle.x -= _joyStickPosition.y * Time.deltaTime * 50;
            angle.y += _joyStickPosition.x * Time.deltaTime * 50;

            //編集後の角度を入れる。
            _player.transform.localEulerAngles = angle;
        }



    }
}
