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
import AllComplaints from "./AllComplaintsEmployee";
import Response from "./ComplaintResponseComponent";
import EmployeeOrderList from "./EmployeeOrderList";
import EmployeeOrder from "./EmployeeOrder";


const RestaurateurRestaurantWithId = ({ match }) => {
  return <RestaurateurRestaurant restId={match.params.id} />;
};

const RestaurateurSectionsWithId = ({ match }) => {
  return <RestaurateurSections restId={match.params.id} />;
};

const EmployeeOrdersListWithId = ({ match }) => {
  return <EmployeeOrderList restId={match.params.id} />;
};

const EmployeeOrderWithId = ({ match }) => {
  return <EmployeeOrder orderId={match.params.id} />;
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
        <Route path="/EmployeeOrdersList/:id" component={EmployeeOrdersListWithId} />
        <Route path="/EmployeeOrder/:id" component={EmployeeOrderWithId} />
        <Route path='/RabatCodeList' component={RabatCodeList}/>
        <Route path='/NewRabatCode' component={NewRabatCode}/>
        <Redirect to="/RestaurantList" />
      </Switch>
    </BrowserRouter>
  );
}

export default Restaurateur;
