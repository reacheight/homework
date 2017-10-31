#include <iostream>
#include <cassert>

#include "binary_tree.h"

struct Node
{
    int value;
    Node* leftChild;
    Node* rightChild;
};

struct Tree
{
    Node* root{};
};

Tree* createTree()
{
    return new Tree{};
}

bool isContained(Tree* tree, int value)
{
    Node* position = tree->root;
    while (position != nullptr && position->value != value)
    {
        (position->value > value) ? position = position->leftChild : position = position->rightChild;
    }

    return position;
}

void insert(Tree* tree, int value)
{
    if (!isContained(tree, value))
    {
        Node* start = tree->root;
        Node* parent = nullptr;
        while (start != nullptr)
        {
            parent = start;
            start = (start->value > value) ? start->leftChild : start->rightChild;
        }

        Node* newNode = new Node{};
        newNode->value = value;

        if (parent == nullptr)
        {
            tree->root = newNode;
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

void erase(Tree* tree, int value)
{
    assert(isContained(tree, value) && !isEmpty(tree));

    Node* position = tree->root;
    Node* parent = nullptr;
    while (position->value != value)
    {
        parent = position;
        position = (position->value > value) ? position->leftChild : position->rightChild;
    }

    if (parent == nullptr)
    {
        tree->root = nullptr;
    }
    else
    {
        if (parent->value > position->value)
        {
            parent->leftChild = nullptr;
        }
        else
        {
            parent->rightChild = nullptr;
        }
    }

    delete position;
}

void printTree(Node* root, bool isDecreasing)
{
    if (root != nullptr)
    {
        isDecreasing ? printTree(root->rightChild, isDecreasing) : printTree(root->leftChild, isDecreasing);
        std::cout << root->value << " ";
        isDecreasing ? printTree(root->leftChild, isDecreasing) : printTree(root->rightChild, isDecreasing);
    }
}

Node* root(Tree* tree)
{
    return tree->root;
}

void deleteTreeNodes(Node* root)
{
    if (root != nullptr)
    {
        deleteTreeNodes(root->leftChild);
        deleteTreeNodes(root->rightChild);
        delete root;
    }
}

void deleteTree(Tree*& tree)
{
    deleteTreeNodes(tree->root);
    tree = nullptr;
}

bool isEmpty(Tree* tree)
{
    return tree->root == nullptr;
}
