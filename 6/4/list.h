#pragma once

// Element of the list.
struct ListElement;

// List structure.
struct List;

// Create empty list.
List* createList();

// Check if the list is empty.
bool isEmpty(List* list);

// Delete list.
void deleteList(List* list);

// Sentinel of the list.
ListElement* sentinel(List* list);

// First element from the list.
ListElement* first(List* list);

// Element next to previous.
ListElement* next(ListElement* previous);

// Key of the element.
std::string key(ListElement* element);

// Value of the element.
std::string value(ListElement* element);

// Check if element is end of the list.
bool isEnd(ListElement* element);

// Insert new note to the list.
void insert(ListElement* previous, std::string key, std::string value);

// Print all notes from the list.
void printList(List* list);

// Size of list.
int size(List* list);
