import React from "react";
import { Switch, Route, Redirect } from 'react-router-dom';
import { BrowserRouter} from 'react-router-dom';
import AdminRestaurantList from "./AdminRestaurantList";
import NewRabatCode from "./NewRabatCode"
import RabatCodeList from "./RabatCodeList"
import Rest from "./AdminRestaurantView";

const RestWithId = ({match}) => {
    return(
        <Rest restId={match.params.id}/>
    );
}

function Admin(props){
    return(
        <BrowserRouter>
            <Switch>
                <Route path='/RestaurantList' component={AdminRestaurantList}/>
                <Route path='/RabatCodeList' component={RabatCodeList}/>
                <Route path='/NewRabatCode' component={NewRabatCode}/>
                <Route path='/Restaurant/:id' component={RestWithId}/>
                <Redirect to='/RestaurantList' />
            </Switch>
        </BrowserRouter>
    );
}

export default Admin;