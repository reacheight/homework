#include <iostream>
#include <fstream>
#include <string>
#include <locale>

#include "list.h"
#include "merge_sort.h"

using namespace std;

int main()
{
    setlocale(LC_ALL, "Russian");

    ifstream in("database.txt");

    if (!in.is_open())
    {
        cout << "Не удалось открыть файл." << endl;
        return 1;
    }

    cout << "Отcортировать записи" << endl << "1 - по именам" << endl << "2 - по номерам" << endl;
    int type = 0;
    while (type != 1 && type != 2)
    {
        cout << "Введите 1 или 2: " << endl;
        cin >> type;
    }

    List* list = createList();

    while (!in.eof())
    {
        string name = "name";
        string number = "number";
        char sep = '-';
        in >> name >> sep >> number;

        string key = "key";
        string value = "value";

        if (type == 1)
        {
            key = name;
            value = number;
        }
        else
        {
            key = number;
            value = name;
        }

        if (!in.eof())
        {
            insert(list, key, value);
        }
    }

    cout << "Отсортированный список:" << endl;
    mergeSort(list);
    printList(list);

    deleteList(list);

    return 0;
}

