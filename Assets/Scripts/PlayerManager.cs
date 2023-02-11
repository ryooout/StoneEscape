using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StateManager;
/// <summary>�v���C���[�𑀍삷�邽�߂̃X�N���v�g</summary>
public class PlayerManager : MonoBehaviour
{
    Rigidbody2D _rb;
    Animator _anim;
    TouchManager _touch;
    /// <summary>�A�j���[�^�[�̕�����Id</summary>
    private int _walkId = Animator.StringToHash("Walk");
    /// <summary>�A�j���[�^�[�̃W�����v����Id</summary>
    private int _jumpId = Animator.StringToHash("Jump");
    /// <summary>�A�j���[�^�[�̃A�C�h������Id</summary>
    private int _idleId = Animator.StringToHash("Idle");
    [SerializeField, Tooltip("�W�����v��")]
    private int _jumpCount;
    [SerializeField, Tooltip("�W�����v�̍ő��")]
    private int _jumpCountMax;
    [SerializeField, Tooltip("���C�t")]
    private int _lifePoint = 3;
    [SerializeField, Tooltip("���C�t�|�C���g�̉摜�z��")]
    private Image[] _lifePointPng = new Image[3];
    [SerializeField, Tooltip("�n�ʂ̃^�O")]
    private string _groundTag;
    [SerializeField, Tooltip("�v���C���[�̃X�s�[�h")]
    private float _speed;
    [SerializeField, Tooltip("�W�����v��")]
    private float _jumpPower;
    [SerializeField, Tooltip("�n�ʂɒ��n���Ă��邩")]
    private bool _isGround;
    [SerializeField, Tooltip("�����Ă��邩")]
    private bool _isMove;
    [SerializeField, Tooltip("�����Ă�Ƃ��ƁA�����Ă�Ƃ��̃R���C�_�[")]
    private Collider2D[] _colider;//1,�����Ă�Ƃ��A2.�����Ă�Ƃ�
    void Start()
    {
        //�C���X�^���X�̐���
        _touch = new TouchManager();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _isMove = true;
    }
    private void Update()
    {
        PlayerMove();
    }
    /// <summary>�v���C���[�̓���</summary>
    private void PlayerMove()
    {
        // �^�b�`��ԍX�V
        this._touch.update();
        // �^�b�`�擾
        TouchManager touch_state = this._touch.getTouch();

        //�����̐���
        if (_isMove)
        {
            this.transform.Translate(_speed, 0, 0);
            _anim.SetBool(_walkId, true);
            _colider[0].enabled = true;
            _colider[1].enabled = false;
        }
        else
        {
            _anim.SetBool(_walkId, false);
            _colider[0].enabled = false;
            _colider[1].enabled = true;
        }

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
            else if (!_isGround && _jumpCount < _jumpCountMax)
            {
                _rb.AddForce(Vector2.up * (_jumpPower + 1), ForceMode2D.Impulse);
                _anim.SetBool(_jumpId, true);
                _jumpCount++;
                Debug.Log(_jumpCount);
            }
        }
        if (_touch._touch_flag)
        {
            //�}�E�X�N���b�N�����Ƃ��̋���
            if (_touch._touch_phase == TouchPhase.Moved)
            {
                this.transform.Translate(0, 0, 0);
                _isMove = false;
                Debug.Log("�������ςȂ�");
                _anim.SetTrigger(_idleId);
            }
            if (_touch._touch_phase == TouchPhase.Ended)
            {
                _isMove = true;
                Debug.Log("������");
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
            Debug.Log(collision.collider.gameObject);
        }
        //��ɂ���������
        StoneManager _stone = collision.collider.GetComponent<StoneManager>();
        if (_stone != null)
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
