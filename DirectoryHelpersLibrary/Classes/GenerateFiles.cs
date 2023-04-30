using static System.IO.File;

namespace DirectoryHelpersLibrary.Classes
{
    public class GenerateFiles
    {

        /// <summary>
        /// Pattern for base file name incrementing together with <see cref="_baseFileName"/>
        /// </summary>
        private static readonly string _pattern = "_{0}";
        /// <summary>
        /// Base file name together with <see cref="_pattern"/>
        /// </summary>
        private static string _baseFileName => "Data";
        /// <summary>
        /// Base file extension
        /// </summary>
        private static string _baseExtension => "json";

        /// <summary>
        /// Wrapper for <seealso cref="NextAvailableFilename"/> to obtain next available file name in a specific folder
        /// </summary>
        /// <returns>Unique ordered file name</returns>
        /// <remarks>
        /// Path is set to main assembly location with a base name of Import.txt
        /// </remarks>
        public static string NextFileName()
            => NextAvailableFilename(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{_baseFileName}.{_baseExtension}"));

        /// <summary>
        /// Wrapper for <see cref="GetNextFilename"/>
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Next incremented file name</returns>
        public static string NextAvailableFilename(string path)
        {

            if (!Exists(path) && Path.GetFileName(path)!.All(char.IsLetter))
            {
                return path;
            }

            return Path.HasExtension(path) ?
                GetNextFilename(path.Insert(
                    path.LastIndexOf(Path.GetExtension(path), StringComparison.Ordinal), _pattern)) :
                GetNextFilename(path + _pattern);

        }
        /// <summary>
        /// Work horse for <seealso cref="NextFileName"/>
        /// </summary>
        /// <param name="pattern"><see cref="_pattern"/></param>
        /// <returns>Next file name</returns>
        /// <remarks>DO NOT Change code in this method w/o talking to Karen</remarks>
        private static string GetNextFilename(string pattern)
        {
            string tmpValue = string.Format(pattern, 1);

            if (tmpValue == pattern)
            {
                throw new ArgumentException("The pattern must include an index place-holder", nameof(pattern));
            }

            if (!Exists(tmpValue))
            {
                return tmpValue;
            }

            int min = 1;
            int max = 2;

            while (File.Exists(string.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                int pivot = (max + min) / 2;

                if (Exists(string.Format(pattern, pivot)))
                {
                    min = pivot;
                }
                else
                {
                    max = pivot;
                }
            }

            return string.Format(pattern, max);
        }
        public static (bool success, string fileName) CreateFile()
        {
            try
            {
                var fileName = GenerateFiles.NextFileName();
                File.WriteAllText(fileName, "");
                return (true, fileName);
            }
            catch (Exception)
            {
                return (false, null);
            }
        }
        /// <summary>
        /// Strip characters from string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>string with numbers only</returns>
        public static string GetNumbers(string input)
            => new(input.Where(char.IsDigit).ToArray());

        /// <summary>
        /// Get all files
        /// </summary>
        private static string[] Files
            => Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory,
                $"{_baseFileName}*.{_baseExtension}");

        /// <summary>
        /// Get last file by int value
        /// </summary>
        /// <returns>Last  file</returns>
        public static string GetLast()
        {
            var result = Files
                .Select(file => new
                {
                    Name = file,
                    Number = Convert.ToInt32(GetNumbers(Path.GetFileName(file)))
                }).MaxBy(x => x.Number);

            return result?.Name;
        }

        /// <summary>
        /// Determine if there are any files
        /// </summary>
        /// <returns></returns>
        public static bool HasAnyFiles() => Files.Length > 0;

        /// <summary>
        /// Remove all files
        /// </summary>
        public static void RemoveAllFiles()
        {
            if (!HasAnyFiles()) return;

            foreach (var file in Files)
            {
                File.Delete(file);
            }
        }
    }
}

