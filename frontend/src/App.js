import './App.css';
import AllComplaints from './components/AllComplaintsComponent'
import ComplaintResponse from './components/ComplaintResponseComponent'
import MakeComplaint from './components/MakeComplaintComponent'
import CreateNewReview from './components/CreateNewReviewComponent'
import AllReviews from './components/AllReviewsComponent'
import LoginComponent from './components/LoginComponent'
import RestaurateurRestaurantList from './components/RestaurateurResaturantList'

function App() {
  const restaurantId = 1
  return (
    <AllReviews RestaurantId = {1} RestaurantName = "Test"/>
  );
}

export default App;
