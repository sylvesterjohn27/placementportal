using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.DAL.Repository.Implementations
{
    public class InterviewProcessRepository : IInterviewProcessRepository
    {
        private readonly PlacementManagementAppDbContext _dbContext;

        public InterviewProcessRepository(PlacementManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<InterviewProcess> GetInterviewProcessByPlacementRequestId(int placementRequestId)
        {         
            return _dbContext.InterviewProcess.Where(c=>c.PlacementRequestId == placementRequestId).ToList();
        }

        public void AddCandidateForInterviewProcess(InterviewProcess interviewProcess)
        {           
            _dbContext.InterviewProcess.Add(interviewProcess);
            _dbContext.SaveChanges();
        }
    }
}