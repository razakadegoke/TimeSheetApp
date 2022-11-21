using Microsoft.EntityFrameworkCore;

namespace TimeSheetApp.Auth{
    //Model utiliser pour création d'un nouvel utilisateur dans la DB
    public class UserAccount
    {
        public int Id { get; set; }
        public string? UserName {get; set;}
        public string? Password {get; set;}
        public string? Email {get; set;}
    }
}