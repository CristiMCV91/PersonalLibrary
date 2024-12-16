using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace PersonalLibrary
{
    internal class PersonalLibrary
    {        
        // Define the Library properties
        public List<Book> Books;
        public List<Book> FilteredBooks;
        public bool BookRead, SortByTitle, ShowBookDetails;

        // The constructor inintialize the collection of books in the library
        public PersonalLibrary()
        {
            Books = new List<Book>();
            FilteredBooks = new List<Book>();
            ShowBookDetails = false;
            SortByTitle = false;            
        }


        // This method diplay the list of books provided as a parameter
        public void ViewBooks(List<Book> books)
        {
            Console.WriteLine("----------------------------------------\n");
            if (books.Count == 0)
            {
                Console.WriteLine("            ~ Empty Library ~           \n");
                Console.WriteLine("----------------------------------------\n");
                return;
            }

            Console.WriteLine("          ~ Books in Library ~         \n");
            Console.WriteLine("----------------------------------------\n");

            for (int i = 0; i < books.Count; i++)
            {
                string additionalSpace;
                if (i < 9) additionalSpace = new string(' ', (i.ToString().Length) + 1);
                else if (i == 9) additionalSpace = new string(' ', (i.ToString().Length));
                else additionalSpace = new string(' ', (i.ToString().Length-1));


                if (ShowBookDetails)
                {
                    Console.WriteLine($"{(i + 1)}:{additionalSpace}{books[i].Title} - {books[i].Author.ToUpper()}");
                    
                    if (i >= 9) additionalSpace += " ";

                    switch (books[i].Status)
                    {
                        
                        case "Reading":
                            float percentage = (((float)books[i].Bookmark / books[i].Pages) * 100);

                            Console.Write($"   {additionalSpace}> Status: {books[i].Status} | ");
                            Console.Write($"Progress: {Utils.GetProgressBar(percentage)}");
                            Console.Write($"{new string(' ', (3 - ((int)percentage).ToString().Length))} {percentage:F0}% | ");
                            Console.Write($"Page:{new string(' ', (3 - (books[i].Bookmark).ToString().Length))} {books[i].Bookmark}/{books[i].Pages}");
                            break;

                        case "Read": Console.Write($"   {additionalSpace}√ Status: {books[i].Status} {new string(' ', ("Reading".Length - books[i].Status.Length))}"); break;
                        case "Unread": Console.Write($"   {additionalSpace}x Status: {books[i].Status} {new string(' ', ("Reading".Length - books[i].Status.Length))}"); break;
                    }
                    Console.WriteLine("\n");
                }
                else 
                {
                    
                    switch (books[i].Status)
                    {
                        case "Reading": Console.WriteLine($"{(i + 1)}:{additionalSpace}{(SortByTitle ? "": ">")} {books[i].Title} - {books[i].Author.ToUpper()} {(SortByTitle ? "<" : "")}"); break;
                        case "Read": Console.WriteLine($"{(i + 1)}:{additionalSpace}{(SortByTitle ? "" : "√")} {books[i].Title} - {books[i].Author.ToUpper()} {(SortByTitle ? "√" : "")}"); break;
                        case "Unread": Console.WriteLine($"{(i + 1)}:{additionalSpace}{(SortByTitle ? "" : "x")} {books[i].Title} - {books[i].Author.ToUpper()} {(SortByTitle ? "x" : "")}"); break;
                    }
                }              
                
            }
            Console.WriteLine("\n----------------------------------------");
        }


        // Displays the main menu options for the library application.
        public int MainMenu()
        {
            int a;
            Console.WriteLine("########################################");
            Console.WriteLine("# Main Menu                            #");
            Console.WriteLine("########################################");

            if (Books.Count == 0)
            {
                Console.WriteLine("[0] EXIT \n[1] Add book \n");
                a = Utils.GetIntFromKeyboard("Select a menu option:", 0, 1);
            }
            else
            {
                Console.WriteLine($"" +
                    $"[0] EXIT \n" +
                    $"\n----File----\n" +
                    $"[1] Add a book \n" +
                    $"[2] Select book No. \n" +
                    $"[3] Filter Books \n" +
                    $"\n----View----\n" +
                    $"[4] Sort By Title \n" +
                    $"[5] Sort By Status \n" +
                    $"[6] {(ShowBookDetails ? "HIDE" : "SHOW")} book details\n" +
                    $"\n----Help----\n" +
                    $"[7] View Help\n" +
                    $"[8] About");
                Console.WriteLine("----------------------------------------");
                a = Utils.GetIntFromKeyboard("> Select a menu option (0-8):", 0, 8);
            }

            return a;

        }


        // Allows the user to add a new book to the library.
        public void AddABook()
        {

            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine("   ~ Add a new book in your library ~   \n");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("[0] Back \n");
            
            string title = Utils.GetStringFromKeyboard("Title: ");
            if (title == "0" ) if (Utils.DoYouWantTo("Return (the input data will be losed)") == true) return;
            else title = Utils.GetStringFromKeyboard("\nTitle: ");

            string author = Utils.GetStringFromKeyboard("Author: ");
            if (author == "0") if (Utils.DoYouWantTo("Return (the input data will be losed)") == true) return;
            else author = Utils.GetStringFromKeyboard("\nAuthor: ");

            string genre = Utils.GetStringFromKeyboard("Genre: ");
            if (genre == "0") if (Utils.DoYouWantTo("Return (the input data will be losed)") == true) return;
            else genre = Utils.GetStringFromKeyboard("\nGenre: ");

            int pages = Utils.GetIntFromKeyboard("Pages: ", 0);
            if (pages == 0) if (Utils.DoYouWantTo("Return (the input data will be losed)") == true) return;
            else pages = Utils.GetIntFromKeyboard("\nPages: ", 0);

            string isbn = Utils.GetStringFromKeyboard("ISBN: ");
            if (isbn == "0") if (Utils.DoYouWantTo("Return (the input data will be losed)") == true) return;
            else isbn = Utils.GetStringFromKeyboard("\nISBN: ");

            string status = "";


            while (!(status == "Reading" || status == "Unread" || status == "Read"))
            {
                status = Utils.GetStringFromKeyboard("Status [Unread, Read, Reading]: ");
                if (status == "0") if (Utils.DoYouWantTo("Return (the input data will be losed)") == true) return;
                else status = Utils.GetStringFromKeyboard("\nStatus [Unread, Read, Reading]: ");

                status = char.ToUpper(status[0]) + status.Substring(1).ToLower();

                if (status == "Reading")
                {
                    int bookmark = Utils.GetIntFromKeyboard("Bookmark [page number]: ", 0, pages);
                    if (bookmark == 0) if (Utils.DoYouWantTo("Return (the input data will be losed)") == true) return;
                    else bookmark = Utils.GetIntFromKeyboard("\nBookmark [page number]: ", 0, pages);

                    Console.WriteLine("\n----------------------------------------");
                    if (Utils.DoYouWantTo("save") == true)
                    {
                        Books.Add(new Book(title, author, genre, pages, isbn, status, bookmark));
                    }
                }
                else if (status == "Unread" || status == "Read")
                {
                    Console.WriteLine("\n----------------------------------------");
                    if (Utils.DoYouWantTo("save") == true)
                    {
                        Books.Add(new Book(title, author, genre, pages, isbn, status));
                    }
                }
            }

        }

        //This is a method for a book selection menu
        public int SelectABookMenu(List<Book> books)
        {
            Console.WriteLine("########################################");
            Console.WriteLine("# Select a Book                        #");
            Console.WriteLine("########################################");
            Console.WriteLine("[0] Back");

            if (books.Count() == 1) { Console.WriteLine($"[{books.Count()}] Select the book\n"); }
            else { Console.WriteLine($"[1-{books.Count()}] Select a book Number\n"); }

            Console.WriteLine("----------------------------------------");
            int bookNumber = Utils.GetIntFromKeyboard($"> Insert your option (0-{books.Count()}):", 0, books.Count());
            return bookNumber;

        }


        //The method diplay the detaliated info of a book provided as a parameter
        public void BookInfo(Book selectedBook)
        {
            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine("           ~ Book Information ~         \n");
            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine($"Title:    {selectedBook.Title}");
            Console.WriteLine($"Author:   {selectedBook.Author}");
            Console.WriteLine($"Genre:    {selectedBook.Genre}");
            Console.WriteLine($"Pages:    {selectedBook.Pages}");
            Console.WriteLine($"ISBN:     {selectedBook.ISBN}");
            Console.WriteLine($"Status:   {selectedBook.Status}");
            Console.WriteLine($"Bookmark: pag. {selectedBook.Bookmark}\n");
            Console.WriteLine("----------------------------------------");
            SetBookStatus(selectedBook);
        }


        // Displays the options for managing a specific book, such as marking it as read/unread,
        public int BookMenu()
        {
            Console.WriteLine("########################################");
            Console.WriteLine("# Book Menu                            #");
            Console.WriteLine("########################################");

            Console.WriteLine($"[0] Back \n[1] Mark as {((BookRead) ? "UNREAD" : "READ")} \n[2] Delete Book \n[3] Edit book info\n");
            Console.WriteLine("----------------------------------------");
            int a = Utils.GetIntFromKeyboard("> Select a menu option (0-3):", 0, 3);

            return a;

        }


        //This method set the boolean state of "BookRead" variable based on the book status
        public void SetBookStatus(Book book)
        {
            switch (book.Status)
            {
                case "Read": BookRead = true; break;
                default: BookRead = false; break;
            }
        }


        //This method chaneg the state of the book status
        public void MarkAsReadUnread(Book book) 
        {
            switch (book.Status)
            {
                case "Read": book.SetStatusPage("Unread"); BookRead = false; break;
                default : book.SetStatusPage("Read"); BookRead = true; break;
            }            
        }


        //This method delete a book
        public void DeleteBook(Book bookToDelete)
        {
            if (Utils.DoYouWantTo("delete") == true)
            {
                Books.Remove(bookToDelete);
            }

        }

        
        // With this method you can select a field that you want to edit, and return a field number
        public int EditBookInfo(Book selectedBook)
        {
            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine("        ~ Edit Book Information ~       \n");
            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine($"[1] Title:         {selectedBook.Title}");
            Console.WriteLine($"[2] Author:        {selectedBook.Author}");
            Console.WriteLine($"[3] Genre:         {selectedBook.Genre}");
            Console.WriteLine($"[4] Pages:         {selectedBook.Pages}");
            Console.WriteLine($"[5] ISBN:          {selectedBook.ISBN}");
            Console.WriteLine($"[6] Status:        {selectedBook.Status}");
            Console.WriteLine($"[7] Bookmark: pag. {selectedBook.Bookmark}\n");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("########################################");
            Console.WriteLine("# Book Menu > Edit book                #");
            Console.WriteLine("########################################");
            Console.WriteLine("[0. Back]\n");
            Console.WriteLine("----------------------------------------");
            int a = Utils.GetIntFromKeyboard("> Select a menu option (0-7):", 0, 7);

            return a;
        }


        //This metod can edit a field number of a selected book sent as a parameter
        public void EditBookField(Book selectedBook, int field)
        {
            switch (field)
            {
                case 1:
                    {
                        Console.WriteLine($"\nOld Title: {selectedBook.Title}");
                        string title = Utils.GetStringFromKeyboard("New Title: ");
                        if (Utils.DoYouWantTo("save") == true)
                        {
                            selectedBook.Title = title;
                        }
                    }
                    break;
                case 2:
                    {
                        Console.WriteLine($"\nOld Author: {selectedBook.Author}");
                        string author = Utils.GetStringFromKeyboard("New Author: ");
                        if (Utils.DoYouWantTo("save") == true)
                        {
                            selectedBook.Author = author;
                        }
                    }
                    break;
                case 3:
                    {
                        Console.WriteLine($"\nOld Genre: {selectedBook.Genre}");
                        string genre = Utils.GetStringFromKeyboard("New Genre: ");
                        if (Utils.DoYouWantTo("save") == true)
                        {
                            selectedBook.Genre = genre;
                        }
                    }
                    break;
                case 4:
                    {
                        Console.WriteLine($"\nOld Pages: {selectedBook.Pages}");
                        int pages = Utils.GetIntFromKeyboard("New Pages: ", 1);
                        if (Utils.DoYouWantTo("save") == true)
                        {
                            selectedBook.Pages = pages;
                        }
                    }
                    break;
                case 5:
                    {
                        Console.WriteLine($"\nOld ISBN: {selectedBook.ISBN}");
                        string isbn = Utils.GetStringFromKeyboard("New ISBN: ");
                        if (Utils.DoYouWantTo("save") == true)
                        {
                            selectedBook.ISBN = isbn;
                        }
                    }
                    break;
                case 6:
                    {
                        Console.WriteLine($"\nOld Status: {selectedBook.Status}");
                        string status = Utils.GetStringFromKeyboard("New Status [Unread, Read, Reading]: ");

                        if (Utils.DoYouWantTo("save") == true)
                        {
                            selectedBook.SetStatusPage(status);
                        }
                    }
                    break;
                case 7:
                    {
                        Console.WriteLine($"\nOld Bookmark: {selectedBook.Bookmark}");
                        int bookmark = Utils.GetIntFromKeyboard("New Bookmark: ", 0);
                        if (Utils.DoYouWantTo("save") == true)
                        {
                            if (bookmark == 0)
                            {
                                selectedBook.SetStatusPage("Unread");
                            }
                            else if (bookmark == selectedBook.Pages)
                            {
                                selectedBook.SetStatusPage("Read");
                            }
                            else
                            {
                                selectedBook.Bookmark = bookmark;
                            }

                        }
                    }
                    break;
            }


        }


        //The method display the Filter menu and return the number of the menu option selected
        public int FilterMenu()
        {
            Console.WriteLine("########################################");
            Console.WriteLine("# Main Menu > Filter Books             #");
            Console.WriteLine("########################################");

            Console.WriteLine("[0] Main Menu \n[1] Filter by Author \n[2] Filter by Genre \n[3] Filter by Status \n");
            Console.WriteLine("----------------------------------------");
            int a = Utils.GetIntFromKeyboard("> Select a menu option (0-3):", 0, 3);

            return a;

        }


        //This method display the values available for the filter selected and return the number of the value selected
        public int FilterBy(string filterBy)
        {
            Console.Clear();
            ConsoleArtLogo();
            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine($"     ~ Available {filterBy} in library ~   \n");
            Console.WriteLine("----------------------------------------\n");

            HashSet<string> filterData = new HashSet<string>();

            for (int i = 0; i < Books.Count; i++)
            {
                switch (filterBy)
                {
                    case "Authors": filterData.Add(Books[i].Author); break;
                    case "Genre": filterData.Add(Books[i].Genre); break;
                    case "Status": filterData.Add(Books[i].Status); break;
                }

            }

            List<string> dataList = new List<string>(filterData);
            dataList.Sort();

            for (int j = 0; j < dataList.Count; j++)
            {
                string additionalSpace;
                if (j < 9) additionalSpace = new string(' ', (j.ToString().Length) + 1);
                else if (j == 9) additionalSpace = new string(' ', (j.ToString().Length));
                else additionalSpace = new string(' ', (j.ToString().Length - 1));

                Console.WriteLine($"[{j + 1}]{additionalSpace}{dataList[j]}");
            }


            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("#########################################");

            switch (filterBy)
            {
                case "Authors": Console.WriteLine("# Main Menu > Filter Books > By Authors #"); break;
                case "Genre": Console.WriteLine("# Main Menu > Filter Books > By Genre   #"); break;
                case "Status": Console.WriteLine("# Main Menu > Filter Books > By Status  #"); break;
            }

            Console.WriteLine("#########################################");
            Console.WriteLine("[0] Back\n");
            Console.WriteLine("----------------------------------------");
            int a = Utils.GetIntFromKeyboard($"> Select a menu option (0-{dataList.Count}):", 0, dataList.Count);


            if (a != 0)
            {
                switch (filterBy)
                {
                    case "Authors": FilteredBooks = Books.FindAll(item => item.Author == dataList[a - 1]); break;
                    case "Genre": FilteredBooks = Books.FindAll(item => item.Genre == dataList[a - 1]); break;
                    case "Status": FilteredBooks = Books.FindAll(item => item.Status == dataList[a - 1]); break;
                }
            }

            return a;

        }


        //This method sort the books by title of by status
        public void SortBy(string sortBy)
        {
            switch (sortBy.ToLower())
            {
                case "title": 
                    Books = Books.OrderBy(item => item.Title).ToList();
                    SortByTitle = true;
                    break;
                case "status": 
                    Books = Books.OrderBy(item => item.Status).ToList();
                    SortByTitle = false;
                    break;
            }
        }


        //This method switch the state of the boolean variable "ShowBookDetails" who is responsile for the books details displayed when are listed
        public void ChangeShowDetails() 
        {
         ShowBookDetails = !ShowBookDetails;
        }


        //This is a method who display an console graphic for the application
        public void ConsoleArtLogo()
        {
            //https://www.asciiart.eu/

            string art =
@"        .--.                    .---.
    .---|__|           .-.      |~~~|
 .--|===|  |_          |_|  .---| S |--.
 |  |===|  |'\     .---!~|  |%%%| C |--|    ____                                 _      _     _ _                          
 |%%|   |  |.'\    |===| |--|   | H |  |   |  _ \ ___ _ __ ___  ___  _ __   __ _| |    | |   (_) |__  _ __ __ _ _ __ _   _ 
 |%%|   |  |\.'\   |   | |__| I | O |  |   | |_) / _ \ '__/ __|/ _ \| '_ \ / _` | |    | |   | | '_ \| '__/ _` | '__| | | |
 |  |   |  | \  \  |===| |==| T | O |  |   |  __/  __/ |  \__ \ (_) | | | | (_| | |    | |___| | |_) | | | (_| | |  | |_| |
 |  |   |__|  \.'\ |   |_|__|   | L |__|   |_|   \___|_|  |___/\___/|_| |_|\__,_|_|    |_____|_|_.__/|_|  \__,_|_|   \__, |
 |  |===|--|   \.'\|===|~|--|%%%|~~~|--|                                                                             |___/   
 ^--^---'--^    `-'`---^-^--^---^---'--'   ";

            Console.WriteLine(art);
        }

        public void HelpInfo()
        {
            string help =@"
    Welcome to the Personal Library Application! Below is the structure and functionality of the program:
    
    MAIN MENU:
    [0] EXIT
       - Ends the application.
    
    ----File----
    [1] Add Book - Allows you to add a new book to the library.
       [0] Back
            - Confirm the exit from the book registration
       
       - You will need to provide the following details:
             - Title
             - Author
             - Genre
             - Number of Pages
             - Status (Read/Unread/Reading)
             - Bookmark (Optional - if the status is 'Reading')
       - After entering the details, you can:
             - SAVE the book and return to the Main Menu.
             - CANCEL to discard changes and return to the Main Menu.
    
    [2] Select Book No. - Enter the book number to perform actions on a specific book.
       [0] Back

       [1-n]. Select a book Number
              [0] Back
              [1] Mark the book as Read/Unread.
              [2] Delete the book (requires confirmation).
              [3] Edit the book details (Title, Author, Genre, Pages, Status, Bookmark).
                  [0] Back
                  [1] Title
                  [2] Author
                  [3] Genre
                  [4] Pages
                  [5] ISBN
                  [6] Status
                  [7] Bookmark

    ----View----
    [3] Filter Books
        [0] Main Menu
        [1] Filter by Author - Select an author to view their books.
              [0] Back 
              [1-n]. Select an Author
                  [0] Back
                  [1-n]. Select a book Number
                    
        [2] Filter by Genre - Select a genre to view books in that category.
              [0] Back 
              [1-n]. Select a Genre
                  [0] Back
                  [1-n]. Select a available genre

        [3] Filter by Status - View books marked as Read or Unread.
              [0] Back 
              [1-n]. Select a Genre
                  [0] Back
                  [1] Read
                  [2] Reading
                  [3] Unread
    
    ----View----
    [4] Sort By Title - Alphabetical order.
    [5] Sort By Status - Grouped by Read/Unread.
    [6] SHOW book details - Toggles the visibility of book details for all books in the library.

    ----Help----
    [7] View Help
    [8] About       
    
    ADDITIONAL NOTES:
    - Use numbers to navigate through the menu options.
    - Follow on-screen instructions for input and confirmation messages.
    - Ensure valid data is entered when adding or editing book details.";

            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine("             ~ HELP MENU ~              \n");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(help);
            Console.WriteLine("\n----------------------------------------\n");
        }

        public void About()
        {
            string about = @"
    Welcome to the Personal Library Application v1.0!  

    This is a console program designed for helping you to efficiently manage your personal book collection. 
    
    # Application Purpose:
    |
    |   The Personal Library application aims to streamline book management by providing tools for tracking reading progress, 
    |   organizing your collection, and keeping everything in one place.
    |   
    #

    # Developer Information:
    |  
    |   Application Name:     Personal Library
    |   Version:              v1.0
    |   Release date:         16.12.2024
    |   Student:              Cristian Macovei
    |   Github:               https://github.com/CristiMCV91/PersonalLibrary
    |                    
    |   Teacher:              Mihai Gonciar
    |   Course:               C# 
    |   Academy:              IT School https://itschool.ro 
    #

    

    Enjoy !  ";

            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine("    ~ About Personal Library v1.0 ~     \n");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(about);
            Console.WriteLine("\n----------------------------------------\n");
        }
    }
}

