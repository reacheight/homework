#include <string>

#include "list.h"

void extend(ListElement* previous, ListElement* start)
{
    while (!isEnd(start))
    {
        insert(previous, key(start), value(start));
        previous = next(previous);
        start = next(start);
    }
}

List* merge(List* a, List* b)
{
    List* newList = createList();
    ListElement* previous = sentinel(newList);

    ListElement* i = first(a);
    ListElement* j = first(b);
    while (!isEnd(i) && !isEnd(j))
    {
        if (key(i) <= key(j))
        {
            insert(previous, key(i), value(i));
            i = next(i);
        }
        else
        {
            insert(previous, key(j), value(j));
            j = next(j);
        }
        previous = next(previous);
    }
    extend(previous, i);
    extend(previous, j);

    return newList;
}

List* mergeSort(List* list)
{
    if (size(list) > 1)
    {
        List* firstHalf = createList();
        List* secondHalf = createList();
        ListElement* firstPrev = sentinel(firstHalf);
        ListElement* secondPrev = sentinel(secondHalf);
        ListElement* start = first(list);
        for (int i = 0; i < size(list)/2; ++i)
        {
            insert(firstPrev, key(start), value(start));
            start = next(start);
        }
        for (int i = 0; i < size(list) - size(list)/2; ++i)
        {
            insert(secondPrev, key(start), value(start));
            start = next(start);
        }
        firstHalf = mergeSort(firstHalf);
        secondHalf = mergeSort(secondHalf);

        return merge(firstHalf, secondHalf);
    }
    return list;
}
