import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import { BrowserRouter } from "react-router-dom";
import RestaurateurRestaurantList from "./RestaurateurResturantList";
import RestaurateurRestaurant from "./RestaurateurRestaurant";
import AddNewDish from "./AddNewDishComponent";
import AddNewSection from "./AddNewSectionComponent";
import FinanceAndStats from "./FinanceAndStatsComponent";

function Restaurateur(props) {
  return (
    <BrowserRouter>
      <Switch>
        <Route
          path="/RestaurateurRestaurantList"
          component={RestaurateurRestaurantList}
        />
        <Route path="/AddNewDish" component={AddNewDish} />
        <Route path="/AddNewSection" component={AddNewSection} />
        <Route
          path="/RestaurateurRestaurant/:id"
          component={RestaurateurRestaurant}
        />
        <Route path="/FinanceAndStats" component={FinanceAndStats} />
        <Redirect to="/RestaurateurRestaurantList" />
      </Switch>
    </BrowserRouter>
  );
}

export default Restaurateur;
