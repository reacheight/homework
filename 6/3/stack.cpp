#include <iostream>

#include "stack.h"

struct StackElement
{
    char value;
    StackElement* next;
};

struct Stack
{
    StackElement* head;
};

Stack* createStack()
{
    return new Stack{};
}

void push(Stack *stack, char value)
{
    StackElement* newHead = new StackElement;
    newHead->value = value;
    newHead->next = stack->head;
    stack->head = newHead;
}

bool isEmpty(Stack* stack)
{
    return stack->head == nullptr;
}

char pop(Stack* stack)
{
    if (!isEmpty(stack))
    {
        char value = stack->head->value;
        StackElement* next = stack->head->next;
        delete stack->head;
        stack->head = next;

        return value;
    }

    std::cout << "Stack is empty, pop returns -1.\n";
    return -1;
}

char top(Stack *stack)
{
    if (!isEmpty(stack))
    {
        return stack->head->value;
    }

    std::cout << "Stack is empty, top returns -1.\n";
    return -1;
}

int size(Stack *stack)
{
    int result = 0;
    StackElement* element = stack->head;
    while (element != nullptr)
    {
        ++result;
        element = element->next;
    }

    return result;
}

void deleteStackElements(StackElement* element)
{
    if (element == nullptr)
    {
        return;
    }

    StackElement* next = element->next;
    delete element;
    deleteStackElements(next);
}

void deleteStack(Stack* stack)
{
    deleteStackElements(stack->head);
    delete stack;
}
