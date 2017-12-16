#pragma once

struct List;

struct ListElement;

List* createList();

void deleteList(List*& list);

void insert(List* list, std::string key, int value);

void eraseFirst(List* list);

void erase(ListElement* previous);

ListElement* first(List* list);

ListElement* next(ListElement* element);

bool isEnd(ListElement* element);

int& getValue(ListElement* element);

std::string getKey(ListElement* element);

int listSize(List* list);