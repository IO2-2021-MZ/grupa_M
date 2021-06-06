import React, { useContext, useEffect } from "react";
import UserContext from '../contexts/UserContext'
import Restaurateur from "./Restaurateur";
import Admin from "./Admin";
import Customer from "./Customer";
import Employee from "./Employee";
import LogOut from "./LogOut";



function Main(){
    const { user, setUser } = useContext(UserContext);
    if(user === undefined){
        return <LogOut/>;
    }
    if(user.role.toLowerCase() === 'restaurateur' || user.role.toLowerCase() === ' restaurateur'){
        console.log(user.address);
        return <Restaurateur />;
    }
    if(user.role.toLowerCase() === 'admin' || user.role.toLowerCase() === ' admin'){
        return <Admin/>;
    }
    if(user.role.toLowerCase() === 'customer' || user.role.toLowerCase() === ' customer'){
        return <Customer/>;
    }
    if(user.role.toLowerCase() === 'employee' || user.role.toLowerCase() === ' employee'){
        return <Employee/>;
    }
}

export default Main;