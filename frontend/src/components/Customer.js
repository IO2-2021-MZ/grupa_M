import React from "react";
import { Switch, Route, Redirect } from 'react-router-dom';
import { BrowserRouter} from 'react-router-dom';
import AddNewOrder from "./AddNewOrderComponent";
import RestsList from "./CustomerResaturantList";
import Rest from "./CustomerRestaurant";
import RestMenu from "./RestaurantMenu";
import CreateReview from "./CreateNewReviewComponent"
import ReviewList from "./RestaurantReviewsCustomerComponent"
import OrdersHistory from "./CustomerOrdersHistory"
import MakeComplaint from "./MakeComplaintComponent"
import Complaints from "./AllComplaintsComponent"
import ComplaintList from "./CustomerComplaintList"

const RestWithId = ({match}) => {
    return(
        <Rest restId={match.params.id}/>
    );
}

const RestMenuWithId = ({match}) => {
    return(
        <RestMenu restId={match.params.id}/>
    );
}

const ReviewWithId = ({match}) => {
    return(
        <CreateReview restId={match.params.id}/>
    );
}

const ReviewForRestaurantWithId = ({match}) => {
    return(
        <ReviewList restId={match.params.id}/>
    );
}

const NewOrderRestaurantWithId = ({match}) => {
    return(
        <AddNewOrder restId={match.params.id}/>
    );
}

const MakeComplaintWithId = ({match}) => {
    return(
        <MakeComplaint orderIdd={match.params.id}/>
    );
}
const ComplaintsWithId = ({match}) => {
    return(
        <Complaints orderId={match.params.id}/>
    );
}

function Customer(props){
    return(
        <BrowserRouter>
            <Switch>
                <Route path='/RestaurantList' component={RestsList}/>
                <Route path='/NewOrder/Restaurant/:id' component={NewOrderRestaurantWithId}/>
                <Route path='/Restaurant/:id' component={RestWithId}/>
                <Route path='/OrdersHistory' component={OrdersHistory}/>
                <Route path='/Review/create/:id' component={ReviewWithId}/>
                <Route path='/Menu/Restaurant/:id' component={RestMenuWithId}/>
                <Route path='/reviews/restaurant/:id' component={ReviewForRestaurantWithId}/>
                <Route path='/Complaints/:id' component = {ComplaintsWithId} />
                <Route path='/ComplaintsList' component = {ComplaintList} />
                <Route path="/MakeComplaint/:id" component={MakeComplaintWithId}/>
                <Redirect to='/RestaurantList' />
            </Switch>
        </BrowserRouter>
    );
}

export default Customer;