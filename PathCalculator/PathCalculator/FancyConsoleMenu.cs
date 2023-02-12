using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace PathCalculator
{
    public class FancyConsoleMenu
    {
        int SelectedIndex;
        string[] Options;
        string Prompt;

        /// <summary>
        /// Creates a menu
        /// </summary>
        /// <param name="prompt">Title</param>
        /// <param name="options">Otions which could be used</param>
        public FancyConsoleMenu(string prompt, string[] options)
        {
            Prompt= prompt;
            Options= options;
            SelectedIndex=0;
        }

        void DisplayText()
        {
            WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string cOption = Options[i];
                if (SelectedIndex==i)
                {
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteLine($">> {cOption} <<");
                }
                else
                {
                    ForegroundColor = ConsoleColor.Gray;
                    WriteLine($"<< {cOption} >>");
                }
                ForegroundColor = ConsoleColor.White;
            }
            ResetColor();
        }

        /// <summary>
        /// Activates menu builder
        /// </summary>
        /// <returns>Int that represents index of selected option</returns>
        public int Run()
        {
            CursorVisible = false; 
            ConsoleKey keyPressed;

            do
            {
                Clear();
                DisplayText();

                var info = ReadKey(true);
                keyPressed = info.Key;

                if (keyPressed==ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex>=Options.Length)
                    {
                        SelectedIndex= 0;
                    }
                }
                else if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex<0)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);

            CursorVisible = true;
            return SelectedIndex;
        }
    }
}
