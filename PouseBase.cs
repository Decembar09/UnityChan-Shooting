using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouseBase : MonoBehaviour
{
    [SerializeField]
    private GameObject _gamePouse;
    //[SerializeField]private GameObject _gamePouseText;

    // Start is called before the first frame update
    void Start()
    {
        _gamePouse.SetActive(false);
        //_gamePouseText.SetActive(false);
    }

    // Update is called once per frame
    //void Update(){}

    public void GamePouse(bool pouse)
    {
        /*
        Time.timeScale�́uUnity���̎��ԁv�̌o�߂��鑬����\���ϐ��ł��B
        �����l�͂P�ŁA����́u�������E�̎��ԁv�̑����Ɠ��������Ƃ��Ӗ����Ă��܂��B
        �Ⴆ�΂��̒l��0.5�ɂ���ƁA�u�������E�̎��ԁv�̔����̑����ł������Ǝ��Ԃ�����A
        �l���Q�ɂ���ƁA�u�������E�̎��ԁv�̔{�̑����ł��΂₭���Ԃ������A�Ƃ�������ɂȂ�܂��B
        �܂�A�u�������E�̎��ԁv�̑����Ƃ̔��ݒ�l�Ƃ��Ď��킯�ł��B

        �ݒ�l��Edit > Project Settings > Time�Ŋm�F�ł��܂��B
         */
        if (pouse) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }

        _gamePouse.SetActive(pouse);
        //_gamePouseText.SetActive(pouse);
    }

}
