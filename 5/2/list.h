#pragma once

struct ListElement
{
    int value;
    ListElement* next;
};

struct List
{
    ListElement* head;
};

// Create empty list.
List* createList();

// Delete list.
void deleteList(List* list);

// Check if list is empty.
bool isEmpty(List* list);

// Insert to the head.
void insertFirst(List* list, int value);

// Insert to the body.
void insert(ListElement* prev, int value);

// Delete element from the list.
void erase(List* list, ListElement* prev);

// Return list' head.
ListElement* first(List* list);

// Return next element form the list.
ListElement* next(ListElement* element);

// Return value of element.
int value(ListElement* element);

// Initialize list with values from 1 to n. Head's value is n, then from 1 to n - 1;
void initializeList(List* list, int n);

// Return size of the list.
int listSize(List* list);
