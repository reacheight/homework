#pragma once

// Structure for hash-table.
struct HashTable;

// Function to create hash-table.
HashTable* createHashTable();

// Function to add element in the table.
void addElement(HashTable* table, const std::string key);

// Function to print elements from the table.
void printTable(HashTable* table);

// Function to delete hash-table.
void deleteHashTable(HashTable*& table);

// Function to calculate maximal and middle size of segments.
std::pair<int, int> maxAndMidSizeOfSegment(HashTable* table);

// Function to calculate fill coefficient.
double fillCoeff(HashTable* table);