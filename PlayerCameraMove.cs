using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Utity�̃C���b�v�g�V�X�e�����_�E�����[�h���ăW���C�X�e�B�b�N���g����B
//Assetstore�ɂ�����炵���B

/// <summary>
/// �W���C�X�e�B�b�N�̓������󂯎���ăv���C���[�J�����𓮂����B
/// </summary>
public class PlayerCameraMove : MonoBehaviour
{
    //�v���C���[�̃I�u�W�F�N�g�i���̏ꍇ�J�����j
    [SerializeField]private GameObject _player;
    [SerializeField]private InputAction _moveAction;

    private Vector2 _joyStickPosition;
    private bool _isJoyStickMove = false;

    // Start is called before the first frame update
    void Start()
    {
        _moveAction.Enable();//�L��������

        //�W���C�X�e�B�b�N�𓮂������Ƃ��Ăяo�����B
        _moveAction.performed += _ =>
        {
            Vector2 value = _moveAction.ReadValue<Vector2>();
            _joyStickPosition = value;
            _isJoyStickMove = true;
        };

        //�W���C�X�e�B�b�N���~�܂����Ƃ��Ƃ��Ăяo�����B
        _moveAction.canceled += _ =>
        {
            _isJoyStickMove = false;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (_isJoyStickMove) {
            //�v���C���[�̃A���O�����󂯎��B
            Vector3 angle = _player.transform.localEulerAngles;

            //��̈�b�ň�x������*�T�O�Œ���
            angle.x -= _joyStickPosition.y * Time.deltaTime * 50;
            angle.y += _joyStickPosition.x * Time.deltaTime * 50;

            //�ҏW��̊p�x������B
            _player.transform.localEulerAngles = angle;
        }



    }
}
