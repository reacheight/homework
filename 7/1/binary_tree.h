#pragma once

// Node of tree.
struct Node;

// Tree.
struct Tree;

// Create binary tree.
Tree* createTree();

// Insert value to the tree.
void insert(Tree* tree, int value);

// Remove value from the tree. Assertion error if value not in the tree or tree is empty.
void erase(Tree* tree, int value);

// Check if value in the tree.
bool isContained(Tree* tree, int value);

// Print all elements in the tree. If isDecreasing - print in decreasing order, else - in increasing order.
void printTree(Node* root, bool isDecreasing = false);

// Root of the tree.
Node* root(Tree* tree);

// Delete tree.
void deleteTree(Tree*& tree);

// Check if tree is empty.
bool isEmpty(Tree* tree);
