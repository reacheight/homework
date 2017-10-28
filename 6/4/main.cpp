#include <iostream>
#include <fstream>
#include <string>

#include "list.h"
#include "merge_sort.h"

using namespace std;

int main()
{
    ifstream in("database.txt");

    if (!in.is_open())
    {
        cout << "Не удалось открыть файл." << endl;
        return 0;
    }

    cout << "Отcортировать записи" << endl << "1 - по именам" << endl << "2 - по записям" << endl;
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
            ListElement* previous = sentinel(list);
            insert(previous, key, value);
            previous = next(previous);
        }
    }

    printList(list);
    printList(mergeSort(list));

    deleteList(list);

    return 0;
}

