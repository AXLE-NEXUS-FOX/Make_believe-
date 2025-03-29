using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackExample : MonoBehaviour
{
    // Stack to store integers (or any other type)
    public Stack<int> numberStack;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the stack
        numberStack = new Stack<int>();

        // Push some numbers onto the stack
        numberStack.Push(10);
        numberStack.Push(20);
        numberStack.Push(30);

        Debug.Log("Stack after pushing numbers:");
        PrintStack(); // Print stack contents

        // Pop a number from the stack
        int poppedNumber = numberStack.Pop();
        Debug.Log("Popped number: " + poppedNumber);
        Debug.Log("Stack after popping a number:");
        PrintStack(); // Print stack contents

        // Peek at the top number without removing it
        int peekedNumber = numberStack.Peek();
        Debug.Log("Peeked number: " + peekedNumber);
        Debug.Log("Stack after peeking a number:");
        PrintStack(); // Print stack contents
    }
    void PrintStack()
    {
        foreach (int number in numberStack)
        {
            Debug.Log(number);
        }
    }

}

