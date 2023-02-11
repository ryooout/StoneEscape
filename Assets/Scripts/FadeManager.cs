using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class FadeManager : MonoBehaviour
{
    /// <summary>���l�̍ő�l </summary>
    const float MAX_��_VALUE = 1f;
    /// <summary>���l�̍ŏ��l </summary>
    const float MIN_��_VALUE = 0f;
    [SerializeField, Header("�t�F�[�h�C���ɂ����鎞��")]
    float _fadeInTime = 1.5f;
    [SerializeField, Header("�t�F�[�h�A�E�g�ɂ����鎞��")]
    float _fadeOutTime = 1.5f;
    [SerializeField]
    Image _fadeImage = default;
    void Start()
    {
        _fadeImage.gameObject.SetActive(true);
    }
    /// <summary>��ʂ����邭�Ȃ�</summary>
    public void FadeIn(Action action = null)
    {
        gameObject.SetActive(true);
        _fadeImage.color = new Color(0, 0, 0, MAX_��_VALUE);
        _fadeImage.DOFade(MIN_��_VALUE, _fadeInTime)
                  .OnComplete(() => action.Invoke());//fade���I��������ƂɈ����̒��̏��������s
    }
    /// <summary>��ʂ��Â��Ȃ�</summary>
    public void FadeOut(Action action = null)
    {
        gameObject.SetActive(true);
        _fadeImage.color = new Color(0, 0, 0, MIN_��_VALUE);
        _fadeImage.DOFade(MAX_��_VALUE, _fadeOutTime)
                  .OnComplete(() => action.Invoke());//fade���I��������ƂɈ����̒��̏��������s
    }
}
