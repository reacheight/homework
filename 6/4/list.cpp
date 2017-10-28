#include <iostream>
#include <string>

#include "list.h"

struct Note
{
    std::string key;
    std::string value;
};

struct ListElement
{
    Note note;
    ListElement* next;
};

struct List
{
    ListElement* sentinel = new ListElement {};
};

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
    list = nullptr;
}

bool isEmpty(List *list)
{
    return list->sentinel->next == nullptr;
}

ListElement* sentinel(List* list)
{
    return list->sentinel;
}

ListElement* first(List* list)
{
    return list->sentinel->next;
}

ListElement* next(ListElement* previous)
{
    return previous->next;
}

bool isEnd(ListElement* element)
{
    return element == nullptr;
}

std::string key(ListElement* element)
{
    return element->note.key;
}

std::string value(ListElement* element)
{
    return element->note.value;
}

void insert(ListElement* previous, std::string key, std::string value)
{
    ListElement* newElement = new ListElement;
    newElement->note = {key, value};
    newElement->next = previous->next;
    previous->next = newElement;
}

void printList(List *list)
{
    if (isEmpty(list))
    {
        std::cout << "Список пуст." << std::endl;
        return ;
    }

    ListElement* start = list->sentinel->next;

    while (start != nullptr)
    {
        std::cout << start->note.key << " - " << start->note.value << std::endl;
        start = start->next;
    }

    std::cout << std::endl;
}

int size(List *list)
{
    int size = 0;
    ListElement* start = list->sentinel;
    while (start->next != nullptr)
    {
        ++size;
        start = start->next;
    }

    return size;
}

