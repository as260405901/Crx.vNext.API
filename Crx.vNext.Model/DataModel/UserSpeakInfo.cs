using Dapper;

namespace Crx.vNext.Model.DataModel
{
    [Table("T_User_Speak")]
    public class UserSpeakInfo : BaseInfo
    {
        public string Speak { get; set; }
    }
}
