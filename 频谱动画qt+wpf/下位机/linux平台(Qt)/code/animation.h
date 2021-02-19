#ifndef ANIMATION_H
#define ANIMATION_H
#include "widget.h"
#include <QPainter>
#include <QPaintDevice>
#define MINDMAX 20
#define BLASTMAX 3



typedef struct
{
    float x;//运动的思维点位横坐标
    float y;//运动的思维点位纵坐标
    float dirx;//运动的思维点位横坐标运动量
    float diry;//运动的思维点位纵坐标运动量
    float r;//
    QColor line_color;
    QColor color;//运动的思维点位颜色
}MTMOVMIND;

typedef struct
{
    float x;//运动的爆炸点位横坐标
    float y;//运动的爆炸点位纵坐标
    float size;//爆炸大小
    float r;
    QColor color;//爆炸点位颜色
}BLAST;

typedef enum
{
  BUSY=0x02U,
  IDLE=0x03U,
}STATUS;
//小球运动状态标记

class Animation
{
public:
    Animation();
    int FFT[64]={0};
    bool UDP_signal;

    void Motion_Mind(QPainter *painter, QColor color);
    void Motion_FFT(QPainter *painter, QColor color);
    /*爆炸*/
    void Motion_Blast(QPainter *painter,QColor color);

    void Motion_Init(int w, int h);
private:
    int width;
    int height;
    float  Motion_speed=0.1;//调节动画速度
    MTMOVMIND mtmovmind[MINDMAX];
    BLAST blast[BLASTMAX];
    STATUS MovMind(int Index);
    void Motion_MindInit();
    void Motion_BlastInit();
};

#endif // ANIMATION_H
