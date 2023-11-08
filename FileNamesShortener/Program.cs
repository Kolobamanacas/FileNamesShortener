using System.Text;

namespace FileNamesShortener;

internal class Program
{
    static int Main(string[] arguments)
    {
        Console.OutputEncoding = Encoding.UTF8;

        string? folderPath = null;
        int? prefixLength = null;

        for (int index = 0; index < arguments.Length; index++)
        {
            if (arguments[index] == "-path")
            {
                if (folderPath != null)
                {
                    Console.WriteLine("""You've passed multiple "-path" keys. Only one "-path" key is allowed.""");

                    return 1;
                }

                if (index + 1 >= arguments.Length)
                {
                    Console.WriteLine("""You didn't provide a path to a directory after "-path" key. Please add space separated path to a folder in a single quotes right after the "-path" key.""");

                    return 1;
                }

                if (!Directory.Exists(arguments[index + 1]) || !File.GetAttributes(arguments[index + 1]).HasFlag(FileAttributes.Directory))
                {
                    Console.WriteLine("""It seems like the path you've provided after the "-path" key doesn't exist or is not a folder or is not a valid path.""");

                    return 1;
                }

                folderPath = arguments[index + 1];
            }
            else if (arguments[index] == "-prefix-length")
            {
                if (prefixLength != null)
                {
                    Console.WriteLine("""You've passed multiple "-prefix-length" keys. Only one "-prefix-length" key is allowed.""");

                    return 1;
                }

                if (index + 1 >= arguments.Length)
                {
                    Console.WriteLine("""You didn't provide a length of a prefix after "-prefix-length" key. Please add space separated number right after the "-prefix-length" key.""");

                    return 1;
                }

                if (!int.TryParse(arguments[index + 1], out _) || int.Parse(arguments[index + 1]) <= 0)
                {
                    Console.WriteLine("""Wrong prefix length is provided. Prefix length should be a positive number.""");

                    return 1;
                }

                prefixLength = int.Parse(arguments[index + 1]);
            }
            else if (index - 1 >= 0 && arguments[index - 1] != "-path" && arguments[index - 1] != "-prefix-length")
            {
                Console.WriteLine("""Wrong arguments passed to the application. Possible arguments are "-path" and "-prefix-length". "-path" is mandatory.""");

                return 1;
            }
        }

        if (folderPath == null)
        {
            Console.WriteLine("""You didn't provide a "-path" keys. The path to the folder is a mandatory parameter.""");

            return 1;
        }

        prefixLength ??= 0;

        foreach (string filePath in Directory.GetFiles(folderPath, "", SearchOption.AllDirectories))
        {
            FileInfo fileInfo = new FileInfo(filePath);
            StringBuilder fileName = new StringBuilder(fileInfo.Name);

            if (fileName.Length - fileInfo.Extension.Length > prefixLength)
            {
                Console.WriteLine("Old name:");
                Console.WriteLine(filePath);

                fileName.Remove((int)prefixLength, fileInfo.Name.LastIndexOf('.') - (int)prefixLength);
                string newPath = Path.Combine(Path.GetDirectoryName(filePath)!, fileName.ToString());

                try
                {
                    fileInfo.MoveTo(newPath, true);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("An error occuded:");
                    Console.WriteLine(exception.ToString());

                    return 1;
                }

                Console.WriteLine("New name:");
                Console.WriteLine(Path.Combine(Path.GetDirectoryName(filePath)!, fileName.ToString()));
                Console.WriteLine();
            }
        }

        return 0;
    }
}