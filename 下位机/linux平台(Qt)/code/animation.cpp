#include "animation.h"
#include <QPen>

Animation::Animation()
{

}

void Animation::Motion_MindInit()
{
    for(int i=0;i<MINDMAX;i++)
    {

        mtmovmind[i].line_color.setRgb(rand()%255,rand()%255,rand()%255);
        mtmovmind[i].color.setRgb(rand()%255,rand()%255,rand()%255);

        mtmovmind[i].x = rand()%(width-4)+4;
        mtmovmind[i].y = rand()%(height);

        mtmovmind[i].dirx = (rand()%30-15)*Motion_speed;
        mtmovmind[i].diry = (rand()%30-15)*Motion_speed;
        if(mtmovmind[i].dirx==0)
        {
            mtmovmind[i].dirx = 0.5;
        }
        if(mtmovmind[i].diry==0)
        {
            mtmovmind[i].diry = 0.5;
        }
    }
}

void Animation::Motion_BlastInit()
{
    for(int i=0;i<BLASTMAX;i++)
    {
        blast[i].x=rand()%width;
        blast[i].y=rand()%height;
        blast[i].size=5+rand()%15;
        blast[i].r=0;
        blast[i].color.setRgb(rand()%255,rand()%255,rand()%255);
    }
}

STATUS  Animation::MovMind(int Index)
{

    if(mtmovmind[Index].x<=3)
    {
        mtmovmind[Index].x = 4;
        return IDLE;
    }
    else if(mtmovmind[Index].x>=width-2)
    {
        mtmovmind[Index].x = width-3;
        return IDLE;
    }
    else if(mtmovmind[Index].y<=0)
    {
        mtmovmind[Index].y = 1;
        return IDLE;
    }
    else if(mtmovmind[Index].y>=height-2)
    {
        mtmovmind[Index].y = height-3;
        return IDLE;
    }
    else
    {
        mtmovmind[Index].x += mtmovmind[Index].dirx;
        mtmovmind[Index].y += mtmovmind[Index].diry;
    }
  return BUSY;
}


void Animation::Motion_Mind(QPainter *painter,QColor color)
{
    int i,j;
    for(i=0;i<MINDMAX;i++)
    {
        if(MovMind(i) == IDLE)
        {
            mtmovmind[i].color.setRgb(rand()%255,rand()%255,rand()%255);
            mtmovmind[i].dirx = (rand()%30-15)*Motion_speed;
            mtmovmind[i].diry = (rand()%30-15)*Motion_speed;
            if(mtmovmind[i].dirx==0)
            {
                mtmovmind[i].dirx = 0.5;
            }
            if(mtmovmind[i].diry==0)
            {
                mtmovmind[i].diry = 0.5;
            }
        }
    }
    for(i=0;i<MINDMAX;i++)
    {
            for(j=0;j<MINDMAX;j++)
            {

                if((mtmovmind[i].x-mtmovmind[j].x)*(mtmovmind[i].x-mtmovmind[j].x)+(mtmovmind[i].y-mtmovmind[j].y)*(mtmovmind[i].y-mtmovmind[j].y)<6000)
                {
                    color.setAlpha(50);
                    QPen pen(color);
                    pen.setWidth(2);
                    painter->setBrush(pen.brush());
                    painter->setPen(pen);
                    painter->setRenderHint(QPainter::SmoothPixmapTransform);
                    painter->drawLine(mtmovmind[i].x,mtmovmind[i].y,mtmovmind[j].x,mtmovmind[j].y);
                }
            }
    }
    for(i=0;i<MINDMAX;i++)
    {
        mtmovmind[i].color.setAlpha(255);
        QPen pen(mtmovmind[i].color);
        painter->setBrush(pen.brush());
        painter->setPen(pen);
        painter->setRenderHint(QPainter::SmoothPixmapTransform);
        painter->drawEllipse(QPoint(mtmovmind[i].x,mtmovmind[i].y),5.5,5.5);
    }


}

void Animation::Motion_Blast(QPainter *painter,QColor color)
{
   int i;
   color.setAlpha(70);
   for(i=0;i<BLASTMAX;i++)
   {
           QPen pen(blast[i].color);
            if(blast[i].size-blast[i].r>0)
            {
               blast[i].r+=0.8;
            }
            else {
                blast[i].x=rand()%width;
                blast[i].y=rand()%height;
                blast[i].size=15+rand()%50;
                blast[i].r=0;
                blast[i].color.setRgb(rand()%255,rand()%255,rand()%255);
            }
            painter->setPen(pen);
            painter->setBrush(pen.brush());
            painter->setRenderHint(QPainter::SmoothPixmapTransform);
            painter->drawEllipse(blast[i].x+5+blast[i].r,blast[i].y+5+blast[i].r,blast[i].size-blast[i].r,blast[i].size-blast[i].r);
            pen.setColor(color);
            painter->setPen(color);
            painter->setBrush(QBrush(QColor(0,0,0,0)));
            painter->setRenderHint(QPainter::SmoothPixmapTransform);
            painter->drawEllipse(blast[i].x,blast[i].y,10+blast[i].size+blast[i].r,10+blast[i].size+blast[i].r);
   }
}

void Animation::Motion_FFT(QPainter *painter, QColor color)
{
    int x = 0;
    color.setAlpha(120);
    QPen pen(color);
    painter->setPen(pen);
    painter->setBrush(pen.brush());
    for(int i=0;i<39;i++)
    {
        x=i<<4;
        painter->drawRect(x,height-FFT[i],10,FFT[i]);
    }
}

void Animation::Motion_Init(int w, int h)
{
    width=w;
    height=h;
    Motion_MindInit();
    Motion_BlastInit();
}
