using NUnit.Framework;

namespace Tests
{
    public class CallCounter
    {
        public int Count { get; private set; } = 0;

        public void OnCallCount() => Count++;
    }
    
    public class CustomButtonModelTest
    {
        class Tapイベント
        {
            private CustomButtonModel _buttonModel;
            private CallCounter _counter;
            
            [SetUp]
            public void SetUp()
            {
                _buttonModel = new CustomButtonModel(1F);
                _counter = new CallCounter();
                _buttonModel.Tapped += _counter.OnCallCount;
            }
            
            [Test]
            public void ボタンを押して離したらTapイベントが発行される()
            {
                _buttonModel.Down(0F);
                _buttonModel.Up();
            
                Assert.That(_counter.Count, Is.EqualTo(1));
            }

            [Test]
            public void ボタンを押した後ボタンから指をずらした状態で離すとTapイベントは発行されない()
            {
                _buttonModel.Down(0F);
                _buttonModel.Exit();
                _buttonModel.Up();

                Assert.That(_counter.Count, Is.Zero);
            }
        
            [Test]
            public void ボタンを押した後ボタンから指をずらして再び戻ってきた状態で離すとTapイベントは発行される()
            {
                _buttonModel.Down(0F);
                _buttonModel.Exit();
                _buttonModel.Enter();
                _buttonModel.Up();

                Assert.That(_counter.Count, Is.EqualTo(1));
            }
        
            [Test]
            public void LongPressイベントが発行されたあとに指を離してもTapイベントは発行されない()
            {
                _buttonModel.Down(1F);
                _buttonModel.LongPress(2F);
                _buttonModel.Up();
            
                Assert.That(_counter.Count, Is.Zero);
            }
        }

        class LongPressイベント
        {
            private CustomButtonModel _buttonModel;
            private CallCounter _counter;
            
            [SetUp]
            public void SetUp()
            {
                _buttonModel = new CustomButtonModel(1F);
                _counter = new CallCounter();
                _buttonModel.LongPressed += _counter.OnCallCount;
            }
            
            [Test]
            public void ボタンを押し続けたらLongPressイベントが発行される()
            {
                _buttonModel.Down(1F);
                _buttonModel.LongPress(2F);
            
                Assert.That(_counter.Count, Is.EqualTo(1));
            }

            [Test]
            public void 任意の秒数押し続けていない場合LongPressイベントは発行されない()
            {
                _buttonModel.Down(1F);
                _buttonModel.LongPress(1.5F);
            
                Assert.That(_counter.Count, Is.Zero);
            }

            [Test]
            public void 長押し中に指をずらした場合LongPressイベントは発行されない()
            {
                _buttonModel.Down(1F);
                _buttonModel.Exit();
                _buttonModel.LongPress(2F);

                Assert.That(_counter.Count, Is.Zero);
            }
        }

        class Animationイベント
        {
            private CustomButtonModel _buttonModel;
            private CallCounter _counter;
            
            [SetUp]
            public void SetUp()
            {
                _buttonModel = new CustomButtonModel(1F);
                _counter = new CallCounter();
            }

            [Test]
            public void ボタンを押した瞬間にPressアニメーションイベントが発行される()
            {
                _buttonModel.Pressed += _counter.OnCallCount;
                
                _buttonModel.Down(0F);
                
                Assert.That(_counter.Count, Is.EqualTo(1));
            }
            
            [Test]
            public void ボタンを離した瞬間にReleaseアニメーションイベントが発行される()
            {
                _buttonModel.Released += _counter.OnCallCount;
                
                _buttonModel.Down(0F);
                _buttonModel.Up();
                
                Assert.That(_counter.Count, Is.EqualTo(1));
            }

            [Test]
            public void 長押しに成功したときReleaseアニメーションイベントが発行される()
            {
                _buttonModel.Released += _counter.OnCallCount;
                
                _buttonModel.Down(0F);
                _buttonModel.LongPress(2F);
                
                Assert.That(_counter.Count, Is.EqualTo(1));                
            }

            [Test]
            public void ボタンを押した後ボタンから指をずらした場合Releaseアニメーションイベントが発行される()
            {
                _buttonModel.Released += _counter.OnCallCount;
                
                _buttonModel.Down(0F);
                _buttonModel.Exit();
                
                Assert.That(_counter.Count, Is.EqualTo(1));
            }

            [Test]
            public void ボタンを押した後ボタンから指をずらして再び戻ってきた場合Pressedアニメーションイベントは発行される()
            {
                _buttonModel.Pressed += _counter.OnCallCount;
                
                _buttonModel.Down(0);
                _buttonModel.Exit();
                _buttonModel.Enter();
                
                Assert.That(_counter.Count, Is.EqualTo(2));
            }

            [Test]
            public void 長押しに成功してReleaseアニメーションイベントが発行された場合指を離すまでアニメーションイベントが発行されない()
            {
                _buttonModel.Pressed += _counter.OnCallCount;
                
                _buttonModel.Down(0F);
                _buttonModel.LongPress(1F);
                _buttonModel.Exit();
                _buttonModel.Enter();
                
                Assert.That(_counter.Count, Is.EqualTo(1));
            }

            [Test]
            public void ボタン以外を押した状態で指をずらした場合Pressedイベントは発行されない()
            {
                _buttonModel.Pressed += _counter.OnCallCount;
                
                _buttonModel.Enter();
                
                Assert.That(_counter.Count, Is.Zero);
            }
            
            [Test]
            public void ボタン以外を押した状態で指をずらした場合Releasedイベントは発行されない()
            {
                _buttonModel.Released += _counter.OnCallCount;
                
                _buttonModel.Enter();
                _buttonModel.Exit();
                
                Assert.That(_counter.Count, Is.Zero);
            }
        }
    }
}