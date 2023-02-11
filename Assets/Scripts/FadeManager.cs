using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class FadeManager : MonoBehaviour
{
    /// <summary>α値の最大値 </summary>
    const float MAX_α_VALUE = 1f;
    /// <summary>α値の最小値 </summary>
    const float MIN_α_VALUE = 0f;
    [SerializeField, Header("フェードインにかかる時間")]
    float _fadeInTime = 1.5f;
    [SerializeField, Header("フェードアウトにかかる時間")]
    float _fadeOutTime = 1.5f;
    [SerializeField]
    Image _fadeImage = default;
    void Start()
    {
        _fadeImage.gameObject.SetActive(true);
    }
    /// <summary>画面が明るくなる</summary>
    public void FadeIn(Action action = null)
    {
        gameObject.SetActive(true);
        _fadeImage.color = new Color(0, 0, 0, MAX_α_VALUE);
        _fadeImage.DOFade(MIN_α_VALUE, _fadeInTime)
                  .OnComplete(() => action.Invoke());//fadeし終わったあとに引数の中の処理を実行
    }
    /// <summary>画面が暗くなる</summary>
    public void FadeOut(Action action = null)
    {
        gameObject.SetActive(true);
        _fadeImage.color = new Color(0, 0, 0, MIN_α_VALUE);
        _fadeImage.DOFade(MAX_α_VALUE, _fadeOutTime)
                  .OnComplete(() => action.Invoke());//fadeし終わったあとに引数の中の処理を実行
    }
}
