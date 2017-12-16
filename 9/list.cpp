#include <iostream>
#include <string>

#include "list.h"

using namespace std;

struct ListElement
{
    std::string key;
    int value;
    ListElement* next;
};

struct List
{
    ListElement* head;
};

List* createList()
{
    return new List{};
}

void deleteList(List*& list)
{
    while (list->head != nullptr)
    {
        ListElement* newHead = list->head->next;
        delete list->head;
        list->head = newHead;
    }

    delete list;
    list = nullptr;
}

void insert(List* list, string key, int value)
{
    ListElement *newHead = new ListElement{};
    newHead->key = key;
    newHead->value = value;
    newHead->next = nullptr;

    if (list->head)
    {
        newHead->next = list->head->next;
    }

    list->head = newHead;
}

void eraseFirst(List* list)
{
    ListElement* newHead = list->head->next;
    delete list->head;
    list->head = newHead;
}

void erase(ListElement* previous)
{
    ListElement* element = previous->next;
    previous->next = element->next;
    delete element;
}

ListElement* first(List* list)
{
    return list->head;
}

ListElement* next(ListElement* element)
{
    return element->next;
}

bool isEnd(ListElement* element)
{
    return element == nullptr;
}

int& getValue(ListElement* element)
{
    return element->value;
}

string getKey(ListElement* element)
{
    return element->key;
}

int listSize(List* list)
{
    int size = 0;

    ListElement* start = list->head;
    while (start)
    {
        ++size;
        start = start->next;
    }

    return size;
}
