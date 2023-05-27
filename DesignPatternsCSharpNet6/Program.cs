Console.WriteLine("123");

string input = "";

do
{
    input = Console.ReadLine() ?? "";

} while (!input.Equals("exit", StringComparison.InvariantCultureIgnoreCase));
