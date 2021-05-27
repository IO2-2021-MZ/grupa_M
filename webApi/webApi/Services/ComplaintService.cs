using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.Enums;
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


        public int CreateNewComplaint(NewComplaint newComplaint, int userId)
        {
            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null ||
                user.Role != (int)Role.customer)
                throw new UnathorisedException("Unathourized");

            if (newComplaint is null) throw new BadRequestException("Bad request");

            var nc = _mapper.Map<Complaint>(newComplaint);
            _context.Complaints.Add(nc);
            _context.SaveChanges();

            return nc.Id;
        }

        public void DeleteComplaint(int id, int userId)
        {
            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null ||
                user.Role != (int)Role.admin)
                throw new UnathorisedException("Unathourized");

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

        public ComplaintDTO GetComplaintById(int? id, int userId)
        {
            var complaint = _context
                                .Complaints
                                .Include(c => c.Order)
                                .FirstOrDefault(code => code.Id == id.Value);

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null ||
                (user.Role == (int)Role.customer && complaint.CustomerId != user.Id) ||
                (user.Role == (int)Role.restaurateur && complaint.Order.RestaurantId != user.RestaurantId) ||
                (user.Role == (int)Role.employee && complaint.Order.RestaurantId != user.RestaurantId))
                throw new UnathorisedException("Unathourized");

            if (complaint is null) throw new NotFoundException("Resource not found");

            var complaintDTO =  _mapper.Map<ComplaintDTO>(complaint);
            return complaintDTO;
        }
        public void CloseComplaint(int id, string response, int userId)
        {
            var complaint = _context
                            .Complaints
                            .Include(c => c.Order)
                            .FirstOrDefault(c => c.Id == id);
            if (complaint == null) throw new NotFoundException("Resources not found");

            var user = _context
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null ||
                (user.Role == (int)Role.customer ) ||
                (user.Role == (int)Role.admin) ||
                (user.Role == (int)Role.restaurateur && complaint.Order.RestaurantId != user.RestaurantId) ||
                (user.Role == (int)Role.employee && complaint.Order.RestaurantId != user.RestaurantId))
                throw new UnathorisedException("Unathourized");

            if (response == null || response == string.Empty) return;
            complaint.Open = false;
            complaint.Response = response;
            _context.SaveChanges();
        }

    }
}
