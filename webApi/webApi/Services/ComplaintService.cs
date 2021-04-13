using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.Exceptions;
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
            if (newComplaint is null) throw new BadRequestException("Bad request");

            var nc = _mapper.Map<Complaint>(newComplaint);
            _context.Complaints.Add(nc);
            _context.SaveChanges();

            return nc.Id;
        }

        public void DeleteComplaint(int id)
        {
            var complaintToDelete = _context.Complaints.FirstOrDefault(c => c.Id == id);

            if (complaintToDelete is null) throw new NotFoundException("Resources not found");

            _context.Complaints.Remove(complaintToDelete);
            _context.SaveChanges();
        }

        public IEnumerable<ComplaintDTO> GetAllComplaints()
        {
            var queryResult = (from Complaints in _context.Complaints select Complaints).OrderBy(x => x.Id).ToList();
            var result = _mapper.Map<List<ComplaintDTO>>(queryResult);
            return result;
        }

        public ComplaintDTO GetComplaintById(int? id)
        {
            var complaint = _context.Complaints.FirstOrDefault(code => code.Id == id.Value);
            if (complaint is null) throw new NotFoundException("Resource not found");

            var complaintDTO =  _mapper.Map<ComplaintDTO>(complaint);
            return complaintDTO;
        }
        public void CloseComplaint(int id)
        {
            var complaint = _context.Complaints.FirstOrDefault(c => c.Id == id);
            if (complaint == null) throw new NotFoundException("Resources not found");

            complaint.Open = false;
            _context.SaveChanges();
        }

    }
}
