#include "widget.h"
#include "ui_widget.h"
#include "animation.h"
#include <QHostInfo>
#include <QtNetwork/QHostAddress>
#include <QtNetwork/QNetworkInterface>

Animation mation;
Widget::Widget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::Widget)
{
    ui->setupUi(this);
    this->setStyleSheet("background-color: rgb(0,0,0)");
    this->showFullScreen();
    this->setCursor(QCursor(Qt::BlankCursor));
    uSocket = new QUdpSocket(this);
    uSocket->bind(2333,QUdpSocket::ShareAddress);//绑定本地UDP接收端口
    connect(uSocket,SIGNAL(readyRead()),this,SLOT(processPendingDatagram()));//新建有个事件
    mation.Motion_Init(608,480);//初始化屏幕分辨率可以自行调节
    startTimer(1);

}
/*string分割int数组*/
int Widget::split(char dst[][80], char* str, const char* spl)
{
    int n = 0;
    char *result = NULL;
    result = strtok(str, spl);
    while( result != NULL )
    {
        strcpy(dst[n++], result);
        result = strtok(NULL, spl);
    }
    return n;
}
void Widget::processPendingDatagram() //处理等待的数据报
{
    while(uSocket->hasPendingDatagrams()) //拥有等待的数据报
    {
        QByteArray buffer; //拥于存放接收的数据报
        buffer.resize(uSocket->pendingDatagramSize());
        uSocket->readDatagram(buffer.data(),buffer.size());
        char *data= buffer.data();
        char buffer_int[64][80];
        int cnt = split(buffer_int, data, ",");
        for(int i=0;i<cnt;i++)
        {
            mation.FFT[i]=3+atoi(buffer_int[i])*2;
        }
    }
}



void Widget::timerEvent(QTimerEvent *)
{
    repaint();
}
void Widget::paintEvent(QPaintEvent *)
{
    QPainter p(this);
    main_color.setHsv(h,255,255);
    mation.Motion_Mind(&p,main_color);
    mation.Motion_Blast(&p,main_color);
    mation.Motion_FFT(&p,main_color);

    if(sig_color==true)
    {
        h+=0.05;
    }
    else{
        h-=0.05;
    }

    if(h==255)
    {
        sig_color=false;
    }else if(h==0)
    {
        sig_color=true;
    }


}


Widget::~Widget()
{
    delete ui;
}
