#pragma once

// Structure of the tree.
struct Tree;

// Function to create tree.
Tree* createTree();

void push(Tree* tree, std::string key, std::string value);

std::string find(Tree* tree, std::string key);

bool isContained(Tree* tree, std::string key);

void deleteElement(Tree* tree, std::string key);

void deleteTree(Tree*& tree);
