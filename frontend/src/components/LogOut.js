import React from "react";
import { Switch, Route, Redirect } from 'react-router-dom';
import { BrowserRouter} from 'react-router-dom';
import SignIn from './SignInComponent';
import SignUp from "./SignUpComponent";
import StartUp from "./StartUp";


const SignInWithRole = ({match}) => {
    return(
        <SignIn role={match.params.role}/>
    );
}
const SignUpWithRole = ({match}) => {
    return(
        <SignUp role={match.params.role}/>
    );
}


function LogOut(props){
    return (
        <BrowserRouter>
            <Switch>
                <Route path='/startup' component={StartUp}/>
                <Route path='/signin/:role' component={SignInWithRole}/>
                <Route path='/signup/:role' component={SignUpWithRole}/>
                <Redirect to='/startup' />
            </Switch>
        </BrowserRouter>
    );
}

export default LogOut;