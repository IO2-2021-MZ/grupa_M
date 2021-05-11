import './App.css';
import AllComplaints from './components/AllComplaintsComponent'
import ComplaintResponse from './components/ComplaintResponseComponent'
import MakeComplaint from './components/MakeComplaintComponent'
import CreateNewReview from './components/CreateNewReviewComponent'
import AllReviews from './components/AllReviewsComponent'
import LoginComponent from './components/LoginComponent'
import RestaurateurRestaurantList from './components/RestaurateurResaturantList'
import AddNewOrder from './components/AddNewOrderComponent'
import AddNewDiscountCode from './components/AddNewDiscountCodeComponent'

function App() {
  const restaurantId = 1
  return (
    //<ComplaintResponse complaintId={11}/>
    <AddNewDiscountCode restaurantId={1}/>
  );
}

export default App;
