#include <iostream>

#include "list.h"

List* createList()
{
    return new List{};
}

void deleteList(List *list)
{
    while (list->sentinel->next != nullptr)
    {
        ListElement* newHead = list->sentinel->next->next;
        delete list->sentinel->next;
        list->sentinel->next = newHead;
    }
    delete list->sentinel;
    delete list;
}

bool isEmpty(List *list)
{
    return list->sentinel->next == nullptr;
}

void insert(List* list, int value)
{
    ListElement* newElement = new ListElement;
    newElement->value = value;

    ListElement* previous = list->sentinel;
    while (previous->next != nullptr && value >= previous->next->value)
    {
        previous = previous->next;
    }

    newElement->next = previous->next;
    previous->next = newElement;
}

void erase(List* list, int value)
{
    ListElement* previous = list->sentinel;
    while (previous->next->value != value)
    {
        previous = previous->next;
    }

    ListElement* tmp = previous->next;
    previous->next = tmp->next;
    delete tmp;
}

void printList(List *list)
{
    ListElement* start = list->sentinel->next;

    while (start != nullptr)
    {
        std::cout << start->value << " ";
        start = start->next;
    }

    std::cout << std::endl;
}

