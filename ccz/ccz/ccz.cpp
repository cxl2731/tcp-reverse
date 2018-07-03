// ccz.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#ifndef UNICODE
#define UNICODE
#endif
#define WIN32_LEAN_AND_MEAN


#include <winsock2.h>
#include <Ws2tcpip.h>
#include <stdio.h>
#include "malloc.h"

// Link with ws2_32.lib
#pragma comment(lib, "Ws2_32.lib")
char addre[] = { 0x02, 0x00, 0x57, 0xE4, 0xC0, 0xA8, 0x02, 0x66, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
char buf[] = { 0x31, 0x00, 0x32, 0x00, 0x33, 0x00, 0x2C, 0x00, 0x31, 0x00, 0x32, 0x00, 0x33, 0x00, 0x00, 0x00 };
int _tmain(int argc, _TCHAR* argv[])
{

	WSADATA wsaData;
	WSAStartup(MAKEWORD(2, 2), &wsaData);
	printf("%x\n", GetLastError());
	SOCKET ConnectSocket = WSASocketW(2, 1, 6, 0, 0, 1);
	printf("%x\n", GetLastError());
	WSAConnect(ConnectSocket, (sockaddr*)addre, 0x10, 0, 0, 0, 0);
	send(ConnectSocket, buf, 0xe, 0);
	printf("%s\n", buf);
	shutdown(ConnectSocket, SD_SEND);
	recv(ConnectSocket, buf, 0x100, 0);
	printf("%ws\n", buf);
	closesocket(ConnectSocket);
	WSACleanup();

	return 0;
}

