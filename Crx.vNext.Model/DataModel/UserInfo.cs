using Dapper;

namespace Crx.vNext.Model.DataModel
{
    [Table("T_User")]
    public class UserInfo : BaseInfo
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
