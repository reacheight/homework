#include <vector>
#include <string>
#include <iostream>

#include "list.h"
#include "hashTable.h"

using namespace std;

const int SIZE = 1000;

struct HashTable
{
    List* array[SIZE];
};

struct HashElement
{
    ListElement* element;
};

HashTable* createHashTable()
{
    auto newHashTable = new HashTable{};
    for (int i = 0; i < SIZE; ++i)
    {
        newHashTable->array[i] = createList();
    }
    return newHashTable;
}

int hashFunc(string str)
{
    int result = 0;
    int p = 31;
    for (char ch : str)
    {
        result =result * p + (int) ch;
    }

    return result;
}

void addElement(HashTable* table, const string key)
{
    int h = hashFunc(key);

    for (auto i = first(table->array[h % SIZE]); !isEnd(i); i = next(i))
    {
        if (getKey(i) == key)
        {
            getValue(i)++;
            return;
        }
    }
    insert(table->array[h % SIZE], key, 1);
}

void printTable(HashTable* table)
{
    for (int i = 0; i < SIZE; ++i)
    {
        for (auto j = first(table->array[i]); !isEnd(j); j = next(j))
        {
            cout << getKey(j) << " - " << getValue(j) << endl;
        }
    }
}

void deleteHashTable(HashTable*& table)
{
    for (int i = 0; i < SIZE; ++i)
    {
        deleteList(table->array[i]);
    }
    delete table;
    table = nullptr;
}