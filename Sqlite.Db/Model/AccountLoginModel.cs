using SqlSugar;

namespace Sqlite.Db.Model
{
    [SugarTable("accountLogin")]
    public class AccountLoginModel
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Reason { get; set; }
    }

}
