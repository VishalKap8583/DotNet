int a1, a2, a3;

for (int i = 1; i <= 3; i++)
{
    Console.WriteLine($"Enter value for a{i}:");
    if (!int.TryParse(Console.ReadLine(), out int value))
    {
        Console.WriteLine("Invalid input, please enter an integer.");
        i--; // Decrement to repeat the input for the same variable
        continue;
    }
    
    switch (i)
    {
        case 1:
            a1 = value;
            break;
        case 2:
            a2 = value;
            break;
        case 3:
            a3 = value;
            break;
    }
}