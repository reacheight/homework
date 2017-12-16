cmake_minimum_required(VERSION 3.7)
project(9)

set(CMAKE_CXX_STANDARD 11)

set(SOURCE_FILES main.cpp list.cpp hashTable.cpp list.h hashTable.h)
add_executable(9 ${SOURCE_FILES})