#include <iostream>

#include "list.h"

List* createList()
{
    return new List{};
}

void deleteList(List* list)
{
    while (list->head != list->head->next)
    {
        ListElement* newHead = list->head->next;

        ListElement* last = newHead;
        while (last->next != list->head)
        {
            last = last->next;
        }

        delete list->head;
        list->head = newHead;
        last->next = newHead;
    }
    delete list->head;
    delete list;
}

bool isEmpty(List* list)
{
    return list->head == nullptr;
}

void insert(ListElement* prev, int value)
{
    ListElement* newElement = new ListElement;
    newElement->value = value;
    newElement->next = prev->next;
    prev->next = newElement;
}

void insertFirst(List* list, int value)
{
    ListElement* newHead = new ListElement;
    newHead->value = value;

    if (isEmpty(list))
    {
        newHead->next = newHead;
    }
    else {
        newHead->next = list->head;

        ListElement* last = list->head;
        while (last->next != list->head)
        {
            last = last->next;
        }

        last->next = newHead;
    }

    list->head = newHead;
}

ListElement* first(List* list)
{
    return list->head;
}

ListElement* next(ListElement* element)
{
    return element->next;
}

int value(ListElement* element)
{
    return element->value;
}

void erase(List* list, ListElement* prev)
{
    if (prev->next == list->head)
    {
        ListElement* newHead = prev->next->next;
        delete prev->next;
        prev->next = newHead;
        list->head = prev;
        return ;
    }
    ListElement* newNext = prev->next->next;
    delete prev->next;
    prev->next = newNext;
}

void initializeList(List* list, int n)
{
    insertFirst(list, n);
    ListElement* previous = list->head;
    for (int i = 1; i < n; ++i)
    {
        insert(previous, i);
        previous = previous->next;
    }
}

int listSize(List* list)
{
    if (isEmpty(list))
    {
        return 0;
    }

    int size = 1;

    ListElement* start = list->head;
    while (start->next != list->head)
    {
        ++size;
        start = start->next;
    }

    return size;
}

void printList(List* list)
{
    std::cout << list->head->value << " ";

    ListElement* start = list->head->next;
    while (start != list->head)
    {
        std::cout << start->value << " ";
        start = start->next;
    }
}
