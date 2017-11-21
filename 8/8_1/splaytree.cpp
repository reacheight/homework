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

bool isContained(Tree* tree, string key)
{
    return find(tree, key) != "";
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

    if (!parent)
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
    else if (key < node->key)
    {
        recursiveDeleteElement(node->leftChild, key);
        return;
    }

    if (!node->leftChild && !node->rightChild)
    {
        delete node;
        node = nullptr;
    }
    else if (!node->leftChild)
    {
        Node* newNode = node->rightChild;
        delete node;
        node = newNode;
    }
    else if (!node->rightChild)
    {
        Node* newNode = node->leftChild;
        delete node;
        node = newNode;
    }
    else
    {
        Node* maxLeftParent = node->leftChild;

        if (!maxLeftParent->rightChild)
        {
            Node* newRightChild = node->rightChild;
            delete node;
            node = maxLeftParent;
            node->rightChild = newRightChild;

            return;
        }

        while (maxLeftParent->rightChild->rightChild)
        {
            maxLeftParent = maxLeftParent->rightChild;
        }

        node->key = maxLeftParent->rightChild->key;
        node->value = maxLeftParent->rightChild->value;

        recursiveDeleteElement(maxLeftParent->rightChild, maxLeftParent->rightChild->key);
    }
}

void deleteElement(Tree* tree, string key)
{
    if (isContained(tree, key))
    {
        recursiveDeleteElement(tree->root, key);
    }
}

void deleteTreeElements(Node* node)
{
    if (!node)
    {
        return;
    }

    deleteTreeElements(node->leftChild);
    deleteTreeElements(node->rightChild);
    delete node;
}

void deleteTree(Tree*& tree)
{
    deleteTreeElements(tree->root);

    delete tree;
    tree = nullptr;
}
