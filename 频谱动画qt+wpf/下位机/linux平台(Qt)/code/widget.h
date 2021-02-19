#ifndef WIDGET_H
#define WIDGET_H

#include <QWidget>
#include <QTimerEvent>
#include <QPaintEvent>
#include <QColor>
#include <QScreen>
#include <QObject>
#include <QtGui>
#include <QUdpSocket>
#include <stdio.h>
#include <stdlib.h>
#include <QPainter>
#include <QtWidgets/QApplication>
#include <QtWidgets/QPushButton>
#include <QDebug>
#include <QPaintDevice>
#include <QVariant>
#include <QRect>
#include <QByteArray>
#include <QMouseEvent>

namespace Ui {
class Widget;
}

class Widget : public QWidget
{
    Q_OBJECT
public:

    explicit Widget(QWidget *parent = 0);
    ~Widget();

    int split(char dst[][80], char* str, const char* spl);
    void timerEvent(QTimerEvent*);
    void paintEvent(QPaintEvent *);
    QByteArray buffer;
public slots:
    void processPendingDatagram();
private:
    QColor main_color;
    qreal  h=0;
    bool sig_color=true;
    QUdpSocket *uSocket;
    Ui::Widget *ui;
};

#endif // WIDGET_H
