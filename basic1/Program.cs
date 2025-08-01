// See https://aka.ms/new-console-template for more information
int a = 1;
bool status = true;
string message = "abc";

Console.WriteLine($"a: {a}, status: {status}, Message: {message}");

if (status)
{
    Console.WriteLine("Status is true");
}
else
{
    Console.WriteLine("Status is false");
}

Console.WriteLine("Choose an option:");
Console.WriteLine("1. Option 1");
Console.WriteLine("2. Option 2");

int choice = Convert.ToInt32(Console.ReadLine());
switch (choice)
{
    case 1:
        Console.WriteLine("You chose option 1");
        break;
    case 2:
        Console.WriteLine("You chose option 2");
        break;
    default:
        Console.WriteLine("Invalid choice");
        break;
}
