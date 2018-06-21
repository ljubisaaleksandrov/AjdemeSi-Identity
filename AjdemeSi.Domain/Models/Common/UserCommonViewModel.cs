namespace AjdemeSi.Domain.Models.Common
{
    public class UserCommonViewModel
    {
        public bool IsDriver { get; set; }

        /// <summary>
        /// Load other ride results if the user is driver - false by default
        /// </summary>
        public bool LoadResults { get; set; }

        //todo
        public UserCommonViewModel()
        {
            IsDriver = true;
            LoadResults = true;
        }
    }
}
