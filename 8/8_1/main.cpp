#include <iostream>

#include "аssociativeАrray.h"

using namespace std;

int main()
{
    Map* map = createMap();
    push(map, "keyM", "100");
    push(map, "keyF", "50");
    push(map, "keyT", "200");
    push(map, "keyH", "75");
    push(map, "keyB", "25");
    push(map, "keyW", "250");

    erase(map, "keyM");

    cout << find(map, "keyH") << endl;

    cout << isContained(map, "keyB") << " " << isContained(map, "keyM") << endl;

    deleteMap(map);

    return 0;
}

