using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.DAL.Models
{
    public class InterviewProcess
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CollegeId { get; set; }
        public int CompanyId { get; set; }
        public double? RoundOneScore { get; set; }
        public string RoundOneRemarks { get; set; }
        public bool RoundOneCleared { get; set; }
        public double? RoundTwoScore { get; set; }
        public string RoundTwoRemarks { get; set; }
        public bool RoundTwoCleared { get; set; }
        public bool IsSelected { get; set; }
        public DateTime? OfferReleasedDate { get; set; }
        public bool IsOfferAccepted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsOfferReleased { get; set; }
        public int PlacementRequestId { get; set; }
    }
}
