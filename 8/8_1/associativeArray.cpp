#include <string>

#include "associativeArray.h"

using namespace std;

struct Node
{
    string key;
    string value;
    Node* leftChild;
    Node* rightChild;
    Node* parent;
};

struct Map
{
    Node* root{};
};

Map* createMap()
{
    return new Map{};
}

Node* getNode(Map* map, string key)
{
    Node* node = map->root;

    while (node && node->key != key)
    {
        node = (key > node->key) ? node->rightChild : node->leftChild;
    }

    return node;
}

void setNode(Node* node, Node* parent, Node* value)
{
    if (!parent)
    {
        return;
    }

    if (parent->leftChild == node)
    {
        parent->leftChild = value;
    }
    else
    {
        parent->rightChild = value;
    }
}

void setRootOrNode(Map* map, Node* node, Node* parent, Node* value)
{
    if (!parent)
    {
        map->root = value;
    }
    else
    {
        setNode(node, parent, value);
    }
}

Node* findMax(Node* node)
{
    while (node->rightChild)
    {
        node = node->rightChild;
    }

    return node;
}

void setParent(Node* child, Node* parent)
{
    if (child)
    {
        child->parent = parent;
    }
}

void keepParent(Node* node)
{
    setParent(node->leftChild, node);
    setParent(node->rightChild, node);
}

void rotate(Node* parent, Node* node)
{
    Node* gparent = parent->parent;

    if (gparent)
    {
        setNode(parent, gparent, node);
    }

    if (parent->leftChild == node)
    {
        parent->leftChild = node->rightChild;
        node->rightChild = parent;
    }
    else
    {
        parent->rightChild = node->leftChild;
        node->leftChild = parent;
    }

    keepParent(node);
    keepParent(parent);
    node->parent = gparent;
}

void splay(Map* map, Node* node)
{
    if (!node->parent)
    {
        map->root = node;
        return;
    }

    Node* parent = node->parent;
    Node* gparent = parent->parent;

    if (!gparent)
    {
        rotate(parent, node);
        map->root = node;
        return;
    }

    bool zigzig = (gparent->leftChild == parent) == (parent->leftChild == node);

    if (zigzig)
    {
        rotate(gparent, parent);
        rotate(parent, node);
    }
    else
    {
        rotate(parent, node);
        rotate(gparent, node);
    }

    splay(map, node);
}

string find(Map* map, string key)
{
    Node* current_pos = getNode(map, key);

    if (current_pos)
    {
        splay(map, current_pos);
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
    if (isContained(map, key))
    {
        auto node = getNode(map, key);
        node->value = value;
        return;
    }

    Node* currentPos = map->root;
    Node* parent = nullptr;
    while (currentPos)
    {
        parent = currentPos;
        currentPos = (key > currentPos->key) ? currentPos->rightChild : currentPos->leftChild;
    }

    Node* newNode = new Node{key, value, nullptr, nullptr, parent};

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

    splay(map, newNode);
}

void erase(Map* map, string key)
{
    if (!isContained(map, key))
    {
        return;
    }

    Node* node = getNode(map, key);
    Node* parent = node->parent;

    if (!node->leftChild && !node->rightChild)
    {
        setRootOrNode(map, node, parent, nullptr);
        delete node;
    }
    else if (!node->rightChild)
    {
        setRootOrNode(map, node, parent, node->leftChild);
        node->leftChild->parent = parent;
        delete node;
    }
    else if (!node->leftChild)
    {
        setRootOrNode(map, node, parent, node->rightChild);
        node->rightChild->parent = parent;
        delete node;
    }
    else
    {
        Node* max = findMax(node->leftChild);

        string newKey = max->key;
        string newValue = max->value;

        erase(map, max->key);

        node->key = newKey;
        node->value = newValue;
    }

    if (parent)
    {
        splay(map, parent);
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
