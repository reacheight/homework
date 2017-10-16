#pragma once

struct ListElement
{
    int value;
    ListElement* next;
};

struct List
{
    ListElement* sentinel = new ListElement {};
};

List* createList();

// Check if the list is empty.
bool isEmpty(List* list);

// Delete list.
void deleteList(List* list);

// Insert new value to the list.
void insert(List* list, int value);

// Delete value from the list.
void erase(List* list, int value);

// Print all elements form the list.
void printList(List* list);
