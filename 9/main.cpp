#include <iostream>
#include <fstream>
#include <clocale>

using namespace std;

#include "hashTable.h"

int main() {
    setlocale(LC_ALL, "Russian");

    ifstream in("input.txt");
    if (!in.is_open())
    {
        cout << "Не удалось открыть файл";
        return 1;
    }

    auto table = createHashTable();

    string tmp = "";
    while (in >> tmp)
    {
        addElement(table, tmp);
    }

    in.close();

    cout << "Таблица частоты слов:" << endl;
    printTable(table);
    deleteHashTable(table);

    return 0;
}