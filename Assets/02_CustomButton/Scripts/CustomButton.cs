using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _duration;
    
    public event Action Tap;
    public event Action LongPress;
    private Coroutine _countDownCoroutine;

    private void Awake()
    {
        LongPress += () => print("long!!!");
        Tap += () => print("tap!");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _animator.SetTrigger("Pressed");
        _countDownCoroutine = StartCoroutine(CountDownLongPress(_duration));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Tap?.Invoke();
        if (_countDownCoroutine != null)
        {
            StopCoroutine(_countDownCoroutine);
        }

        _animator.SetTrigger("Released");
    }

    private IEnumerator CountDownLongPress(float duration)
    {
        yield return new WaitForSeconds(duration);
        LongPress?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_countDownCoroutine != null)
        {
            StopCoroutine(_countDownCoroutine);
        }
        
        _animator.SetTrigger("Released");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetTrigger("Pressed");
    }
}
