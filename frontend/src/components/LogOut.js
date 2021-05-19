import React from "react";
import { Switch, Route, Redirect } from 'react-router-dom';
import { BrowserRouter} from 'react-router-dom';
import SignIn from './SignInComponent';
import SignUp from "./SignUpComponent"




function LogOut(props){
    return (
        <BrowserRouter>
            <Switch>
                <Route path='/signin' component={SignIn}/>
                <Route path='/signup' component={SignUp}/>
                <Redirect to='/signin' />
            </Switch>
        </BrowserRouter>
    );
}

export default LogOut;