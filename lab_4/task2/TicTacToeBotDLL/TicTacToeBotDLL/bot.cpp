#include "pch.h"
#include "bot.h"

const int MAXSIZE = 9;

int Size;
int Field[MAXSIZE][MAXSIZE];

void _cdecl Init(int size) {
	Size = size;
	for (int i = 0; i < Size; i++)
		for (int j = 0; j < Size; j++)
			Field[i][j] = -1;
}

void _stdcall MakeMove(int move) {
	int i = move / Size;
	int j = move % Size;
	Field[i][j] = 1;
}

int _stdcall GetMove() {
	int move = solve(0, getMaxDepth()).second;
    int i = move / Size;
    int j = move % Size;
    Field[i][j] = 0;
    return move;
}

int getMaxDepth() {
    int maxDepth = 0;
    int tmp = 1;
    int cnt = getEmptyCnt();
    while (tmp * cnt < 100000 && cnt > 0) {
        tmp *= cnt;
        cnt--;
        maxDepth++;
    }
    return maxDepth;
}

int getEmptyCnt() {
    int cnt = 0;
    for (int i = 0; i < Size; i++) {
        for (int j = 0; j < Size; j++) {
            if (Field[i][j] == -1) cnt++;
        }
    }
    return cnt;
}

pair<int, int> solve(int depth, int maxDepth) {
    if (isWin(0)) return { 1, -1 };
    if (isWin(1)) return { -1, -1 };
    if (depth == maxDepth || isEnd()) return { 0, -1 };
    int bestValue;
    vector<int> bestMoves;
    if (depth % 2 == 0) {
        bestValue = -2;
        for (int i = 0; i < Size; i++) {
            for (int j = 0; j < Size; j++) {
                if (Field[i][j] == -1) {
                    Field[i][j] = 0;
                    int t_value = solve(depth + 1, maxDepth).first;
                    Field[i][j] = -1;
                    if (t_value > bestValue) {
                        bestMoves.clear();
                        bestValue = t_value;
                        bestMoves.push_back(i * Size + j);
                    }
                    else if (t_value == bestValue) {
                        bestMoves.push_back(i * Size + j);
                    }
                }
            }
        }
    }
    else {
        bestValue = 2;
        for (int i = 0; i < Size; i++) {
            for (int j = 0; j < Size; j++) {
                if (Field[i][j] == -1) {
                    Field[i][j] = 1;
                    int t_value = solve(depth + 1, maxDepth).first;
                    Field[i][j] = -1;
                    if (t_value < bestValue) {
                        bestMoves.clear();
                        bestValue = t_value;
                        bestMoves.push_back(i * Size + j);
                    }
                    else if (t_value == bestValue) {
                        bestMoves.push_back(i * Size + j);
                    }
                }
            }
        }
    }
    return { bestValue, bestMoves[rand() % bestMoves.size()] };
}

bool isWin(int symbol)
{
    bool ok;
    for (int i = 0; i < Size; i++)
    {
        ok = true;
        for (int j = 0; j < Size; j++)
        {
            if (Field[i][j] != symbol) ok = false;
        }
        if (ok) return true;
    }
    for (int j = 0; j < Size; j++)
    {
        ok = true;
        for (int i = 0; i < Size; i++)
        {
            if (Field[i][j] != symbol) ok = false;
        }
        if (ok) return true;
    }
    ok = true;
    for (int i = 0, j = 0; i < Size; i++, j++)
    {
        if (Field[i][j] != symbol) ok = false;
    }
    if (ok) return true;
    ok = true;
    for (int i = 0, j = Size - 1; i < Size; i++, j--)
    {
        if (Field[i][j] != symbol) ok = false;
    }
    if (ok) return true;
    return false;
}

bool isEnd()
{
    return getEmptyCnt() == 0;
}