import React from "react";
import { Switch, Route, Redirect } from 'react-router-dom';
import { BrowserRouter} from 'react-router-dom';
import EmployeeRestaurantList from "./EmployeeRestaurantList";



function Employee(props){
    return(
        <BrowserRouter>
            <Switch>
                <Route path='/RestaurantList' component={EmployeeRestaurantList}/>
                <Redirect to='/NewOrder' />
            </Switch>
        </BrowserRouter>
    );
}

export default Employee;