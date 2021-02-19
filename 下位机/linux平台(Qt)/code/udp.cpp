#include "widget.h"


UDP::UDP()
{


}
bool UDP::UDP_Init()
{
    uSocket=new QUdpSocket();
    uSocket->bind(2333,QUdpSocket::ShareAddress);//绑定本地UDP接收端口
    sig=true;//标记初始化完成
    return true;
}
void UDP::Datagram()
{
    if(sig!=true)
        return;
    while(uSocket->hasPendingDatagrams()) //拥有等待的数据报
    {
        QByteArray buffer; //拥于存放接收的数据报
        buffer.resize(uSocket->pendingDatagramSize());
        uSocket->readDatagram(buffer.data(),buffer.size());
        char *data= buffer.data();
        qDebug()<<data;
        char buffer_int[64][80];
        int cnt = split(buffer_int, data, ",");
        for(int i=0;i<cnt;i++)
        {
            mation.FFT[i]=3+atoi(buffer_int[i])*2;
        }
    }
}
/*处理接收的string分割成int数组*/
int UDP::split(char dst[][80], char* str, const char* spl)
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
