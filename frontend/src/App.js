import './App.css';
import AllComplaints from './components/AllComplaintsComponent'
import ComplaintResponse from './components/ComplaintResponseComponent'
import MakeComplaint from './components/MakeComplaintComponent'
import CreateNewReview from './components/CreateNewReviewComponent'
import AllReviews from './components/AllReviewsComponent'

function App() {
  const restaurantId = 1
  return (
    <AllReviews RestaurantId = {1} RestaurantName = "Test"/>
  );
}

export default App;
