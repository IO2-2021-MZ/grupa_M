using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.Complaint;

namespace webApi.Services
{

    public interface IComplaintService
    {
        public TransferComplaint GetComplaintById(int? id);
        int CreateNewComplaint(NewComplaint newComplaint);
        void DeleteComplaint(int id);
        IEnumerable<TransferComplaint> GetAllComplaints();
        void CloseComplaint(int id);

    }
    public class ComplaintService : IComplaintService
    {
        Models.IO2_RestaurantsContext _context;
        public ComplaintService(Models.IO2_RestaurantsContext context)
        {
            _context = context;
        }
        public void CloseComplaint(int id)
        {
            throw new System.NotImplementedException();
        }

        public int CreateNewComplaint(NewComplaint newComplaint)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteComplaint(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TransferComplaint> GetAllComplaints()
        {
            throw new System.NotImplementedException();
        }

        public TransferComplaint GetComplaintById(int? id)
        {
            throw new System.NotImplementedException();
        }


    }
}