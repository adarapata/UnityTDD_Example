using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _duration;
    
    public event Action Tap;
    public event Action LongPress;
    private Coroutine _countDownCoroutine;

    private float _downTime;

    private void Awake()
    {
        LongPress += () => print("long!!!");
        Tap += () => print("tap!");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _downTime = Time.time;
        _animator.SetTrigger("Pressed");
        _countDownCoroutine = StartCoroutine(CountDownLongPress(_duration));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Tap?.Invoke();
        StopCoroutine(_countDownCoroutine);
        _animator.SetTrigger("Released");
    }

    private IEnumerator CountDownLongPress(float duration)
    {
        yield return new WaitForSeconds(duration);
        LongPress?.Invoke();
    }
}
