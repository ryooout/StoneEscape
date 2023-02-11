using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StateManager;
/// <summary>プレイヤーを操作するためのスクリプト</summary>
public class PlayerManager : MonoBehaviour
{
    Rigidbody2D _rb;
    Animator _anim;
    TouchManager _touch;
    /// <summary>アニメーターの歩きのId</summary>
    private int _walkId = Animator.StringToHash("Walk");
    /// <summary>アニメーターのジャンプ時のId</summary>
    private int _jumpId = Animator.StringToHash("Jump");
    /// <summary>アニメーターのアイドル時のId</summary>
    private int _idleId = Animator.StringToHash("Idle");
    [SerializeField, Tooltip("ジャンプ回数")]
    private int _jumpCount;
    [SerializeField, Tooltip("ジャンプの最大回数")]
    private int _jumpCountMax;
    [SerializeField, Tooltip("ライフ")]
    private int _lifePoint = 3;
    [SerializeField, Tooltip("ライフポイントの画像配列")]
    private Image[] _lifePointPng = new Image[3];
    [SerializeField, Tooltip("地面のタグ")]
    private string _groundTag;
    [SerializeField, Tooltip("プレイヤーのスピード")]
    private float _speed;
    [SerializeField, Tooltip("ジャンプ力")]
    private float _jumpPower;
    [SerializeField, Tooltip("地面に着地しているか")]
    private bool _isGround;
    [SerializeField, Tooltip("動いているか")]
    private bool _isMove;
    [SerializeField, Tooltip("滑ってるときと、立ってるときのコライダー")]
    private Collider2D[] _colider;//1,滑ってるとき、2.立ってるとき
    void Start()
    {
        //インスタンスの生成
        _touch = new TouchManager();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _isMove = true;
    }
    private void Update()
    {
        PlayerMove();
    }
    /// <summary>プレイヤーの動き</summary>
    private void PlayerMove()
    {
        // タッチ状態更新
        this._touch.update();
        // タッチ取得
        TouchManager touch_state = this._touch.getTouch();

        //動きの制御
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
            //マウスクリックしたときの挙動
            if (_touch._touch_phase == TouchPhase.Moved)
            {
                this.transform.Translate(0, 0, 0);
                _isMove = false;
                Debug.Log("押しっぱなし");
                _anim.SetTrigger(_idleId);
            }
            if (_touch._touch_phase == TouchPhase.Ended)
            {
                _isMove = true;
                Debug.Log("離した");
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
        //岩にあたった時
        StoneManager _stone = collision.collider.GetComponent<StoneManager>();
        if (_stone != null)
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
