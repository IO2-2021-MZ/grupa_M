import React, { useContext, useEffect } from "react";
import UserContext from '../contexts/UserContext'
import Restaurer from "./Restaurateur";
import Admin from "./Admin";
import Customer from "./Customer";
import Employee from "./Employee";
import LogOut from "./LogOut";



function Main(){
    const { user, setUser } = useContext(UserContext);
    if(user === undefined){
        return <LogOut/>;
    }
    if(user.role === 'Restaurer'){
        return <Restaurer />;
    }
    if(user.role === 'Admin'){
        return <Admin/>;
    }
    if(user.role === 'Customer'){
        return <Customer/>;
    }
    if(user.role === 'Employee'){
        return <Employee/>;
    }
}

export default Main;