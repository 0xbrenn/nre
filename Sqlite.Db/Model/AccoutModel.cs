using SqlSugar;


namespace Sqlite.Db.Model
{

    [SugarTable("account")]
    public class AccoutModel
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }

        public string ApiHash { get; set; }
        public int ApiId { get; set; }
        public string Password { get; set; }
       
    }


}
