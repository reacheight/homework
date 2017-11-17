#include <string>

#include "splaytree.h"

using namespace std;

struct Node
{
    string key;
    string value;
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

string find(Tree* tree, string key)
{
    Node* current_pos = tree->root;
    while (current_pos && current_pos->key != key)
    {
        current_pos = (key > current_pos->key) ? current_pos->rightChild : current_pos->leftChild;
    }

    if (current_pos)
    {
        return current_pos->value;
    }

    return "";
}

void push(Tree* tree, string key, string value)
{
    Node* current_pos = tree->root;
    Node* parent = nullptr;
    while (current_pos)
    {
        parent = current_pos;
        current_pos = (key > current_pos->key) ? current_pos->rightChild : current_pos->leftChild;
    }

    Node* newNode = new Node{key, value, nullptr, nullptr};

    if (!parrent)
    {
        tree->root = newNode;
        return;
    }

    if (key > parent->key)
    {
        parent->rightChild = newNode;
    }
    else
    {
        parent->leftChild = newNode;
    }
}

void recursiveDeleteElement(Node*& node, string key)
{
    if (key > node->key)
    {
        recursiveDeleteElement(node->rightChild, key);
        return;
    }
    else
    {
        recursiveDeleteElement(node->leftChild, key);
        return;
    }

    if (!node->leftChild && !node->rightChild)
    {
        delete node;
        node = nullptr;
    }
//    else if (node->leftChild && node->rightChild)
//    {
//        Node* start = node->leftChild;
//        if (start->rightChild)
//        {
//            while (start->rightChild)
//            {
//                start = start->rightChild;
//            }
//        }
//    }
//    else if (!node->leftChild)
//    {
//        Node* newNode = node->rightChild;
//        delete node;
//        node = newNode;
//    }
//    else if(!node->rightChild)
//    {
//        Node*
//    }
}

void deleteElement(Tree* tree, string key)
{
    return;
}
