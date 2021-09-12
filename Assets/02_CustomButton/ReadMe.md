﻿# 課題：カスタムボタンを作ろう

通常のボタンの機能だけだと足りない部分があったので、自前で実装することになりました。ざっくりと欲しい要望は以下のとおりです。

- ボタンを押して、離したら「タップ(Tap)」と判定する
- ボタンを一定時間押し続けると「長押し(LongPress)」と判定する

## 実装した部分

- ボタンを押して離したらTapイベントが発行される
- 任意の秒数押し続けたらLongPressイベントが発行される
- ボタンを押した瞬間にPressアニメーションが再生される
- ボタンを離した瞬間にReleaseアニメーションが再生される

## もっと細かく必要だった

しかし、いざ使ってみると細かいところで色々機能が足りていないことに気づきました。特にアニメーション周りが足りていないような気がしており、ごちゃごちゃになったり、押した状態で動き回ると奇妙な挙動を起こします。
プランナーとすり合わせたところ、もう少し細かいところまで決めることができました。

- ボタンを押して離したらTapイベントが発行される
- ボタンを押した後ボタンから指をずらした状態で離すとTapイベントは発行されない
- ボタンを押した後ボタンから指をずらして再び戻ってきた状態で離すとTapイベントは発行される
- LongPressイベントが発行されたあとに指を離してもTapイベントは発行されない
- 任意の秒数押し続けたらLongPressイベントが発行される
- 任意の秒数押し続けていない場合LongPressイベントは発行されない
- 長押し中に指をずらした場合LongPressイベントは発行されない
- ボタンを押した瞬間にPressアニメーションが再生される
- ボタンを離した瞬間にReleaseアニメーションが再生される
- ボタンを押した後ボタンから指をずらした場合Releaseアニメーションが再生される
- ボタンを押した後ボタンから指をずらして再び戻ってきた場合Pressedアニメーションが再生される
- ボタン以外を押した状態で指をずらした場合Pressedアニメーションが再生されない
- ボタン以外を押した状態で指をずらした場合Releasedアニメーションが再生されない
- 長押しに成功したときReleaseアニメーションが再生される
- 長押しに成功してReleaseアニメーションが再生された場合指を離すまでアニメーションが再生されない

これらが問題なく動作するように、テストを書きながら実装してみましょう。