# .NET5 Book Library

Console application for managing a book library, kept in a JSON file.

### User commands

- Adding a new book
- Borrowing a book from the library
- Returning a book
- Listing books with a filter
- Removing a book from the library

### Implemented constraints

- Taking a book longer than 2 months isn't allowed
- Reader cannot keep more than 3 books at the same time
- Deleting a book works only with present books
- Reader cannot enter the returning book activity, if it doesn't have any borrowed books

### Notes for execution

- The current version requires *Visual Studio* with *.NET5* framework installed.
- To run the program, clone the repository, open the solution with VS, make sure you have all packages installed and hit *Debug*
- Follow the program instructions listed in the opened terminal
- By default the program uses the library data file *books.json* that is located in *Data* folder. If you want to build the book library from scratch, simply rename or delete the *books.json* file and the program will create a new file based on user inputs through the terminal.

### Development choices

- To manage book availability, return date and reader name fields have been added to the book model. Compared to creating another model and file for readers (library visitors), it required less storage and made queries simple.
- All *Book* classes fields (except reader and return date) have been protected from changing the value. These values are set only during object creation, when the class constructor gets called.
- Only the *FileManipulator* class methods read and write to the file.
- The main library commands are executed by *Commander* class methods, it requires validated user inputs and modifies library data that is accessed through a FileManipulator type object.
- Only the *UserInputReader* class methods gather input from the terminal
- All input validations that require checking the library data are done in *ConstraintValidator's* methods.
- The program passed 16 different unit tests, written using *xUnit* tool. Note: to run them, each testing file needs to be run separately, because they share the same data file.