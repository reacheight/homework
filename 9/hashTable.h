#pragma once

struct HashTable;

struct HashElement;

HashTable* createHashTable();

void addElement(HashTable* table, const std::string key);

void printTable(HashTable* table);

void deleteHashTable(HashTable*& table);