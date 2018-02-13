#include <string>

#include "list.h"

void extend(List* list, ListElement* start)
{
    while (!isEnd(start))
    {
        insert(list, getKey(start), getValue(start));
        start = next(start);
    }
}

List* merge(List* listA, List* listB)
{
    List* newList = createList();

    ListElement* i = first(listA);
    ListElement* j = first(listB);
    while (!isEnd(i) && !isEnd(j))
    {
        if (getKey(i) <= getKey(j))
        {
            insert(newList, getKey(i), getValue(i));
            i = next(i);
        }
        else
        {
            insert(newList, getKey(j), getValue(j));
            j = next(j);
        }
    }
    extend(newList, i);
    extend(newList, j);

    deleteList(listA);
    deleteList(listB);

    return newList;
}

void mergeSort(List*& list)
{
    auto size = listSize(list);

    if (size <= 1)
    {
        return;
    }

    List* firstHalf = createList();
    List* secondHalf = createList();
    ListElement* start = first(list);
    for (int i = 0; i < size; ++i)
    {
        if (i < size/2)
        {
            insert(firstHalf, getKey(start), getValue(start));
        }
        else
        {
            insert(secondHalf, getKey(start), getValue(start));
        }

        start = next(start);
    }

    mergeSort(firstHalf);
    mergeSort(secondHalf);

    deleteList(list);

    list = merge(firstHalf, secondHalf);
}
