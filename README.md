# Password Manager

A simple console-based Password Manager written in C#.

## Features

- Add new passwords
- AES encryption for stored passwords
- Decrypt passwords when displaying them
- Search passwords by website
- Edit existing passwords
- Delete password entries
- Save data to file
- Load data from file automatically on startup

## Technologies Used

- C#
- .NET
- AES Encryption
- File I/O (StreamReader / StreamWriter)
- Lists and Objects

## Project Structure

### PasswordEntry

Stores:

- Website
- Username
- Password (encrypted)

### PasswordManager

Contains the main functionality:

- AddPassword()
- EncryptPassword()
- DecryptPassword()
- ShowPasswords()
- SearchByWebsite()
- EditPassword()
- DeletePassword()
- SaveToFile()
- LoadFromFile()

## File Format

Passwords are stored in:

```text
passwords.txt
```

Each entry is saved as:

```text
website|username|encryptedPassword
```

Example:

```text
google.com|geno|Y7Rk2DkM9...
github.com|geno|N3Fj7LpQ1...
```

## How It Works

1. User enters website, username and password.
2. Password is encrypted using AES.
3. Data is stored in memory and saved to file.
4. On startup, data is loaded automatically.
5. Passwords are decrypted only when displayed.

## Menu

```text
1. Add Password
2. Show Passwords
3. Search By Website
4. Delete Password
5. Edit Password
6. Exit
```

## Learning Goals

This project was created to practice:

- Object-Oriented Programming (OOP)
- Classes and Objects
- Constructors
- Lists
- File Handling
- AES Encryption
- Methods
- Loops
- Conditionals
- Git and GitHub

## Author

Geno Dimitrov