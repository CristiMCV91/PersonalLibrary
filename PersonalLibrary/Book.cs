using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary
{
    public class Book
    {
        //Define the book properties
        public string Title;
        public string Author;
        public string Genre;
        public int Pages;
        public string ISBN;
        public string Status;
        public int Bookmark;

        //Constructor for book initialization with the required parameters
        public Book(string title, string author, string genre, int pages, string isbn, string status = "Unread", int bookmark = 0)
        {
            // Set the book properties 
            Title = title;
            Author = author;
            Genre = genre;
            Pages = pages;
            ISBN = isbn;
            Status = char.ToUpper(status[0]) + status.Substring(1).ToLower();
            Bookmark = bookmark;
            SetStatusPage(Status);

        }
        // This is a method to set the reading status and update the bookmark based on status
        public void SetStatusPage(string status)
        {
            status = char.ToUpper(status[0]) + status.Substring(1).ToLower();
            if (status == "Reading") { Status = status; }
            else if (status == "Read") { Status = status; ; Bookmark = Pages; }
            else { Status = "Unread"; Bookmark = 0; }
        }
    }
}
