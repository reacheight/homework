#include <iostream>
#include <vector>
#include <fstream>
#include <clocale>

using namespace std;

int code(char c)
{
    if (c == '/')
    {
        return 1;
    }

    if (c == '*')
    {
        return 2;
    }

    return 0;
}

int main()
{
    setlocale(LC_ALL, "Russian");

    ifstream in("input.txt");
    if (!in.is_open())
    {
        cout << "Не удалось открыть файл." << endl;
        return 1;
    }
//                                 s   /   *
    vector<vector<int>> matrix = {{0,  1,  0},
                                  {0,  1,  2},
                                  {2,  2,  3},
                                  {2,  1,  3}};

    int curState = 0;
    int prevState = 0;

    bool isReady = false;
    string comment = "";

    char c = '0';
    while (in.get(c))
    {
        prevState = curState;
        curState = matrix[curState][code(c)];

        if (prevState == 1 && curState == 2)
        {
            comment += "/*";
        }
        else if (curState == 2 || curState == 3)
        {
            comment += c;
        }
        else if (prevState == 3 && curState == 1)
        {
            isReady = true;
            comment += "/";
        }

        if (isReady)
        {
            cout << comment << endl;
            comment = "";
            isReady = false;
        }
    }

    return 0;
}