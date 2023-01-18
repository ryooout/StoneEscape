using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>�v���C���[�𑀍삷�邽�߂̃X�N���v�g</summary>
public class PlayerManager : MonoBehaviour
{
    Rigidbody2D _rb;
    Animator _anim;
    /// <summary>�A�j���[�^�[�̕�����Id</summary>
    private int _walkId = Animator.StringToHash("Walk");
    /// <summary>�A�j���[�^�[�̃W�����v����Id</summary>
    private int _jumpId = Animator.StringToHash("Jump");
    /// <summary>�A�j���[�^�[�̃A�C�h������Id</summary>
    private int _idleId = Animator.StringToHash("Idle");
    [SerializeField, Tooltip("�W�����v��")]
    private int _jumpCount;
    [SerializeField, Tooltip("���C�t")]
    private int _lifePoint = 3;
    [SerializeField, Tooltip("���C�t�|�C���g�̉摜�z��")]
    private Image[] _lifePointPng = new Image[3];
    [SerializeField,Tooltip("�n�ʂ̃^�O")] 
    private string _groundTag;
    [SerializeField,Tooltip("�v���C���[�̃X�s�[�h")] 
    private float _speed;
    [SerializeField,Tooltip("�W�����v��")] 
    private float _jumpPower;
    [SerializeField,Tooltip("�n�ʂɒ��n���Ă��邩")] 
    private bool _isGround;
    [SerializeField,Tooltip("�����Ă��邩")] 
    private bool _isMove;
    [SerializeField,Tooltip("���L�[���N���b�N���Ă��邩�ǂ���")]
    private bool _isMouseClick;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _isMove = true;
        _isMouseClick = false;
    }
    private void Update()
    {
        PlayerMove();
    }
    /// <summary>�v���C���[�̓���</summary>
    private void PlayerMove()
    {
        //�W�����v����
        if (Input.GetButtonDown("Jump"))
        {
            if (_isGround)
            {
                _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                _anim.SetBool(_jumpId, true);
                _jumpCount++;
                Debug.Log(_jumpCount);
            }
            else if (!_isGround&&_jumpCount<2)
            {
                _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                _anim.SetBool(_jumpId, true);
                _jumpCount++;
                Debug.Log(_jumpCount);
            }
        }
        //�����̐���
        if (_isMove)
        {
            this.transform.Translate(_speed, 0, 0);
            _anim.SetBool(_walkId, true);
        }
        else
        {
            _anim.SetBool(_walkId, false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (!_isMouseClick)
            {
                this.transform.Translate(0, 0, 0);
                _isMove = false;
                _isMouseClick = true;
                Debug.Log("1�񉟂��Ă�");
                _anim.SetTrigger(_idleId);
            }
            else
            {
                _isMove = true;
                _isMouseClick = false;
                Debug.Log("2�񉟂��Ă�");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_groundTag))
        {
            _isGround = true;
            _jumpCount = 0;
            Debug.Log(_jumpCount);
            _anim.SetBool(_jumpId, false);
        }
        //��ɂ���������
        StoneManager _stone = collision.collider.GetComponent<StoneManager>();
        if(_stone!=null)
        {
            Debug.Log("��������");
            _lifePointPng[_lifePoint - 1].gameObject.SetActive(false);
            _lifePoint--;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_groundTag))
        {
            _isGround = false;
        }
    }
}
