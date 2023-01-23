using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.BAL.Models
{
    public class InteviewProcessViewModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public double RoundOneScore { get; set; }
        public string RoundOneRemarks { get; set; }
        public bool RoundOneCleared { get; set; }
        public double RoundTwoScore { get; set; }
        public string RoundTwoRemarks { get; set; }
        public bool RoundTwoCleared { get; set; }
        public bool IsSelected { get; set; }
        public string OfferReleasedDate { get; set; }
        public bool IsOfferAccepted { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool IsOfferReleased { get; set; }
        public int PlacementRequestId { get; set; }
    }
}
