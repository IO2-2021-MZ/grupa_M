import React from "react";
import { Switch, Route, Redirect } from 'react-router-dom';
import { BrowserRouter} from 'react-router-dom';
import AdminRestaurantList from "./AdminRestaurantList";



function Admin(props){
    return(
        <BrowserRouter>
            <Switch>
                <Route path='/RestaurantList' component={AdminRestaurantList}/>
                <Redirect to='/RestaurantList' />
            </Switch>
        </BrowserRouter>
    );
}

export default Admin;