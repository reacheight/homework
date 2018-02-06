#pragma once

// Structure for the list.
struct List;


// Structure for the list's node.
struct ListElement;

// Function to create list.
List* createList();

// Function to delete list.
void deleteList(List*& list);

// Function to insert element to the list (into head).
void insert(List* list, const std::string& key, int value);

// Function to delete head of the list.
void eraseFirst(List* list);

// Function to delete element from the list next to previous.
void erase(ListElement* previous);

// Get head of the list.
ListElement* first(List* list);

// Get next element from the list.
ListElement* next(ListElement* element);

// Check if element is end.
bool isEnd(ListElement* element);

// Get value of the element (by reference).
int& getValue(ListElement* element);

// Get key of the element.
std::string getKey(ListElement* element);

// Get list's size.
int listSize(List* list);