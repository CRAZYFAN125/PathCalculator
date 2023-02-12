using Newtonsoft.Json;
using System;
using System.IO;
using static System.Console;

namespace PathCalculator
{
    /// <summary>
    /// Code to make faster doing things
    /// </summary>
    public class Behaviour
    {
        /// <summary>
        /// Gives current folder where files will go
        /// </summary>
        /// <returns>String with directory path</returns>
        public static string ApplicationDataPath()
        {
            string dir = Directory.GetCurrentDirectory();
            if (!Directory.Exists(Path.Combine(dir, "saveDirectory")))
            {
                Directory.CreateDirectory(Path.Combine(dir, "saveDirectory"));
            }
            return Path.Combine(dir, "saveDirectory");
        }

        #region Console text methods

        /* print(object data)
        /// <summary>
        /// Print informations
        /// </summary>
        /// <param name="data">Data to print in console</param>
        public static void print(object data)
        {
            WriteLine(data);
        }
        */

        /// <summary>
        /// Print informations with color
        /// </summary>
        /// <param name="data">Data to print in console</param>
        /// <param name="color">Color</param>
        public static void print(object data, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor consoleColor = ForegroundColor;
            ForegroundColor = color;
            WriteLine(data);
            ForegroundColor = consoleColor;
        }

        /// <summary>
        /// Reads information given from console
        /// </summary>
        /// <returns>String with data</returns>
        public string read()
        {
            return ReadLine();
        }

        /// <summary>
        /// Cleans fully console window
        /// </summary>
        public static void clear()
        {
            Clear();
        }

        #endregion


        #region Save System Code

        #region Quick
        /// <summary>
        /// Makes a save in save folder using JSON
        /// </summary>
        /// <param name="data">Thing to save</param>
        /// <param name="name">Saved file name (without extension)</param>
        /// <returns>True if saved succesfully</returns>
        public bool QuickSave(object data, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                string json = JsonConvert.SerializeObject(data, new JsonSerializerSettings() { Formatting = Formatting.Indented });
                File.WriteAllText(Path.Combine(ApplicationDataPath(), name + ".json"), json);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Loads a file from save directory
        /// </summary>
        /// <typeparam name="T">Class which indicates type of data in file</typeparam>
        /// <param name="name">File name (without extension)</param>
        /// <returns>Data from saved file</returns>
        public object QuickLoad<T>(string name)
        {
            if (File.Exists(Path.Combine(ApplicationDataPath(), name + ".json")))
            {
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(Path.Combine(ApplicationDataPath(), name + ".json")));
            }
            else
                return null;
        }

        /// <summary>
        /// Loads a file from save directory
        /// </summary>
        /// <typeparam name="T">Class which indicates type of data in file</typeparam>
        /// <param name="name">File name (without extension)</param>
        /// <param name="returnedData">Gives data from saved file</param>
        /// <returns>True if loaded file</returns>
        public bool QuickLoad<T>(string name, out object returnedData)
        {
            if (File.Exists(Path.Combine(ApplicationDataPath(), name + ".json")))
            {
                returnedData = JsonConvert.DeserializeObject<T>(File.ReadAllText(Path.Combine(ApplicationDataPath(), name + ".json")));
                return true;
            }
            else
            {
                returnedData = null;
                return false;
            }
        }
        #endregion

        #region Normal

        /// <summary>
        /// Makes a file with save
        /// </summary>
        /// <param name="data">Data to save</param>
        /// <param name="name">Name of save file (with extension, for examle .json|.txt will be as normal txt)</param>
        /// <param name="path">Name of folder in which file has to be saved</param>
        /// <returns>True if saved succesfull</returns>
        public bool Save(object data, string name, string path = "")
        {
            if (path != "")
            {
                path = Path.Combine(ApplicationDataPath(), path);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            else
            {
                path = ApplicationDataPath();
            }


            if (!string.IsNullOrEmpty(name))
            {
                if (name.Split('.')[1] == "txt")
                {
                    File.WriteAllText(Path.Combine(path, name), data as string);
                    return true;
                }
                else
                {
                    string json = JsonConvert.SerializeObject(data, new JsonSerializerSettings() { Formatting = Formatting.Indented });
                    File.WriteAllText(Path.Combine(path, name), json);
                    return true;
                }

            }
            return false;
        }

        /// <summary>
        /// Loads a file from save directory
        /// </summary>
        /// <typeparam name="T">Class which indicates type of data in file</typeparam>
        /// <param name="name">File name (with extension, for example .json)</param>
        /// <param name="path">Name of folder from which file has to be load</param>
        /// <param name="returnedData">Gives data from saved file</param>
        /// <returns>True if loaded succesfully</returns>
        public bool Load<T>(string name, string path, out object returnedData)
        {
            if (File.Exists(Path.Combine(Path.Combine(ApplicationDataPath(), path), name)))
            {
                returnedData = JsonConvert.DeserializeObject<T>(File.ReadAllText(Path.Combine(ApplicationDataPath(), name + ".json")));
                return true;
            }
            else
            {
                returnedData = null;
                return false;
            }
        }

        #endregion

        #region Delete function

        /// <summary>
        /// Deletes specific folder from save folder
        /// </summary>
        /// <param name="folderName">Name of folder to delete from save folder</param>
        /// <returns>True if deleted</returns>
        public bool DeleteFolder(string folderName)
        {
            folderName = Path.Combine(ApplicationDataPath(), folderName);
            if (Directory.Exists(folderName))
            {
                Directory.Delete(folderName);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes specific file from save folder
        /// </summary>
        /// <param name="fileName">Name of file to delete from save folder</param>
        /// <returns>True if deleted</returns>
        public bool DeleteFile(string fileName)
        {
            fileName = Path.Combine(ApplicationDataPath(), fileName);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                return true;
            }
            return false;
        }
        #endregion

        #endregion

        /// <summary>
        /// Gets factorial from number
        /// </summary>
        /// <param name="x">Number for factorial (greater than 0)</param>
        /// <returns>Factorial of number (0 if there was an error)</returns>
        public int Factorial(int x)
        {
            if (x <= 0)
            {
                return 0;
            }

            int silnia = 1;
            if (x == 1)
            {
                return 1;
            }
            for (int i = x - 1; i > 1; i--)
            {
                silnia *= i;
            }
            return silnia;
        }

        /// <summary>
        /// It's a Factorial but said in Polish
        /// </summary>
        /// <param name="x">Number for factorial (greater than 0)</param>
        /// <returns>Factorial of number (0 if there was an error)</returns>
        public int Silnia(int x) => Factorial(x);

        /// <summary>
        /// Gives string with JSON code of element
        /// </summary>
        /// <param name="x">Data to JSON</param>
        /// <returns>String with JSON code</returns>
        public string toJson(object x) =>JsonConvert.SerializeObject(x, new JsonSerializerSettings() { Formatting = Formatting.Indented });

    }
}
