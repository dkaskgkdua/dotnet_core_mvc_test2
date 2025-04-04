using System.ComponentModel.DataAnnotations;

namespace test2.Models
{
    public class Board
    {
        public int BoardId { get; set; }
        public string? BoardType { get; set; }
        public int Group_Num { get; set; }
        public int ParentId { get; set; }
        public int Group_Order { get; set; }
        public int Group_Tap { get; set; }
        public string? NickName { get; set; }
        public DateTime WriteTime { get; set; }
        public string? WriteIp { get; set; }
        public string? UpdUserId { get; set; }
        public string? UpdIp { get; set; }
        public string? Title { get; set; }
        [Display(Name ="본문")]
        public string? BoardContents { get; set; }
        public int SeeCount { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public string? FileUrl { get; set; }
        public int ReplyCount { get; set; }

    }
}
