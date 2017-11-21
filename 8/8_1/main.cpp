#include <iostream>
#include <string>

#include "splaytree.h"

using namespace std;

int main()
{
    Tree* map = createTree();
    push(map, "keyM", "100");
    push(map, "keyF", "50");
    push(map, "keyT", "200");
    push(map, "keyH", "75");
    push(map, "keyB", "25");
    push(map, "keyW", "250");

    deleteElement(map, "keyM");

    cout << isContained(map, "keyB") << " " << isContained(map, "keyM") << endl;

    deleteTree(map);

    return 0;
}

