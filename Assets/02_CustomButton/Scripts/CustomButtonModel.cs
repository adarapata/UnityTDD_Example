
    using System;

    public class CustomButtonModel
    {
        public event Action Tapped;
        public event Action LongPressed;
        
        public event Action Pressed;
        public event Action Released;

        private readonly float _longPressDuration;

        private bool _isEnter;
        private bool _isDown;
        private bool _isLongPressSuccess;
        private float _buttonDownAt;

        public CustomButtonModel(float longPressLongPressDuration)
        {
            _longPressDuration = longPressLongPressDuration;
        }
        
        public void Down(float downTime)
        {
            _isEnter = true;
            _isDown = true;
            _buttonDownAt = downTime;
            Pressed?.Invoke();
        }

        public void Up()
        {
            if (_isEnter && _isDown)
            {
                Tapped?.Invoke();
                Released?.Invoke();
            }

            _isDown = false;
            _isLongPressSuccess = false;
        }

        public void LongPress(float longPressTime)
        {
            if (IsValidLongPressTime(longPressTime) && _isEnter)
            {
                LongPressed?.Invoke();
                Released?.Invoke();
                _isLongPressSuccess = true;
            }

            _isDown = false;
        }

        public void Exit()
        {
            _isEnter = false;
            if (!_isLongPressSuccess && _isDown)
            {
                Released?.Invoke();
            }
        }

        public void Enter()
        {
            _isEnter = true;
            if (!_isLongPressSuccess && _isDown)
            {
                Pressed?.Invoke();
            }
        }

        private bool IsValidLongPressTime(float longPressTime) => longPressTime - _buttonDownAt >= _longPressDuration;
    }
