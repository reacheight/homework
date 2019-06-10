#include <iostream>
#include <clocale>

#include "list.h"

using namespace std;

int main()
{
    setlocale(LC_ALL, "Russian");

    List* list = createList();

    int n = 0;
    int m = 0;
    cout << "Введите два числа: n и m" << endl;
    cin >> n >> m;

    initializeList(list, n);

    ListElement* start = first(list);
    while (listSize(list) != 1)
    {
        for (int i = 1; i < m; ++i)
        {
            start = next(start);
        }
        erase(list, start);
    }

    cout << "Последним останется воин с номером " << value(first(list)) << endl;

    deleteList(list);

    return 0;
}

