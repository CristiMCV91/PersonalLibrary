using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PersonalLibrary;


namespace PersonalLibrary
{
    // This static class provide usefull methods for the application
    static class Utils
    {

        // With this method you can get a int from the keyboard
        //The parameters allow to set the message provided as text and the input min/max limits
        public static int GetIntFromKeyboard(string whatToInput = "", int min = int.MinValue, int max = int.MaxValue)
        {
            int a;
            bool aValid;
          
            do
            {
                Console.Write($"{whatToInput} ");
                aValid = int.TryParse(Console.ReadLine(), out a);

                if (!(aValid && (a >= min && a <= max)))
                {
                    Console.WriteLine("Invalid data!");
                    aValid = false;
                }
                        
                    

            } while (!aValid);

            return a;
        }


        // With this method you can get a string from the keyboard
        //The parameters allow to set the message provided as text
        public static string GetStringFromKeyboard(string whatToInput = "")
        {
            string a;

            do
            {
                Console.Write($"{whatToInput} ");
                a = Console.ReadLine();
            } while (string.IsNullOrEmpty(a));

            return a;
        }


        // With this method you can get a bool from the keyboard.
        //The parameters allow to set the message provided as text
        // This method is usefull for action confirmation in the application
        public static bool DoYouWantTo(string action) 
        {
            int answer = 0;

            do 
            {
                answer = GetIntFromKeyboard($"\nDo you want to {action} ? \n[1. Yes] \n[2. No] \nSelect an option: ");

            } while (!(answer == 1 || answer == 2));

            if (answer == 1) 
            { return true; }
            else 
            { return false; }
                        
        }


        //This method provide you a string progress bar based on a percentage between 0-100 as a parameter
        public static string GetProgressBar(float percent)
        {
            int hashCountLength, dashCountLength;

            if ((int)percent % 10 >= 5)
            {
                hashCountLength = (((int)percent / 10) * 2) + 1;
                dashCountLength = ((10 - ((int)percent / 10)) * 2) - 1;
            }
            else
            {
                hashCountLength = ((int)percent / 10) * 2;
                dashCountLength = (10 - ((int)percent / 10)) * 2;
            }

            string hashCount = new string('#', hashCountLength);
            string dashCount = new string('-', dashCountLength);

            string progressBar = $"[{hashCount}{dashCount}]";

            return progressBar;
        }  

    }
}
