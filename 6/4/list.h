#pragma once

// List structure.
struct List;

// Create empty list.
List* createList();

// Check if the list is empty.
bool isEmpty(List* list);

// Delete list.
void deleteList(List* list);

// Insert new note to the list.
void insert(List* list, std::string key, std::string value);

// Print all notes from the list.
void printList(List* list);

// Size of list.
int size(List* list);
