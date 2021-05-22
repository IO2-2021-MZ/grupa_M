import React from "react";
import { Switch, Route, Redirect } from 'react-router-dom';
import { BrowserRouter} from 'react-router-dom';
import AddNewOrder from "./AddNewOrderComponent";
import RestsList from "./CustomerResaturantList";
import Rest from "./CustomerRestaurant";
import RestMenu from "./RestaurantMenu";
import CreateReview from "./CreateNewReviewComponent"

const RestWithId = ({match}) => {
    return(
        <Rest restId={match.params.id}/>
    );
}

const MenuWithId = ({match}) => {
    return(
        <RestMenu restId={match.params.id}/>
    );
}

const ReviewWithId = ({match}) => {
    return(
        <CreateReview restId={match.params.id}/>
    );
}

function Customer(props){
    return(
        <BrowserRouter>
            <Switch>
                <Route path='/RestaurantList' component={RestsList}/>
                <Route path='/NewOrder' component={AddNewOrder}/>
                <Route path='/Restaurant/:id' component={RestWithId}/>
                <Route path='/Restaurant/menu/:id' component={MenuWithId}/>
                <Route path='/Review/create/:id' component={ReviewWithId}/>
                <Redirect to='/RestaurantList' />
            </Switch>
        </BrowserRouter>
    );
}

export default Customer;