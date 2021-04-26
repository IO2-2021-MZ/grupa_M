import './App.css';
import AllComplaints from './components/AllComplaintsComponent'
import ComplaintResponse from './components/ComplaintResponseComponent'
import LoginComponent from './components/LoginComponent'
import RestaurateurRestaurantList from './components/RestaurateurResaturantList'

function App() {
  const restaurantId = 1
  return (
    <AllComplaints restaurantId={1}/>
  );
}

export default App;
