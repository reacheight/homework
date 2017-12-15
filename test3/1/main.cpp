#include <iostream>

#include "associativeArray.h"

using namespace std;

int main()
{
    Map* map = createMap();

    int tmp = -1;
    while (cin >> tmp)
    {
        if (tmp == 0)
        {
            break;
        }

        if (find(map, tmp) == 0)
        {
            push(map, tmp, 1);
        }
        else
        {
            push(map, tmp, find(map, tmp) + 1);
        }
    }

    printMap(map);

    deleteMap(map);

    return 0;
}

