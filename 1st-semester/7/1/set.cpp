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

void getParentAndPos(Node*& child, Node*& parent, int value, bool isInsert=true)
{
    while ((isInsert && child) || (!isInsert && child->value != value))
    {
        parent = child;
        child = (child->value > value) ? child->leftChild : child->rightChild;
    }
}


void insert(Set* set, int value)
{
    if (isContained(set, value))
    {
        return;
    }

    Node* start = set->root;
    Node* parent = nullptr;
    getParentAndPos(start, parent, value);

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

bool isList(Node* node)
{
    return !node->leftChild && !node->rightChild;
}

void erase(Set* set, int value)
{
    if (!isContained(set, value) || isEmpty(set))
    {
        return;
    }

    Node* position = set->root;
    Node* parent = nullptr;
    getParentAndPos(position, parent, value, false);

    if (!position->leftChild && !position->rightChild)
    {
        if (!parent)
        {
            set->root = nullptr;
            return;
        }

        if (position->value > parent->value)
        {
            parent->rightChild = nullptr;
        }
        else
        {
            parent->leftChild = nullptr;
        }

        delete position;
    }
    else if (position->leftChild && position->rightChild)
    {
        Node* start = position->rightChild;
        Node* startParent = position;
        while (start->leftChild)
        {
            startParent = start;
            start = start->leftChild;
        }

        position->value = start->value;

        if (startParent == position)
        {
            if (position == set->root)
            {
                set->root->rightChild = start->rightChild;
            }
            else
            {
                position->rightChild = start->rightChild;
            }
        }
        else
        {
            startParent->leftChild = start->rightChild;
        }

        delete start;
    }
    else
    {
        if (position->leftChild)
        {
            if (parent == nullptr)
            {
                set->root = position->leftChild;
                return;
            }

            if (position->value > parent->value)
            {
                parent->rightChild = position->leftChild;
            }
            else
            {
                parent->leftChild = position->leftChild;
            }
        }
        else
        {
            if (parent == nullptr)
            {
                set->root = position->rightChild;
                return;
            }

            if (position->value > parent->value)
            {
                parent->rightChild = position->rightChild;
            }
            else
            {
                parent->leftChild = position->rightChild;
            }
        }

        delete position;
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
    delete set;
    set = nullptr;
}

bool isEmpty(Set* set)
{
    return !set->root;
}
