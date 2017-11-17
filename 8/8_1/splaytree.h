#pragma once

// Structure of the tree.
struct Tree;

// Function to create tree.
Tree* createTree();

void push(Tree* tree, string key, string value);

string find(Tree* tree, string key);

void deleteElement(Tree* tree, string key);
