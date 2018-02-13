#include <iostream>
#include <clocale>

#include "set.h"

using namespace std;

void printMenu()
{
    cout << "0 - выйти" << endl <<
            "1 - добавить элемент в множество" << endl <<
            "2 - удалить элемент из множества" << endl <<
            "3 - проверить, содержится ли элемент в множестве" << endl <<
            "4 - напечатать элементы множества в порядке возрастания" << endl <<
            "5 - напечатать элементы множества в порядке убывания" << endl;
}

void insertValue(Set* set)
{
    cout << "Введите значение, которое вы хотите добавить в множество:" << endl;
    int value = 0;
    cin >> value;

    insert(set, value);
}

bool isSetEmpty(Set* set)
{
    if (!isEmpty(set))
    {
        return false;
    }

    cout << "Множество пусто." << endl;
    return true;

}

void eraseValue(Set* set)
{
    if (isSetEmpty(set))
    {
        return;
    }

    cout << "Введите значение, которое вы хотите удалить из множества:" << endl;
    int value = 0;
    cin >> value;

    if (!isContained(set, value))
    {
        cout << "Данного элемента нет в множестве" << endl;
        return;
    }

    erase(set, value);
}

void isContainValue(Set* set)
{
    cout << "Введите значение, которое вы хотите проверить на принадлежность множеству:" << endl;
    int value = 0;
    cin >> value;

    cout << "Данное значение " << (isContained(set, value) ? "содержится " : "не содержится ") << "в множестве." << endl;
}

int main()
{
    setlocale(LC_ALL, "Russian");

    Set* set = createSet();

    while (true)
    {
        printMenu();

        int command = -1;
        cin >> command;

        switch (command)
        {
            case 0 : deleteSet(set);
                     return 0;

            case 1 : insertValue(set);
                     break;

            case 2 : eraseValue(set);
                     break;

            case 3 : isContainValue(set);
                     break;

            case 4 : if (!isSetEmpty(set))
                     {
                        printSet(root(set));
                        cout << endl;
                     }
                     break;

            case 5 : if (!isSetEmpty(set))
                     {
                        printSet(root(set), true);
                        cout << endl;
                     }
                     break;

            default : cout << "Нет команды под таким номером." << endl;
        }
    }

    return 0;
}

