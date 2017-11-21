#pragma once

// Structure of the associative array.
struct Map;

// Function to create associative array.
Map* createMap();

// Function to push element with key and value in the associative array.
void push(Map* map, std::string key, std::string value);

// Function to find value by key.
std::string find(Map* map, std::string key);

// Function to check if element with key contained in the associative array.
bool isContained(Map* map, std::string key);

// Function to remove element with key.
void erase(Map* map, std::string key);

// Function to delete associative array.
void deleteMap(Map*& map);
