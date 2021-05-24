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
    if(user === 'Restaurer'){ // role
        return <Restaurer />;
        // RestaurateurRestaurant List
        // AddNewDish 
    }
    if(user === 'Admin'){
        return <Admin/>;
        //AdminRestaurantList
    }
    if(user === 'Customer'){
        return <Customer/>;
        // Customer RestaurantList
        // AddNewOrder
    }
    if(user === 'Employee'){
        return <Employee/>;
        //EmployeeRestaurantList
    }
}

export default Main;