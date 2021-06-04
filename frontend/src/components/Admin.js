import React from "react";
import { Switch, Route, Redirect } from 'react-router-dom';
import { BrowserRouter} from 'react-router-dom';
import AdminRestaurantList from "./AdminRestaurantList";
import NewRabatCode from "./NewRabatCodeAdmin"
import RabatCodeList from "./RabatCodeList"
import Rest from "./AdminRestaurantView";
import FinanceAndStats from "./FinanceAndStatsComponent";
import AllComplaints from "./AllComplaintsComponent";


const RestWithId = ({match}) => {
    return(
        <Rest restId={match.params.id}/>
    );
}

const FinanceAndStatsWithId = ({match}) => {
    return(
        <FinanceAndStats restaurantId={match.params.id}/>
    );
}

const ComplaintsWithId = ({match}) => {
    return(
        <AllComplaints restaurantId={match.params.id}/>
    );
}


function Admin(props){
    return(
        <BrowserRouter>
            <Switch>
                <Route path='/Complaints/:id' component = {ComplaintsWithId} />
                <Route path='/RestaurantList' component={AdminRestaurantList}/>
                <Route path='/RabatCodeList' component={RabatCodeList}/>
                <Route path='/NewRabatCode' component={NewRabatCode}/>
                <Route path='/Restaurant/:id' component={RestWithId}/>
                <Route path="/FinanceAndStats/:id" component={FinanceAndStatsWithId} />
                <Redirect to='/RestaurantList' />
            </Switch>
        </BrowserRouter>
    );
}

export default Admin;