#pragma once

// Node of set.
struct Node;

// Set.
struct Set;

// Create binary set.
Set* createSet();

// Insert value to the set.
void insert(Set* set, int value);

// Remove value from the set. Assertion error if value not in the set or set is empty.
void erase(Set* set, int value);

// Check if value in the set.
bool isContained(Set* set, int value);

// Print all elements in the set. If isDecreasing - print in decreasing order, else - in increasing order.
void printSet(Node* root, bool isDecreasing = false);

// Root of the set.
Node* root(Set* set);

// Delete set.
void deleteSet(Set*& set);

// Check if set is empty.
bool isEmpty(Set* set);
