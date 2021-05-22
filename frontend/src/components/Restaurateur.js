import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import { BrowserRouter } from "react-router-dom";
import RestaurateurRestaurantList from "./RestaurateurRestaurantList";
import RestaurateurRestaurant from "./RestaurateurRestaurant";
import AddNewDish from "./AddNewDishComponent";
import AddNewSection from "./AddNewSectionComponent";
import FinanceAndStats from "./FinanceAndStatsComponent";
import RestaurateurSections from "./RestaurateurSections";

const RestaurateurRestaurantWithId = ({ match }) => {
  return <RestaurateurRestaurant restId={match.params.id} />;
};

const RestaurateurSectionsWithId = ({ match }) => {
  return <RestaurateurSections restId={match.params.id} />;
};

const FinanceAndStatsWithId = ({ match }) => {
  return <FinanceAndStats restaurantId={match.params.id} />;
};

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
          component={RestaurateurRestaurantWithId}
        />
        <Route
          path="/RestaurateurSections/:id"
          component={RestaurateurSectionsWithId}
        />
        <Route path="/FinanceAndStats/:id" component={FinanceAndStatsWithId} />
        <Redirect to="/RestaurateurRestaurantList" />
      </Switch>
    </BrowserRouter>
  );
}

export default Restaurateur;
