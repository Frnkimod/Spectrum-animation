#ifndef UDP_H
#define UDP_H
#include <QUdpSocket>
#include <QString>
#include "animation.h"

class UDP:Widget
{
public:
    UDP();

    bool UDP_Init();
public slots:
    void Datagram();
private:

    bool sig=false;
    int split(char dst[][80], char* str, const char* spl);
};

#endif // UDP_H
