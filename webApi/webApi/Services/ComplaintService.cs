using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.Models;
using webApi.DataTransferObjects.Complaint;

namespace webApi.Services
{

    public interface IComplaintService
    {
        public Complaint GetComplaintById(int? id);
        int CreateNewComplaint(NewComplaint newComplaint);
        void DeleteComplaint(int id);
        IEnumerable<Complaint> GetAllComplaints();
        void CloseComplaint(int id);

    }
    public class ComplaintService : IComplaintService
    {
        IO2_RestaurantsContext _context;
        public ComplaintService(IO2_RestaurantsContext context)
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

        public IEnumerable<Complaint> GetAllComplaints()
        {
            throw new System.NotImplementedException();
        }

        public Complaint GetComplaintById(int? id)
        {
            return _context.Complaints.FirstOrDefault(complaint => complaint.Id == id);
        }


    }
}
