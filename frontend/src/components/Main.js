import React, { useContext, useEffect } from "react";
import UserContext from '../contexts/UserContext'
import Restaurer from "./Restaurer";
import Admin from "./Admin";
import Customer from "./Customer";
import Employee from "./Employee";
import LogOut from "./LogOut";


function Main(){
    const { user, setUser } = useContext(UserContext);
    if(user === undefined){
        return <LogOut/>;
    }
    if(user.role === 0){ // role
        return <Restaurer />;
        // RestaurateurRestaurant List
        // AddNewDish 
    }
    if(user.role === 1){
        return <Admin/>;
        //AdminRestaurantList
    }
    if(user.role === 2){
        return <Customer/>;
        // Customer RestaurantList
        // AddNewOrder
    }
    if(user.role === 3){
        return <Employee/>;
        //EmployeeRestaurantList
    }
}

export default Main;