using SqlSugar;
using System;

namespace Sqlite.Db.Model
{
    [SugarTable("inviteLog")]
    public class InviteLogs
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string Account { get; set; }
        public string GroupName { get; set; }
        public string GroupUserName { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }
        public long UserId { get; set; }
        public bool IsSuccess { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }


    }
}
