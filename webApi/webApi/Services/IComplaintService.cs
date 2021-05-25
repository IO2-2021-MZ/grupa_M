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
        public ComplaintDTO GetComplaintById(int? id, int userId);
        int CreateNewComplaint(NewComplaint newComplaint, int userId);
        void DeleteComplaint(int id, int userId);
        IEnumerable<ComplaintDTO> GetAllComplaints();
        void CloseComplaint(int id, string respons, int userId);

    }
}