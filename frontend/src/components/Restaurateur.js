import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import { BrowserRouter } from "react-router-dom";
import RestaurateurRestaurantList from "./RestaurateurRestaurantList";
import RestaurateurRestaurant from "./RestaurateurRestaurant";
import AddNewDish from "./AddNewDishComponent";
import AddNewSection from "./AddNewSectionComponent";
import FinanceAndStats from "./FinanceAndStatsComponent";
import RestaurateurSections from "./RestaurateurSections";
import RabatCodeList from "./RabatCodeList";
import NewRabatCode from "./NewRabatCode";

const RestaurateurRestaurantWithId = ({ match }) => {
  return <RestaurateurRestaurant restId={match.params.id} />;
};

const RestaurateurSectionsWithId = ({ match }) => {
  return <RestaurateurSections restId={match.params.id} />;
};

const FinanceAndStatsWithId = ({ match }) => {
  return <FinanceAndStats restaurantId={match.params.id} />;
};

const AddNewDishWithId = ({ match }) => {
  return <AddNewDish sectionId={match.params.id} />;
};

const AddNewSectionWithId = ({ match }) => {
  return <AddNewSection restaurantId={match.params.id} />;
};

function Restaurateur(props) {
  return (
    <BrowserRouter>
      <Switch>
        <Route
          path="/RestaurateurRestaurantList"
          component={RestaurateurRestaurantList}
        />
        <Route path="/AddNewDish/:id" component={AddNewDishWithId} />
        <Route path="/AddNewSection/:id" component={AddNewSectionWithId} />
        <Route
          path="/RestaurateurRestaurant/:id"
          component={RestaurateurRestaurantWithId}
        />
        <Route
          path="/RestaurateurSections/:id"
          component={RestaurateurSectionsWithId}
        />
        <Route path="/FinanceAndStats/:id" component={FinanceAndStatsWithId} />
        <Route path='/RabatCodeList' component={RabatCodeList}/>
        <Route path='/NewRabatCode' component={NewRabatCode}/>
        <Redirect to="/RestaurateurRestaurantList" />
      </Switch>
    </BrowserRouter>
  );
}

export default Restaurateur;
