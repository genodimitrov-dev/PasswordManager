class PasswordEntry
{
    public string Website { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public PasswordEntry(string website, string username, string password)
    {
        Website = website;
        Username = username;
        Password = password;
    }
}