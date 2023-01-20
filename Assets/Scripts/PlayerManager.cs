using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>プレイヤーを操作するためのスクリプト</summary>
public class PlayerManager : MonoBehaviour
{
    Rigidbody2D _rb;
    Animator _anim;
    /// <summary>アニメーターの歩きのId</summary>
    private int _walkId = Animator.StringToHash("Walk");
    /// <summary>アニメーターのジャンプ時のId</summary>
    private int _jumpId = Animator.StringToHash("Jump");
    /// <summary>アニメーターのアイドル時のId</summary>
    private int _idleId = Animator.StringToHash("Idle");
    [SerializeField, Tooltip("ジャンプ回数")]
    private int _jumpCount;
    [SerializeField, Tooltip("ライフ")]
    private int _lifePoint = 3;
    [SerializeField, Tooltip("ライフポイントの画像配列")]
    private Image[] _lifePointPng = new Image[3];
    [SerializeField,Tooltip("地面のタグ")] 
    private string _groundTag;
    [SerializeField,Tooltip("プレイヤーのスピード")] 
    private float _speed;
    [SerializeField,Tooltip("ジャンプ力")] 
    private float _jumpPower;
    [SerializeField,Tooltip("地面に着地しているか")] 
    private bool _isGround;
    [SerializeField,Tooltip("動いているか")] 
    private bool _isMove;
    [SerializeField,Tooltip("二回キーをクリックしているかどうか")]
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
    /// <summary>プレイヤーの動き</summary>
    private void PlayerMove()
    {
        //ジャンプ処理
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
        //動きの制御
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
                Debug.Log("1回押してる");
                _anim.SetTrigger(_idleId);
            }
            else
            {
                _isMove = true;
                _isMouseClick = false;
                Debug.Log("2回押してる");
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
        //岩にあたった時
        StoneManager _stone = collision.collider.GetComponent<StoneManager>();
        if(_stone!=null)
        {
            Debug.Log("当たった");
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
