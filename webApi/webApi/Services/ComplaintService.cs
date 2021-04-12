using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.Models;

namespace webApi.Services
{

    public class ComplaintService : IComplaintService
    {
        IO2_RestaurantsContext _context;
        private readonly IMapper _mapper;
        public ComplaintService(IO2_RestaurantsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        

        public int CreateNewComplaint(NewComplaint newComplaint)
        {
            var nc = _mapper.Map<Complaint>(newComplaint);
            _context.Complaints.Add(nc);
            _context.SaveChanges();

            return nc.Id;
        }

        public bool DeleteComplaint(int id)
        {
            var complaintToDelete = _context.Complaints.FirstOrDefault(c => c.Id == id);

            if (complaintToDelete == null) return false;

            _context.Complaints.Remove(complaintToDelete);
            return true;
        }

        public IEnumerable<ComplaintDTO> GetAllComplaints()
        {
            var queryResult = (from Complaints in _context.Complaints select Complaints).OrderBy(x => x.Id).ToList();
            var result = _mapper.Map<List<ComplaintDTO>>(queryResult);
            return result;
        }

        public ComplaintDTO GetComplaintById(int? id)
        {
            if (id == null) return null;

            return _mapper.Map<ComplaintDTO>(_context.Complaints.FirstOrDefault(code => code.Id == id.Value));
        }
        public bool CloseComplaint(int id)
        {
            throw new NotImplementedException();
            //var complaintToDelete = _context.Complaints.FirstOrDefault(c => c.Id == id);

            //if (complaintToDelete == null) return false;

            //complaintToDelete.Open = false;
            //return true;
        }

    }
}
