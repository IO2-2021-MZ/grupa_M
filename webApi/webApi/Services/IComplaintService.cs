using System.Collections.Generic;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.DiscountCodeDTO;
using webApi.DataTransferObjects.DishDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.DataTransferObjects.ReviewDTO;

namespace webApi.Services
{
    public interface IComplaintService
    {
        public ComplaintDTO GetComplaintById(int? id);
        int CreateNewComplaint(NewComplaint newComplaint);
        bool DeleteComplaint(int id);
        IEnumerable<ComplaintDTO> GetAllComplaints();
        bool CloseComplaint(int id);

    }
}