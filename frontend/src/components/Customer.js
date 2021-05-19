import React from "react";
import { Switch, Route, Redirect } from 'react-router-dom';
import { BrowserRouter} from 'react-router-dom';
import AddNewOrder from "./AddNewOrderComponent";



function Customer(props){
    return(
        <BrowserRouter>
            <Switch>
                <Route path='/NewOrder' component={AddNewOrder}/>
                <Redirect to='/NewOrder' />
            </Switch>
        </BrowserRouter>
    );
}

export default Customer;