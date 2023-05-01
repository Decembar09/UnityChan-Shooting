using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{

    private static PlayerBase _instance;//���g�̃N���X���i�[

    [SerializeField]private LifeBase _lifeBase;//�̗͂̏��������N���X
    [SerializeField]private ScoreBase _scoreBase;
    [SerializeField]private Animator _playerAnimation;
    [SerializeField]private GameOverBase _gameOverBase;


    public int LifePoint = 0;
    public int _scorePoint = 0;
    //damage���󂯂����̃A�j���[�V��������
    private float _damageTimer = 1.0f;
    private float _attackTimer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        LifePoint = 10;
        _scorePoint = 10;
        _instance = this;
        _lifeBase.SetLife(LifePoint);

    }

    // Update is called once per frame
    void Update()
    {
        if(_playerAnimation.GetBool("Damage")){
            _damageTimer -= Time.deltaTime;

            if(_damageTimer <= 0){
               _playerAnimation.SetBool("Damage",false); 
            }
        }

        if (_playerAnimation.GetBool("Attack")){
            _attackTimer -= Time.deltaTime;

            if (_attackTimer <= 0)
            {
                _playerAnimation.SetBool("Attack", false);
            }
        }

    }

    //PlayerBase�N���X���󂯎��
    public static PlayerBase GetInstance()
    { return _instance; }

    public void PlayerDamage() {

        //if (LifePoint == 0) { return; }
        LifePoint--;

        if (LifePoint == 0) {
            _gameOverBase.GameOverStart();
        }
        //damage���󂯂����̃A�j���C�V����
        _playerAnimation.SetBool("Damage",true);
        _damageTimer = 1.0f;

        _lifeBase.SetLife(LifePoint);
    }

    public void AddScore(int score) 
    {
        _scorePoint += score;
        _scoreBase.SetScore(_scorePoint);
    }

    public void PlayerAttack()
    {
        _playerAnimation.SetBool("Attack", true);
        _attackTimer = 0.5f;
    }

}
