import React from "react";
import { Switch, Route, Redirect } from 'react-router-dom';
import { BrowserRouter} from 'react-router-dom';
import RestsList from "./RestaurateurResaturantList";


function Restaurer(props){
    return(
        <BrowserRouter>
            <Switch>
                <Route path='/RestaurantList' component={RestsList}/>
                <Redirect to='/RestaurantList' />
            </Switch>
        </BrowserRouter>
    );
}

export default Restaurer;
// RestaurateurRestaurant List
// AddNewDish 