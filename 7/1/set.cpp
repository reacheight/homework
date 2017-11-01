#include <iostream>
#include <cassert>

#include "set.h"

struct Node
{
    int value;
    Node* leftChild;
    Node* rightChild;
};

struct Set
{
    Node* root{};
};

Set* createSet()
{
    return new Set{};
}

bool isContained(Set* set, int value)
{
    Node* position = set->root;
    while (position && position->value != value)
    {
        (position->value > value) ? position = position->leftChild : position = position->rightChild;
    }

    return position;
}

void insert(Set* set, int value)
{
    if (!isContained(set, value))
    {
        Node* start = set->root;
        Node* parent = nullptr;
        while (start)
        {
            parent = start;
            start = (start->value > value) ? start->leftChild : start->rightChild;
        }

        Node* newNode = new Node{};
        newNode->value = value;

        if (!parent)
        {
            set->root = newNode;
        }
        else
        {
            if (parent->value > value)
            {
                parent->leftChild = newNode;
            }
            else
            {
                parent->rightChild = newNode;
            }
        }
    }
}

void erase(Set* set, int value)
{
    assert(isContained(set, value) && !isEmpty(set));

    Node* position = set->root;
    Node* parent = nullptr;
    while (position->value != value)
    {
        parent = position;
        position = (position->value > value) ? position->leftChild : position->rightChild;
    }

    if (!position->leftChild && !position->rightChild)
    {
        if (!parent)
        {
            delete position;
            set->root = nullptr;
        }
        else
        {
            if (position->value > parent->value)
            {
                delete position;
                parent->rightChild = nullptr;
            }
            else
            {
                delete position;
                parent->leftChild = nullptr;
            }
        }
    }
    else if (position->leftChild && position->rightChild)
    {
        Node* start = position->rightChild;
        while (start->leftChild)
        {
            start = start->leftChild;
        }

        if (!start->leftChild)
        {
            position->value = start->value;
            position->rightChild = start->rightChild;
            delete start;
        }
        else
        {
            position->value = start->leftChild->value;
            delete start->leftChild;
            start->leftChild = nullptr;
        }
    }
    else
    {
        if (position->leftChild)
        {
            position->value = position->leftChild->value;
            delete position->leftChild;
            position->leftChild = nullptr;
        }
        else
        {
            position->value = position->rightChild->value;
            delete position->rightChild;
            position->rightChild = nullptr;
        }
    }
}

void printSet(Node* root, bool isDecreasing)
{
    if (root)
    {
        isDecreasing ? printSet(root->rightChild, isDecreasing) : printSet(root->leftChild, isDecreasing);
        std::cout << root->value << " ";
        isDecreasing ? printSet(root->leftChild, isDecreasing) : printSet(root->rightChild, isDecreasing);
    }
}

Node* root(Set* set)
{
    return set->root;
}

void deleteSetNodes(Node* root)
{
    if (root)
    {
        deleteSetNodes(root->leftChild);
        deleteSetNodes(root->rightChild);
        delete root;
    }
}

void deleteSet(Set*& set)
{
    deleteSetNodes(set->root);
    set = nullptr;
}

bool isEmpty(Set* set)
{
    return !set->root;
}
