// See https://aka.ms/new-console-template for more information

int LevDist(string word1, string word2)
{
    var matrix = new int[word1.Length + 1, word2.Length + 1];
    for (var i = 0; i < word1.Length; i++)
    {
        matrix[i, 0] = i;
    }
    for (var j = 0; j < word2.Length; j++)
    {
        matrix[0, j] = j;
    }
    for (var j = 1; j <= word2.Length; j++)
    {
        for (var i = 1; i <= word1.Length; i++)
        {
            if (word1[i-1] == word2[j-1])
            {
                matrix[i, j] = matrix[i-1, j-1];
            }
            else
            {
                matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), matrix[i - 1, j - 1] + 1);
            }
        }
    }
    return matrix[word1.Length, word2.Length];
}


Console.Write(">Enter name: ");
var name = Console.ReadLine();

if (name.Trim() == string.Empty)
{
    Console.Write("Your name was not found.");
    return;
}

if (!File.Exists("/Users/gabriela/Downloads/us.txt"))
{
    Console.Write("File not found");
}
else
{
    using (var text = File.OpenText("/Users/gabriela/Downloads/us.txt"))
    {
        string str;
        var variants = new List<string>();
        while ((str = text.ReadLine()) != null)
        {
            var res = LevDist(name.Trim(), str);
            switch (res)
            {
                case 0:
                    Console.WriteLine($"Hello, {name}!");
                    return;
                case < 2:
                    variants.Add(str);
                    break;
            }
        }
        while (variants.Count != 0)
        {
            Console.WriteLine($">Did you mean “{variants[0]}”? Y/N");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "Y":
                    Console.WriteLine($"Hello, {variants[0]}!");
                    return;
                case "N":
                    variants.RemoveAt(0);
                    break;
                default:
                    Console.WriteLine("Something went wrong. Check your input and retry.");
                    return;
            }
        }
        Console.WriteLine("Your name was not found.");
    }
}
