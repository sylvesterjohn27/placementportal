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

        public bool AddOrRemoveCandidateForInterviewProcess(int placementRequestId, List<StudentViewModel> studentList, bool isRemove)
        {
            var placementRequest = _placementRequestRepository.GetPlacementRequestById(placementRequestId);
            if (placementRequest != null)
            {
                //Placement Request id approved
                if (!isRemove)
                {
                    foreach (var candidate in studentList)
                    {
                        var candidateExists = _interviewProcessRepository.GetCandidateByPlacementRequestIdandStudentId(placementRequestId, candidate.Id);
                        if (candidateExists == null)
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
                    }
                }
                else
                {
                    var existingCandidates = _interviewProcessRepository.GetInterviewProcessByPlacementRequestId(placementRequestId);
                    if (existingCandidates != null)
                    {
                        foreach (var candidate in existingCandidates)
                        {
                            _interviewProcessRepository.RemoveCandidateFromInterviewProcess(candidate);
                        }
                    }
                }                
                return true;               
            }           
            return false;
        }

        public InteviewProcessViewModel GetCandidateById(int interviewProcessId)
        {
            var candidate = _interviewProcessRepository.GetInterviewProcessById(interviewProcessId);
            var interviewProcess = new InteviewProcessViewModel
            {
                Id = candidate.Id,
                PlacementRequestId = candidate.PlacementRequestId,
                CollegeId = candidate.CollegeId,
                CompanyId = candidate.CompanyId,
                StudentId = candidate.StudentId,
                RoundOneScore = Convert.ToDouble(candidate.RoundOneScore),
                RoundOneRemarks = candidate.RoundOneRemarks,
                RoundOneCleared = candidate.RoundOneCleared,
                RoundTwoScore = Convert.ToDouble(candidate.RoundTwoScore),
                RoundTwoRemarks = candidate.RoundTwoRemarks,
                RoundTwoCleared = candidate.RoundTwoCleared,
                IsSelected = candidate.IsSelected,
                IsOfferAccepted = candidate.IsOfferAccepted,
                IsOfferReleased = candidate.IsOfferReleased,
                CreatedDate = (candidate.CreatedDate!=null)?candidate.CreatedDate.Value.ToString("yyyy-mm-dd") : null,
                ModifiedDate = (candidate.ModifiedDate != null) ? candidate.ModifiedDate.Value.ToString("yyyy-mm-dd") : null,
                OfferReleasedDate = (candidate.OfferReleasedDate != null) ? candidate.OfferReleasedDate.Value.ToString("yyyy-mm-dd") : null,
                CollegeName = _userRepository.GetUserById(candidate.CollegeId).Name,
                CompanyName = _userRepository.GetUserById(candidate.CompanyId).Name,
                StudentName = _StudentRepository.GetStudentById(candidate.StudentId).StudentName
            };
            return interviewProcess;
        }

        public void AddOrUpdateCandidateInterviewProcess(InteviewProcessViewModel model)
        {
            var interviewProcess = new InterviewProcess
            {
                Id = model.Id,
                PlacementRequestId = model.PlacementRequestId,
                CollegeId = model.CollegeId,
                CompanyId = model.CompanyId,
                StudentId = model.StudentId,
                RoundOneScore = model.RoundOneScore,
                RoundOneRemarks = model.RoundOneRemarks,
                RoundOneCleared = model.RoundOneCleared,
                RoundTwoScore = model.RoundTwoScore,
                RoundTwoRemarks = model.RoundTwoRemarks,
                RoundTwoCleared = model.RoundTwoCleared,
                IsSelected = model.IsSelected,
                IsOfferAccepted = model.IsOfferAccepted,
                IsOfferReleased = model.IsOfferReleased,
                ModifiedDate = DateTime.Now,
                OfferReleasedDate = (model.OfferReleasedDate!=null)?Convert.ToDateTime(model.OfferReleasedDate):null,
            };
            _interviewProcessRepository.AddCandidateForInterviewProcess(interviewProcess);
        }
    }
}
