using System.Windows.Input;

namespace Basic_Anti_Virus
{ 
    class Program
    {
        static void Main(string[] args)
        {
            
            
                Console.WriteLine("Basic Antivirus Started");
 

                // Stores the signatures file path in a string variable
                string signaturesFilePath = "C:\\Users\\jquin\\source\\repos\\Basic Anti-Virus\\Basic Anti-Virus\\Signatures\\signatures.txt";

                // Takes the list returned from LoadSignatures and puts it in the malwareSignature List
                List<string> malwareSignatures = LoadSignatures(signaturesFilePath);
            do
            {

                Console.WriteLine("Enter the path of the directory to scan: ");
                string? directoryPath = Console.ReadLine();


                //Checks if the variable directoryPath is NULL
                if (directoryPath == null)
                {
                    Console.WriteLine("directoryPath is NULL");

                }



                // Checks if the directoryPath exist
                if (Directory.Exists(directoryPath))
                {
                    ScanDirectory(directoryPath, malwareSignatures);
                }
                else
                {
                    Console.WriteLine("Directory not found.");
                }

                Console.WriteLine("Scan complete.");

                Console.WriteLine("Press ESC to stop the program");
            }
            // Will keep looping after one run and then can be stoped by using the ESC key
            while (Console.ReadKey().Key != ConsoleKey.Escape);

            
        }

        static List<string> LoadSignatures(string filePath)
        {
            List<string> signatures = new List<string>();

            //Checks to see if the file exists
            if (File.Exists(filePath))
            {
                // Reads all indivdual line of the file and stores it in the List named signatures
                signatures.AddRange(File.ReadAllLines(filePath));

                Console.WriteLine("Loaded signatures.");
            }
            else
            {
                Console.WriteLine("Signatures file not found.");
            }
            
            //Returns the list 
            return signatures;
        }

        static void ScanDirectory(string directoryPath, List<string> signtures)
        {
            // Makes sure that it looks at every file that ends with '.' and seaches every subdirectories 
            foreach (string file in Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories))
            {

                ScanFile(file, signtures);
            }
        }

        static void ScanFile(string filePath, List<string> signatures)
        {
            try
            {
                //Puts all of the text of the filePath on the variable fileCount
                string fileContent = File.ReadAllText(filePath);

                
                foreach (string signature in signatures)
                {
                    // Checks if the string of fileContent contains any strings in the List of signatures
                    if (fileContent.Contains(signature))
                    {
                        Console.WriteLine($"Malware dectected in file: {filePath}");

                    }
                    else if (!fileContent.Contains(signature))
                    {
                        Console.WriteLine($"File is clean of known signatures: {filePath}");
                    }
                    return;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error scanning file: {filePath}", ex);
            }
        }

    }

}


