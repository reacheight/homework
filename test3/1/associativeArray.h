#pragma once

// Structure of the associative array.
struct Map;

// Function to create associative array.
Map* createMap();

// Function to push element with key and value in the associative array.
void push(Map* map, int key, int value);

// Function to find value by key.
int find(Map* map, int key);

// Function to check if element with key contained in the associative array.
bool isContained(Map* map, int key);

// Function to remove element with key.
void erase(Map* map, int key);

// Function to delete associative array.
void deleteMap(Map*& map);

void printMap(Map* map);
