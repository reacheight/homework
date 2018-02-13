#include <iostream>
#include <fstream>
#include <string>

#include "tree.h"

using namespace std;

int main()
{
    ifstream in("file.txt");
    if (!in.is_open())
    {
        cout << "Не удалось открыть файл" << endl;
        return -1;
    }

    string query = "";
    getline(in, query);
    in.close();

    Tree* tree = createTree(query);

    printTree(tree);

    cout << calculate(tree) << endl;

    deleteTree(tree);

    return 0;
}

