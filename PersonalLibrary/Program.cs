
namespace PersonalLibrary
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Instantiate a PersonalLibrary object who contain the books
            PersonalLibrary personalLibrary = new PersonalLibrary();

            // Call the method who load the data in the library
            LoadData(); 
            
            //this is the program running loop until a "return" command will be executed
            while (true)
            {
                Console.Clear();
                personalLibrary.ConsoleArtLogo(); // Run the art logo method
                personalLibrary.ViewBooks(personalLibrary.Books); // List the books of the library

                int mainMenuOption = personalLibrary.MainMenu(); //Main menu selection

                // Main Menu witch based on previous selection
                switch (mainMenuOption)
                {
                    case 0: // Exit option
                        if(Utils.DoYouWantTo("EXIT") == true) return;
                        break;
                    case 1: // Add a book
                        ManageAddBook();
                        break;
                    case 2: // Select a book
                        ManageBookSelection(personalLibrary.Books);
                        break;
                    case 3: // Filter menu
                        ManageFilterMenu();
                        break;
                    case 4: // Sort by title
                        personalLibrary.SortBy("title");
                        break;
                    case 5: // Sort by status
                        personalLibrary.SortBy("status");
                        break;
                    case 6: // Hide / Show details of books listed
                        personalLibrary.ChangeShowDetails();
                        break;
                    case 7: // Enter to the Help information
                        ManageHelpMenu();
                        break;
                    case 8: // Display the About infromation section
                        ManagAboutSection();
                        break;
                }                                
            }

            // This is a method who load "hard coded" data into the library
            void LoadData()
            {
                personalLibrary.Books.Add(new Book("Despre Dumnezeu si om", "Lev Tolstoi", "Filozofie si Spiritualitate", 272, "9789735076603"));
                personalLibrary.Books.Add(new Book("Neuroplasticitatea, Secretul longevitatii creierului", "Leon Danaila", "Sanatate", 280, "9786303051710", "Reading", 229));
                personalLibrary.Books.Add(new Book("Fii obsedat sau fii mediocru", "Grant Cardone", "Dezvoltare Personala", 290, "9789975334921", "Read"));
                personalLibrary.Books.Add(new Book("Deep Work", "Cal Newport", "Dezvoltare Personala", 300, "978067223255", "Read"));
                personalLibrary.Books.Add(new Book("Cel mai intelept din incapere", "Tom Gilovich, Lee Ross", "Dezvoltare Personala", 336, "9786063335273", "Read"));
                personalLibrary.Books.Add(new Book("Arta Negocierii", "Chris Voss", "Leadership", 304, "6069456327", "Reading", 80));
                personalLibrary.Books.Add(new Book("Jurnalul fericirii", "Nicolae Steinhardt", "Spiritualitate", 576, "9789734627370", "Unread"));
                personalLibrary.Books.Add(new Book("Manager 80/20", "Richard Koch", "Business", 287, "9786069135020", "Read"));
                personalLibrary.Books.Add(new Book("50 de idei pe care trebuie sa le cunosti - Fizica", "Jane Baker", "Stiinta", 203, "9786063323058", "Unread"));
                personalLibrary.Books.Add(new Book("Cel mai bogat om din Babilon", "George S. Clason", "Business", 143, "9789737780027", "Reading", 60));
                personalLibrary.Books.Add(new Book("Secretele succesului", "Dale Carnegie", "Dezvoltare Personala", 279, "9789737780027", "Reading", 241));
            }
           

            // This Method manage the displayed info for a add a book
            void ManageAddBook()
            {
                Console.Clear();
                personalLibrary.ConsoleArtLogo();
                personalLibrary.AddABook();
            }


            // This method manage a book selction
            void ManageBookSelection(List<Book> books)
            {
                Console.Clear();
                personalLibrary.ConsoleArtLogo();
                personalLibrary.ViewBooks(books);

                int bookNumber = personalLibrary.SelectABookMenu(books);
        
                while (bookNumber != 0)
                {
                    Console.Clear() ;
                    personalLibrary.ConsoleArtLogo();
                    personalLibrary.BookInfo(books[bookNumber - 1]);
                    int bookMenuOption = personalLibrary.BookMenu();

                    switch (bookMenuOption)
                    {
                        case 0:
                            return;
                        case 1:
                            personalLibrary.MarkAsReadUnread(books[bookNumber - 1]);
                            break;
                        case 2:
                            personalLibrary.DeleteBook(books[bookNumber - 1]);               
                            return;
                        case 3:
                            ManageEditBook(books[bookNumber - 1]);
                            break;
                    }
                }
            }


            // This method manage the edit book process
            void ManageEditBook(Book selectedBook)
            {                
                while (true)
                {
                    Console.Clear();
                    personalLibrary.ConsoleArtLogo();
                    int editOption = personalLibrary.EditBookInfo(selectedBook);
                    if (editOption == 0) return;

                    if (editOption >= 1 && editOption <= 7)
                    {
                        personalLibrary.EditBookField(selectedBook, editOption);
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                }
            }


            // This method manage the Filter menu
            void ManageFilterMenu()
            {
                while (true)
                {
                    if (personalLibrary.Books.Count == 0) return;

                    Console.Clear();
                    personalLibrary.ConsoleArtLogo();
                    personalLibrary.ViewBooks(personalLibrary.Books);
                    int filterMenuOption = personalLibrary.FilterMenu();
                    if (filterMenuOption == 0) return;
                    int a;

                    switch (filterMenuOption)
                    {
                        case 1:
                            a = personalLibrary.FilterBy("Authors");
                            if (a == 0) continue;
                            ManageBookSelection(personalLibrary.FilteredBooks);
                            break;
                        case 2:
                            a = personalLibrary.FilterBy("Genre");
                            if (a == 0) continue;
                            ManageBookSelection(personalLibrary.FilteredBooks);
                            break;
                        case 3:
                            a = personalLibrary.FilterBy("Status");
                            if (a == 0) continue;
                            ManageBookSelection(personalLibrary.FilteredBooks);
                            break;                       
                    }
                }
            }

            // This method display the Help information 
            void ManageHelpMenu()
            {
                Console.Clear();
                personalLibrary.ConsoleArtLogo();
                personalLibrary.HelpInfo();
                Console.Write("    Press ENTER to return to Main Menu...");
                Console.ReadLine();
                return;
            
            }


            // This method display the About information 
            void ManagAboutSection()
            {
                Console.Clear();
                personalLibrary.ConsoleArtLogo();
                personalLibrary.About();
                Console.Write("    Press ENTER to return to Main Menu...");
                Console.ReadLine();
                return;

            }

        }
    }
}


