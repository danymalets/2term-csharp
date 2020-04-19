#pragma once

#include <iostream>
#include <vector> 
using namespace std;

extern "C" __declspec(dllexport) void _cdecl Init(int size);

extern "C" __declspec(dllexport) void _stdcall MakeMove(int move);

extern "C" __declspec(dllexport) int _stdcall GetMove();

pair<int, int> solve(int depth, int maxDepth);

int getMaxDepth();

int getEmptyCnt();

bool isWin(int symbol);

bool isEnd();