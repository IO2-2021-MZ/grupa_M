import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import { BrowserRouter } from "react-router-dom";
import RestaurateurRestaurantList from "./EmployeeRestaurantList";
import RestaurateurRestaurant from "./EmployeeRestaurant";
import AddNewDish from "./AddNewDishComponent";
import AddNewSection from "./AddNewSectionComponent";
import FinanceAndStats from "./FinanceAndStatsComponent";
import RestaurateurSections from "./EmployeeSections";
import RabatCodeList from "./RabatCodeList";
import NewRabatCode from "./NewRabatCode";
import AddNewRestaurant from "./AddNewRestaurantComponent";
import PatchSection from "./PatchSection";
import PatchDish from "./PatchDish"

import RestaurateurOrderList from "./RestaurateurOrderList";
import RestaurateurOrder from "./RestaurateurOrder";
import AllComplaints from "./AllComplaintsEmployee"
import Response from "./ComplaintResponseComponent"


const RestaurateurRestaurantWithId = ({ match }) => {
  return <RestaurateurRestaurant restId={match.params.id} />;
};

const RestaurateurSectionsWithId = ({ match }) => {
  return <RestaurateurSections restId={match.params.id} />;
};

const RestaurateurOrdersListWithId = ({ match }) => {
  return <RestaurateurOrderList restId={match.params.id} />;
};

const RestaurateurOrderWithId = ({ match }) => {
  return <RestaurateurOrder orderId={match.params.id} />;
};
const ComplaintsWithId = ({match}) => {
  return(
      <AllComplaints restaurantId={match.params.id}/>
  );
}

const ResponseWithId = ({match}) => {
  return(
      <Response complaintId={match.params.id}/>
  );
}


function Restaurateur(props) {
  return (
    <BrowserRouter>
      <Switch>
        <Route
          path="/RestaurantList"
          component={RestaurateurRestaurantList}
        />
        <Route path='/Complaints/:id' component = {ComplaintsWithId} />
        <Route path='/Response/:id' component = {ResponseWithId} />
        <Route
          path="/Restaurant/:id"
          component={RestaurateurRestaurantWithId}
        />
        <Route
          path="/RestaurateurSections/:id"
          component={RestaurateurSectionsWithId}
        />
        <Route path="/RestaurateurOrdersList/:id" component={RestaurateurOrdersListWithId} />
        <Route path="/RestaurateurOrder/:id" component={RestaurateurOrderWithId} />
        <Route path='/RabatCodeList' component={RabatCodeList}/>
        <Route path='/NewRabatCode' component={NewRabatCode}/>
        <Redirect to="/RestaurantList" />
      </Switch>
    </BrowserRouter>
  );
}

export default Restaurateur;
