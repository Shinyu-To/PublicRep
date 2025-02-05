//--------gameName:JUST SHOOT!--------//
PImage Player;
PImage Enemy;
PImage Shoot;
float sp = 7;//一回キーボードを押すplayerの移動距離
Player myPlayer;
Enemy [] enemys = new Enemy [5];//敵のオブジェクトの配列
ArrayList<Shoot> shoots= new ArrayList<Shoot>();//名前はshootsのshootの配列リスト
int firespeed = 8;//火力
int playerLife = 10;//Playerのlife
int count = 0;
int score = 0;
int bestScore = 0;//一番いい点数
boolean gameOver = false;//ゲームオーバー
boolean gameBegin = false;

boolean [] keys = new boolean [128];

//--------------------setup--------------------//
void setup() {
  size(1280, 720);//ウインドウのサイズ
  Player = loadImage("player.png");
  Enemy = loadImage("enemy.png");
  Shoot = loadImage("shoot.png");
  Player.resize(75, 75);
  Enemy.resize(75, 75);
  Shoot.resize(20, 70);
  myPlayer = new Player();
  for (int i = 0; i<enemys.length; i++) {
    enemys[i] = new Enemy(random(width/3, width - width/3), random(-100, -50));
  }
}

//--------------------draw--------------------//
void draw() {
  //スタート画面やゲームオーバー
  if (gameBegin == false)
  {
    showBegin();
  }
  if (keyPressed && key == 'r' && gameBegin ==false)
  {
    gameBegin = true;
  }
  if (keyPressed && key == 'r'&& gameOver)
  {
    restart();
  }
  if (playerLife<=0)
  {
    showOver();
  }
  if (gameOver || gameBegin==false)
  {
    return;
  }
  //背景
  rectMode(CENTER);
  noStroke();
  fill(255, 120);
  rect(width/2, height/2, width, height);

  //-----------point
  textAlign(LEFT);
  textSize(20);
  fill(#41A4D1);
  text("Score:"+score, 0.05 * width, 0.05 *  height);
  text("Life:"+ playerLife, 0.05*width, 0.1*height);

  //-----------playerの表示、操作
  myPlayer.display();
  move();

  //-----------敵の再生、アクション
  for (int i = 0; i<enemys.length; i++) {
    enemys[i].display();
    enemys[i].update();
    enemys[i].hit();
    if (enemys[i].reach() ) {
      playerLife -= 1;//life-1
      enemys[i] = new Enemy((int)random(width/3, width - width/3), -50);
    }
    if (enemys[i].die() ) {//もし敵倒されたら
      score ++;
      enemys[i] = new Enemy((int)random(width/3, width - width/3), -50);
      //その敵を初期化する、範囲はrandom(width/3,width-width/3),-50。intはrandomの浮動小数点数を整数に変化する
    }
  }

  //-----------shoot弾幕（２列）
  if (count%firespeed == 0) {//もしcountがfirespeedを割った余りは0なら
    Shoot shoot1 = new Shoot(myPlayer.x-20, myPlayer.y - 70);
    Shoot shoot2 = new Shoot(myPlayer.x+20, myPlayer.y - 70);
    shoots.add(shoot1);
    shoots.add(shoot2);
  }
  count += 1;
  for (int i = 0; i< shoots.size(); i++) {
    Shoot S = (Shoot)shoots.get(i);
    S.display();
    if (S.check()) {
      shoots.remove(i);
    }
  }
}


//--------------------Player--------------------//
class Player {
  color c;
  float x;
  float y;
  Player() {
    c = color(0, 0, 255, 80);
    x = width/2;
    y = height-100;
  }
  void display() {
    imageMode(CENTER);
    image(Player, x, y);
    //Playerの行動範囲を設定
    if (x < width/3 ) {
      x = width/3;
    } else if (x > width - width/3) {
      x = width - width/3;
    } else if (y > height) {
      y = height;
    }
    if (y < 0) {
      y = 0;
    }
  }
}

void move() {
  if (keys['a'])
    myPlayer.x -= sp;
  if (keys['d'])
    myPlayer.x += sp;
  if (keys['w'])
    myPlayer.y -= sp;
  if (keys['s'])
    myPlayer.y += sp;
}

void keyPressed() {
  keys[key] = true;
}
void keyReleased() {
  keys[key] = false;
}

//--------------------Shoot--------------------//
class Shoot {
  float x;
  float y;
  float speed = 10;
  Shoot(float x, float y) {
    this.x = x;
    this.y = y;
  }
  void display() {
    imageMode(CENTER);
    image(Shoot,x,y);
    y -= speed;
  }
  boolean check() {
    if (y < 0) {
      return true;
    } else
      return false;
  }
}

//--------------------Enemy--------------------//
class Enemy {
  float x;
  float y;
  float velocity=random(1, 2);//速度
  float accelerate=0.01;//加速度
  int life;
  Enemy(float x, float y) {
    this.x = x;
    this.y = y;
    life = (int)random(3, 8);//(int)はrandomの浮動小数点数を整数に変化する
  }
  void display() {
    imageMode(CENTER);
    tint(255, 40+20*life);
    image(Enemy, x, y);
  }
  void update() {//敵の移動、更新
    y += velocity;//ｙ=y+velocity
    velocity += accelerate;//velocity=velocity+accelerate
  }
  //敵が防御線を越えたか、倒された
  boolean die() {
    if (life <= 0) {
      return true;
    }
    return false;
  }
  //防御線を越えたかどうか
  boolean reach() {//もし防御線を越えたら，boolean reachが先に行う（lifeを1減らすために先に行う）
    if (y > height+25)//もし敵の位置がheight+25を超えたら
    {
      return true;
    }
    return false;
  }
  //---------------Hit---------------//
  void hit() {
    for (int i = 0; i < shoots.size(); i++) {//弾幕との距離を比べる
      Shoot shoot = (Shoot) shoots.get(i);
      if (dist(x, y, shoot.x, shoot.y) < 50) {//弾幕と敵の距離が5より小さいなら
        life -= 1;//敵のlifeが1減る
        shoots.remove(i);
      }
    }
  }
}

//--------------------スタート画面＆ゲームオーバー--------------------//
void showOver()
{
  gameOver = true;
  for (int i = 0; i< shoots.size(); i++)
  {
    shoots.remove(i);
  }
  bestScore = bestScore > score ? bestScore: score ;//
  background(0);
  textAlign(CENTER);
  fill(255);
  textSize(42);
  text("BestScore:" + bestScore, width/2, height/2-100);
  text("Score:" + score, width/2, height/2);
  textSize(20);
  text("Press R to restart", width/2, height/2+100);
}
void restart() {
  gameOver = false;
  score = 0;
  playerLife = 10;
  for (int i = 0; i<enemys.length; i++) {
    enemys[i] = new Enemy(random(width/3, width - width/3), random(-100, -50));
  }
}
void showBegin()
{
  background(0);
  textAlign(CENTER);
  fill(255);
  textSize(35);
  text("JUST SHOOT!", width/2, height/2-50);
  textSize(20);
  text("use 'wasd' to move", width/2, height/2+50);
  text("Press R to start", width/2, height/2+100);
}
