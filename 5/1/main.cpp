#include <iostream>
#include <clocale>

#include "list.h"

using namespace std;

void printMenu()
{
    cout << "0 - выйти"                                    << endl <<
            "1 - добавить значение в сортированный список" << endl <<
            "2 - удалить значение из списка"               << endl <<
            "3 - распечатать список"                       << endl;
}

void insertValue(List* list)
{
    cout << "Введите значение, которое будет добавлено в список :" << endl;
    int value = 0;
    cin >> value;
    insert(list, value);
}

void eraseValue(List* list)
{
    if (isEmpty(list))
    {
        cout << "Список пуст, из него нельзя ничего удалить." << endl;
        return;
    }

    cout << "Введите значение, которое будет удалено из списка :" <<endl;
    int value = 0;
    cin >> value;
    erase(list, value);
}

int main()
{
    setlocale(LC_ALL, "Russian");

    List* list = createList();

    while (true)
    {
        printMenu();

        int command = -1;
        cin >> command;

        switch (command)
        {
            case 0 : deleteList(list);
                     return 0;

            case 1 : insertValue(list);
                     break;

            case 2 : eraseValue(list);
                     break;

            case 3 : printList(list);
                     break;
            default : cout << "Нет команды под таким номером." << endl;
        }
    }

    deleteList(list);

    return 0;
}
