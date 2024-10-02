using System.ComponentModel.DataAnnotations;

namespace ServiceHub.WebApp.Models
{
    public class WorkReviewViewModel
    {
        [Display(Name = "RCA 1")]
        public string RCA1 { get; set; } = string.Empty;

        [Display(Name = "RCA 2")]
        public string RCA2 { get; set; } = string.Empty;

        [Display(Name = "RCA 3")]
        public string RCA3 { get; set; } = string.Empty;

        [Display(Name = "RCA 4")]
        public string RCA4 { get; set; } = string.Empty;

        [Display(Name = "RCA 5")]
        public string RCA5 { get; set; } = string.Empty;

        [Display(Name = "RCA Comments")]
        public string RCAComments { get; set; } = string.Empty;

        [Display(Name = "Part 1")]
        public string Part1 { get; set; } = string.Empty;

        [Display(Name = "Part 2")]
        public string Part2 { get; set; } = string.Empty;

        [Display(Name = "Part 3")]
        public string Part3 { get; set; } = string.Empty;

        [Display(Name = "Part 4")]
        public string Part4 { get; set; } = string.Empty;
    }
}