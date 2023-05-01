using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayTimeControl : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _timeText;
    [SerializeField]private PouseBase _gamePouseBase;

    public static bool beingMeasured; // �v�����ł��邱�Ƃ�\���ϐ�

    private float time; // �o�ߎ��Ԃ��i�[����ϐ�
    
    // Start is called before the first frame update
    void Start()
    {
        // �����l�́u�v�����v���
        beingMeasured = true;
        time = 0.0f;
        //_timeText = GetComponent<Text>();
        _timeText.text = "TIME:" + "00:00:00";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))// 0���N���b�N; 1�E�N���b�N; 2�^�� 
        {
            _gamePouseBase.GamePouse(beingMeasured);
            
            // TimerScript��beingMeasured�̒l�𔽓]������
            beingMeasured = !beingMeasured;
            // �{�^���̕�����؂�ւ���
            _timeText.text = beingMeasured ? "STOP" : "START";
        }

        if (!beingMeasured)
        {
            return;///// �u�v�����ł͂Ȃ��v�ꍇ�A�ȍ~�̏����͎��s���Ȃ� /////
        }

        /*Time.unscaledTime�ɂ���
            Time.unscaledDeltaTime��Time.deltaTime���l�A�O��̃t���[������̌o�ߎ��ԁi�b�j��\���Ă��܂����A
            Time.deltaTime�Ƃ͈قȂ�Time.timeScale�̉e�����󂯂܂���B
            ���̂��߁ATime.unscaledDeltaTime���g�����^�C�}�[��Time.timeScale�̒l�ɂ�炸���̑����Ői�ݑ����Ă��܂��B
        */
        time += Time.deltaTime;
        //ToString(�g0.00�h)��float�^��string�^�ɕϊ����郁�\�b�h�ł��B
        //�����͕ϊ���̃t�H�[�}�b�g���Ӗ����Ă���A��L�̏������́u�i�l�̌ܓ������A�j������Q�ʂ܂ŕ\������v���Ƃ�\���Ă��܂��B
        _timeText.text = "TIME:" + time.ToString("000.00");
    }

    public void SetTime(int time)
    {
        //Time.deltaTime//��0.02�b
        _timeText.text = "TIME:" + time;
    }

/*
    public void OnClick() //Object��Button�̎�
    {
        // beingMeasured�̒l�𔽓]������
        beingMeasured = !beingMeasured;
        // �{�^���̕�����؂�ւ���
        _timeText.text = beingMeasured ? "STOP" : "START";
    }
 */
}
