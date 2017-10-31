#include <iostream>

#include "binary_tree.h"

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

void insertValue(Tree* tree)
{
    cout << "Введите значение, которое вы хотите добавить в множество:" << endl;
    int value = 0;
    cin >> value;

    insert(tree, value);
}

bool isTreeEmpty(Tree* tree)
{
    if (isEmpty(tree))
    {
        cout << "Множество пусто." << endl;
        return true;
    }

    return false;
}

void eraseValue(Tree* tree)
{
    if (!isTreeEmpty(tree))
    {
        cout << "Введите значение, которое вы хотите удалить из множества:" << endl;
        int value = 0;
        cin >> value;

        erase(tree, value);
    }
}

void isContainValue(Tree* tree)
{
    cout << "Введите значение, которое вы хотите проверить на пренадлежность множеству:" << endl;
    int value = 0;
    cin >> value;

    cout << "Данное значение " << (isContained(tree, value) ? "содержится " : "не содержится ") << "в множестве." << endl;
}

int main()
{
    Tree* tree = createTree();

    while (true)
    {
        printMenu();

        int command = -1;
        cin >> command;

        switch (command)
        {
            case 0 : deleteTree(tree);
                     return 0;

            case 1 : insertValue(tree);
                     break;

            case 2 : eraseValue(tree);
                     break;

            case 3 : isContainValue(tree);
                     break;

            case 4 : if (!isTreeEmpty(tree))
                     {
                        printTree(root(tree));
                        cout << endl;
                     }
                     break;

            case 5 : if (!isTreeEmpty(tree))
                     {
                        printTree(root(tree), true);
                        cout << endl;
                     }
                     break;

            default : cout << "Нет команды под таким номером." << endl;
        }
    }

    return 0;
}

