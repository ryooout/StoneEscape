using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    bool _isStart;
    public bool IsStart { get => _isStart; set => _isStart = value; }
    private void Awake()
    {
        _isStart = false;
    }
    private void Start()
    {
        StartCoroutine(CountDown());
    }
    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1.0f);

        yield return new WaitForSeconds(1.0f);

        yield return new WaitForSeconds(1.0f);

        yield return new WaitForSeconds(1.0f);
        _isStart = true;
    }
}
