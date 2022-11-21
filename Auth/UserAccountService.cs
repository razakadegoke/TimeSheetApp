namespace TimeSheetApp.Auth
{
    //Service qui permet d'intéragir avec le DB 
    public class UserAccountService
    {
        private UserAccountDbContext dbContext;

        public UserAccountService(UserAccountDbContext _dbContext)
        {
           dbContext = _dbContext;
        }

        //Get le compte qui match le nom mit en parametre
        public UserAccount? GetByUserName(string _userName)
        {
            return dbContext.UserAccount.FirstOrDefault(x => x.UserName == _userName);
        } 

        //Ajoute un nouvel utilisateur a la DB
        public async Task<UserAccount> CreateNewAccount(UserAccount _userAccount)
        {
            try
            {
                dbContext.Add(_userAccount);
                await dbContext.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                
                throw;
            }
            return _userAccount;
        }
    }
}