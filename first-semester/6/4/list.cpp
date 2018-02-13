#include <iostream>
#include <string>

#include "list.h"

using namespace std;

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
    ListElement* head;
    ListElement* tail;
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

void insert(List* list, string key, string value)
{
    ListElement* newTail = new ListElement{};
    newTail->note = {key, value};
    newTail->next = nullptr;

    if (!list->head)
    {
        list->head = newTail;
    }

    if (list->tail)
    {
        list->tail->next = newTail;
    }

    list->tail = newTail;
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

string getValue(ListElement* element)
{
    return element->note.value;
}

string getKey(ListElement* element)
{
    return element->note.key;
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

void printList(List *list)
{
    if (!list->head)
    {
        std::cout << "Список пуст." << std::endl;
        return;
    }

    ListElement* start = list->head;

    while (start != nullptr)
    {
        cout << start->note.key << " - " << start->note.value << endl;
        start = start->next;
    }

    cout << endl;
}

bool isEmpty(List* list)
{
    return !list->head;
}
