#include <iostream>

#include "tree.h"

using namespace std;

int main()
{
    Tree* tree = createTree("(* (+ 1 1) 2)");

    printTree(tree);

    cout << calculate(tree) << endl;

    return 0;
}

