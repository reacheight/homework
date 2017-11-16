#pragma once

// Structure of the tree.
struct Tree;

// Create tree and initilize it with arithmetic expression.
Tree* createTree(std::string query);

// Print tree in the prefix form.
void printTree(Tree* tree);

// Calculate value of arithmetic expression.
int calculate(Tree* tree);

// Delete tree.
void deleteTree(Tree*& tree);
