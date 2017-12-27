#include <iostream>
#include <vector>
#include <fstream>
#include <clocale>
#include <string>

using namespace std;

const string FILENAME = "state_table.txt";

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

vector<vector<int>> readStateTable(const string& filename)
{
    ifstream tableIn(filename);

    int stringsCount = 0;
    int rowsCount = 0;
    tableIn >> stringsCount >> rowsCount;

    vector<vector<int>> stateTable(stringsCount, vector<int>(rowsCount));
    for (auto& string : stateTable)
    {
        for (auto& element : string)
        {
            tableIn >> element;
        }
    }
    tableIn.close();

    return stateTable;
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

    vector<vector<int>> stateTable = readStateTable(FILENAME);

    int curState = 0;
    int prevState = 0;

    bool isReady = false;
    string comment = "";

    char c = '0';
    while (in.get(c))
    {
        prevState = curState;
        curState = stateTable[curState][code(c)];

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

    in.close();

    return 0;
}