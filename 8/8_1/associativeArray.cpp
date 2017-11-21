#include <string>

#include "associativeArray.h"

using namespace std;

struct Node
{
    string key;
    string value;
    Node* leftChild;
    Node* rightChild;
};

struct Map
{
    Node* root{};
};

Map* createMap()
{
    return new Map{};
}

string find(Map* map, string key)
{
    Node* current_pos = map->root;
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

bool isContained(Map* map, string key)
{
    return find(map, key) != "";
}

void push(Map* map, string key, string value)
{
    Node* current_pos = map->root;
    Node* parent = nullptr;
    while (current_pos)
    {
        parent = current_pos;
        current_pos = (key > current_pos->key) ? current_pos->rightChild : current_pos->leftChild;
    }

    Node* newNode = new Node{key, value, nullptr, nullptr};

    if (!parent)
    {
        map->root = newNode;
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

void erase(Map* map, string key)
{
    if (isContained(map, key))
    {
        recursiveDeleteElement(map->root, key);
    }
}

void deleteMapElements(Node* node)
{
    if (!node)
    {
        return;
    }

    deleteMapElements(node->leftChild);
    deleteMapElements(node->rightChild);
    delete node;
}

void deleteMap(Map*& map)
{
    deleteMapElements(map->root);

    delete map;
    map = nullptr;
}
