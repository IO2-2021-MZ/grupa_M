import React from "react";
import { Switch, Route, Redirect } from 'react-router-dom';
import { BrowserRouter} from 'react-router-dom';
import SignIn from './SignInComponent';
import StartUp from "./StartUp";
import SignUpCustomer from "./CustomerSU";
import SignUpEmployee from "./EmployeeSU";
import SignUpAdmin from "./AdminsSU";
import SignUpRestaurateur from "./RestaurateurSU";


const SignInWithRole = ({match}) => {
    return(
        <SignIn role={match.params.role}/>
    );
}


function LogOut(props){
    return (
        <BrowserRouter>
            <Switch>
                <Route path='/startup' component={StartUp}/>
                <Route path='/signin/:role' component={SignInWithRole}/>
                <Route path='/signup/admin' component={SignUpAdmin}/>
                <Route path='/signup/customer' component={SignUpCustomer}/>
                <Route path='/signup/restaurer' component={SignUpRestaurateur}/>
                <Route path='/signup/employee' component={SignUpEmployee}/>
                <Redirect to='/startup' />
            </Switch>
        </BrowserRouter>
    );
}

export default LogOut;