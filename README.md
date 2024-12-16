# Personal Library

## Overview

The Personal Library application is a console-based program designed to help you manage your personal book collection.
Built using C#, it provides a comprehensive set of features for organizing, filtering, and maintaining a record of your books. 
The application use hardcoded data as demo purpose, but this ca be upgraded to read/write data in JSON files or database.

---
![Personal Library - Console UI](Pictures%20and%20Diagram/Personal%20Library%20-%20Console%20UI.png)
---
![Personal Library - Show Details](Pictures%20and%20Diagram/Personal%20Library%20-%20Show%20Details.png)

## Features

- **Add a Book**: Enter book details such as title, author, genre, status (read/unread), and bookmarks.
- **View All Books**: Display the entire library with all book details.
- **Filter Books**: Search and organize books by author, genre, or reading status.
- **Edit Book Information**: Update book details if is need it.
- **Delete a Book**: Remove books that are no longer needed.
- **Help Menu**: Display a tree structure of the menu for easy navigation.

---

## Technologies Used

- **Programming Language**: C#
- **IDE**: Visual Studio 2022

---

## How to Use

1. **Navigation**:
   - Use the main menu options by typing the number corresponding to the desired action.
   - Example: Type `1` to add a new book.

2. **Book Management - File Section**:
   - Add, view, edit, or delete books using the respective menu options.

3. **Visualization Options - View Section**:
   - Apply different vizualization as sorting data, and show the details of the books.

4. **Help Menu**:
   - Access the help menu to view a detailed tree structure of all available options.

---

## File Organization

```
PersonalLibrary
├── PersonalLibrary           # Main project folder
│   ├── Book.cs               # Book class
│   ├── PersonalLibrary.cs    # PersonalLibrary class
│   ├── Program.cs            # Entry point
│   ├── Utils.cs              # Utilities class
│
├── PersonalLibrary.sln       # Solution fIle for start the project
├── Pictures and Diagram      # Folder with pictures for documentation and program diagram
│   │ 
│   ├── Personal Library - Working Diagram - rev.7.pdf
│ 
├── README.md                 # Readme file
```

---

## Development

- **Application Name**: Personal Library
- **Version**: 1.0
- **Release Date**: 16.12.2024
- **Author / Student**: Cristian Macovei

- **Teacher**: Mihai Gonciar
- **Course**: C#
- **Academy**: IT School https://itschool.ro

---

## Future Improvements

- **Search Functionality**: Add a feature to locate books by keywords.
- **Graphical User Interface (GUI)**: Transition from console-based to a graphical interface.
- **Database integration**: Enable synchronization of library with a database.
- **Multi-language feature**: Add support for multiple languages.

---

## Contributing

Contributions are welcome! Feel free to submit bug reports, feature requests, or pull requests to help improve the application.
