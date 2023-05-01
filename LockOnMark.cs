using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnMark : MonoBehaviour
{

    [SerializeField]private Camera _targetCamera;

    private static RectTransform _parentUI;
    //��ԏ�̐e�ɕt���Ă���R���|�[�l���g���擾����BCampas
    //�q�̃R���|�[�l���g�͂�GetComponentChildren<RectTransform>();

    //[SerializeField]
    private Transform _target;
    //[SerializeField]
    private Transform _targetUI;
    [SerializeField]private Vector3 _worldOffset=new Vector3(0f, 0f,0f);

    private Vector2 _targetScreenPos;
    private Vector2 uiLocalPos;
    private GameObject _lockOnTarget;
    private string _lockOnTargetTag; 
    private float scale = 3.0f;
    //private float _timer = 0f;

/*
    //���������b�\�h�iPrefab���琶�����鎞�ȂǂɎg���j
    public void Initialize(Transform target, Camera targetCamera = null) {
        _target = target;
        _targetCamera = targetCamera != null ? targetCamera : Camera.main;

        Debug.Log("_target" + _target.transform.localPosition);

        OnUpdatePosition();
    }
*/
        
    public void SetTarget(string tagName) {
        _lockOnTargetTag = tagName;
    }

    private void Awake(){
  
        if(_targetCamera == null){
         _targetCamera = Camera.main;
        }

        _parentUI = GetComponentInParent<RectTransform>();
        //��ԏ�̐e�ɕt���Ă���R���|�[�l���g���擾����BCampas
        //�q�̃R���|�[�l���g�͂�GetComponentChildren<RectTransform>();

        //_parentUI = _targetUI.parent.GetComponent<RectTransform>();
        //_parentUI = this.GetComponent<RectTransform>();
        //����context��Ketword "this"�͎g���Ȃ��B���R�͕s���B
    }

    void Update()
    {
      /*if (_timer < 0.1f)
        {
            _timer += Time.deltaTime; return;
        }*/

        
            scale = scale - 0.5f;
            if (scale >= 0.5f)
            {
                this.transform.localScale = new Vector3(scale, scale, scale);
                //_timer = 0f;
            }

            if (_lockOnTargetTag != null)
            {
                OnUpdatePosition();
            }
    }

    private void OnUpdatePosition(){
 
        var cameraTransform = _targetCamera.transform;
        var cameraDir = cameraTransform.forward;

        /*
         * �I�u�W�F�N�g�ƌ������㓯���ʒu��UI��\�����邽�߂ɂ�
         *�I�u�W�F�N�g�̃��[���h���W���X�N���[�����W��TransFormRect�̃��[�J�����W
         *�̏��ɍ��W�ϊ�����K�v������܂��B
         * ���̂Ƃ��A�J�����w��Ɉʒu����I�u�W�F�N�g���X�N���[�����W�ɓ��e����邽��
         * �w��̂��̂��\���ɂ������ꍇ�͑O�㔻����s���K�v������܂��B
         */
        
        
        //try{
        Debug.Log("_lockOnTargetTag  " + _lockOnTargetTag);

        _lockOnTarget = GameObject.FindGameObjectWithTag(_lockOnTargetTag);

        if (_lockOnTarget == null) { Destroy(this.gameObject); }

        _target = _lockOnTarget.transform;

        var targetWorldPos = _target.position + _worldOffset;
        var targetDir = targetWorldPos - cameraTransform.position;
        

        //���ς��g���ăJ�����O�����ǂ�������
        var isFront = Vector3.Dot(cameraDir,targetDir) > 0;

        this.gameObject.SetActive(isFront);
        //_targetUI.gameObject.SetActive(isFront);
        if(!isFront)return;
        

        _targetScreenPos = _targetCamera.WorldToScreenPoint(targetWorldPos);
        //public Vector3 WorldToScreenPoint(Vector3 position);

         //Debug.Log(_parentUI, transform);
         //Debug.Log(_targetCamera);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentUI,
            _targetScreenPos,
            _targetCamera,//�I�[�o�[���C���[�h�̏ꍇnull
            out uiLocalPos);

        /*public static bool ScreenPointToLocalPointInRectangle(
         * RectTransform rect, �ϊ����RectTransForm���[�J�����W�̐e���w�肵�܂��B
         * Vector2 screenPoint,�ϊ����̃X�N���[�����W���w��
         * Camera cam,
         * out Vector2 localPoint
         * );
         */

        this.transform.localPosition =  uiLocalPos;
         //_targetUI.localPosition =  uiLocalPos;
            //this.localPosition =  uiLocalPos;
        //Debug.Log("_targetScreenPos" + _targetScreenPos);
        //Debug.Log("_targetUI.localPosition" + this.transform.localPosition);
        //}catch(IOException e){Debug.Log("Exception" + e);}
        //}catch(NullReferenceException e){Debug.Log("Exception" + e);}
    }
    public void Delete() { Destroy(this.gameObject); }

}
