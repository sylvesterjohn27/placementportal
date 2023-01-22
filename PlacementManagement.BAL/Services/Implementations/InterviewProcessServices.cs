using PlacementManagement.BAL.Models;
using PlacementManagement.BAL.Services.Interfaces;
using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.BAL.Services.Implementations
{
    public class InterviewProcessServices: IInterviewProcessServices
    {
        private readonly IInterviewProcessRepository _interviewProcessRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _StudentRepository;
        private readonly IPlacementRequestRepository _placementRequestRepository;


        public InterviewProcessServices(IInterviewProcessRepository interviewProcessRepository, IUserRepository userRepository,IStudentRepository studentRepository, IPlacementRequestRepository placementRequestRepository)
        {
            _interviewProcessRepository = interviewProcessRepository;
            _userRepository = userRepository;
            _StudentRepository= studentRepository; 
            _placementRequestRepository = placementRequestRepository;
        }

        public List<InteviewProcessViewModel> GetInterviewProcessByPlacementRequestId(int placementRequestId)
        {
            var result = _interviewProcessRepository.GetInterviewProcessByPlacementRequestId(placementRequestId);
            var candidateList = new List<InteviewProcessViewModel>();
            foreach (var item in result)
            {
                candidateList.Add(new InteviewProcessViewModel
                {
                    Id = item.Id,
                    CollegeId = item.CollegeId,
                    CompanyId = item.CompanyId,
                    CollegeName = _userRepository.GetUserById(item.CollegeId).Name,
                    CompanyName = _userRepository.GetUserById(item.CompanyId).Name,
                    CreatedDate = item.CreatedDate?.ToString("yyyy-MM-dd"),
                    RoundOneScore = Convert.ToDouble(item.RoundOneScore),
                    RoundOneRemarks = item.RoundOneRemarks,
                    RoundOneCleared = item.RoundOneCleared,
                    RoundTwoScore = Convert.ToDouble(item.RoundTwoScore),
                    RoundTwoRemarks = item.RoundTwoRemarks,
                    RoundTwoCleared = item.RoundTwoCleared,
                    IsSelected = item.IsSelected,
                    IsOfferReleased = item.IsOfferReleased,
                    IsOfferAccepted = item.IsOfferAccepted,
                    ModifiedDate = item.ModifiedDate?.ToString("yyyy-MM-dd"),
                    OfferReleasedDate = item.OfferReleasedDate?.ToString("yyyy-MM-dd"),
                    StudentId = item.StudentId,
                    StudentName = _StudentRepository.GetStudentById(item.StudentId).StudentName,
                    PlacementRequestId = item.PlacementRequestId
                });
            }
            return candidateList;
        }

        public bool AddCandidateForInterviewProcess(int placementRequestId, List<StudentViewModel> studentList)
        {
            var placementRequest = _placementRequestRepository.GetPlacementRequestById(placementRequestId);
            if (placementRequest != null)
            {
                foreach (var candidate in studentList)
                {
                    _interviewProcessRepository.AddCandidateForInterviewProcess(new InterviewProcess
                    {
                        PlacementRequestId = placementRequestId,
                        CollegeId = candidate.CollegeId,
                        CompanyId = placementRequest.CompanyId,
                        StudentId = candidate.Id,
                        RoundOneScore = 0,
                        RoundOneRemarks = string.Empty,
                        RoundOneCleared = false,
                        RoundTwoScore = 0,
                        RoundTwoRemarks = string.Empty,
                        RoundTwoCleared = false,
                        IsSelected = false,
                        IsOfferAccepted = false,
                        IsOfferReleased = false,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        OfferReleasedDate = null
                    });
                }
                return true;
            }           
            return false;
        }
    }
}
