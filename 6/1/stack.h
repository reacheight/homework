#pragma once

// Stack.
struct Stack;

// Create stack.
Stack* createStack();

// Push value at the top of the stack.
void push(Stack* stack, int value);

// Check is stack empty.
bool isEmpty(Stack* stack);

// Pop value from head and return it. If stack is empty return -1.
int pop(Stack* stack);

// Return top element of the stack, don't pop it. If stack is empty return -1.
int top(Stack* stack);

// Return size of the stack;
int size(Stack* stack);

// Delete stack.
void deleteStack(Stack*& stack);
