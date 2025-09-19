using SqlSugar;
using System;

namespace Sqlite.Db.Model
{
    [SugarTable("settings")]
    public class SettingModel
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public int SendDelayMin { get; set; }
        public int SendDelayMax { get; set; }
        public int SendMin { get; set; }
        public int SendMax { get; set; }
        public int SendMsgNum { get; set; }
        public DateTime ScheduleDate { get; set; }


        public int InviteDelayMin { get; set; }
        public int InviteDelayMax { get; set; }
        public int InviteMin { get; set; }
        public int InviteMax { get; set; }
        public int InviteUserNum { get; set; }

        public int MaxSendStranger { get; set; }
        public int MaxInviteStranger { get; set; }

        public int MaxPeerFlood { get; set; }
        public int MaxToManyReqestDelay { get; set; }

        public bool ForceIgnore { get; set; }
    }
}
