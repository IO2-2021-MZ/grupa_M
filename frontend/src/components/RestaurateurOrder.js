import React, { useState, useContext, useEffect } from "react";
import SnackbarContext from "../contexts/SnackbarContext";
import LoadingContext from "../contexts/LoadingContext";
import axios from "axios";
import { makeStyles } from "@material-ui/core/styles";
import Typography from "@material-ui/core/Typography";
import AppBar from "@material-ui/core/AppBar";
import { CardMedia, Container, CssBaseline } from "@material-ui/core";
import ArrowBackIcon from "@material-ui/icons/ArrowBack";
import Toolbar from "@material-ui/core/Toolbar";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardActions from "@material-ui/core/CardActions";
import TextField from "@material-ui/core/TextField";
import InputLabel from "@material-ui/core/InputLabel";
import MenuItem from "@material-ui/core/MenuItem";
import FormHelperText from "@material-ui/core/FormHelperText";
import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";
import apiUrl from "../shared/apiURL";
import UserContext from "../contexts/UserContext";
import HomeIcon from "@material-ui/icons/Home";
import Rating from "@material-ui/lab/Rating";
import StarBorderIcon from "@material-ui/icons/StarBorder";
import Box from "@material-ui/core/Box";
import { Link as RouterLink } from "react-router-dom";

const useStyles = makeStyles((theme) => ({
  icon: {
    marginRight: theme.spacing(2),
  },
  heroContent: {
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(8, 0, 6),
  },
  heroButtons: {
    marginTop: theme.spacing(4),
  },
  cardGrid: {
    paddingTop: theme.spacing(8),
    paddingBottom: theme.spacing(8),
  },
  card: {
    display: "flex",
    flexDirection: "column",
    color: "default",
  },
  cardMedia: {},
  cardContent: {
    flexGrow: 1,
    color: "default",
  },
  footer: {
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(6),
  },
  formControl: {
    margin: theme.spacing(1),
    minWidth: 120,
  },
  selectEmpty: {
    marginTop: theme.spacing(2),
  },
}));

export default function RestaurateurOrder(props) {
  const classes = useStyles();

  const { orderId } = props;
  const { setLoading } = useContext(LoadingContext);
  const { setSnackbar } = useContext(SnackbarContext);
  const [order, setOrder] = useState({
    "id": 0,
    "paymentMethod": "string",
    "state": "string",
    "date": "2021-05-23T13:26:52.095Z",
    "originalPrice": 0,
    "finalPrice": 0,
    "address": {
      "city": "string",
      "street": "string",
      "postCode": "string"
    },
    "discountCode": {
      "id": 0,
      "code": "string",
      "dateFrom": "2021-05-23T13:26:52.095Z",
      "dateTo": "2021-05-23T13:26:52.095Z",
      "restaurantId": 0,
      "percent": 0
    },
    "restaurant": {
      "id": 0,
      "name": "string",
      "contactInformation": "user@example.com",
      "rating": 0,
      "state": "string",
      "address": {
        "city": "string",
        "street": "string",
        "postCode": "string"
      },
      "owing": 0,
      "aggregatePayment": 0
    },
    "positions": [
      {
        "id": 0,
        "name": "string",
        "price": 0,
        "description": "string"
      }
    ],
    "employee": {
      "name": "string",
      "surname": "string",
      "email": "string"
    }
  });
  const [restId, setRestId] = useState(0);
  const { user, setUser } = useContext(UserContext);

  async function fetchData() {
    setLoading(true);
    console.log(props);
    var config = {
      method: "get",
      url: apiUrl + "order?id=" + orderId,
      headers: {
        Authorization: "Bearer " + user.token,
      },
    };

    try {
      const response = await axios(config);
      console.log(response);
      console.log(restId);
      setOrder(response.data);
      setRestId(response.data.restaurant.id);
    } catch (e) {
      console.error(e);
      setSnackbar({
        open: true,
        message: "Error occured",
        type: "error",
      });
    }
    setLoading(false);
  }

  const refuseOrder = async() => {
    setLoading(true);

    var config = {
      method: 'post',
      url: apiUrl + "order/refuse?id=" + order.id,
      headers: { 
        'Authorization': 'Bearer ' + user.token
      }
    };
    
    const response =  axios(config)
    .then(() => fetchData())
    .catch((error) => setSnackbar(error.message));
    setLoading(false);
  }

  const acceptOrder = async() => {
    setLoading(true);

    var config = {
      method: 'post',
      url: apiUrl + "order/accept?id=" + order.id,
      headers: { 
        'Authorization': 'Bearer ' + user.token
      }
    };
    
    const response =  axios(config)
    .then(() => fetchData())
    .catch((error) => setSnackbar(error.message));
    setLoading(false);
  }

  const realizeOrder = async() => {
    setLoading(true);

    var config = {
      method: 'post',
      url: apiUrl + "order/realized?id=" + order.id,
      headers: { 
        'Authorization': 'Bearer ' + user.token
      }
    };
    
    const response =  axios(config)
    .then(() => fetchData())
    .catch((error) => setSnackbar(error.message));
    setLoading(false);
  }

  useEffect(() => {
    fetchData();
  }, [setOrder]);

  return (
    <React.Fragment>
      <CssBaseline />
      <AppBar>
        <Toolbar>
          <Button>
            <RouterLink
              to={"/RestaurateurOrdersList/"+restId}
              style={{ color: "#FFF" }}
            >
              <ArrowBackIcon fontSize="large" />
            </RouterLink>
          </Button>
          <Typography variant="h6" color="inherit" noWrap>
            Order View
          </Typography>
        </Toolbar>
      </AppBar>
      <div className={classes.heroContent}>
        <Container className={classes.cardGrid} maxWidth="md">
          <Grid container spacing={4} alignItems="center">
            <Grid xs={12}>
              <Card>
                <CardContent>
                    <Typography variant="h4" color="textPrimary" align="center">
                          Details:  
                    </Typography>
                    <Typography variant="h5" color="textPrimary" align="left">
                          State: {order.state}  
                    </Typography>
                    <Typography variant="h5" color="textPrimary" align="left">
                          Date: {order.date.replace('T',' ')}  
                    </Typography>
                    <Typography variant="h5" color="textPrimary" align="left">
                          Address: {order.address.postCode +
                      " " +
                      order.address.city +
                      ", " +
                      order.address.street} 
                    </Typography>
                    {order.employee!==null ?(
                        <Typography variant="h5" color="textPrimary" align="left">
                            Employee: {order.employee.name +
                      " " +
                      order.employee.surname +
                      ", " +
                      order.employee.email} 
                        </Typography>
                    ) : (
                        <Typography variant="h5" color="textPrimary" align="left">
                        </Typography>)
                    
                    }
                    <Typography variant="h5" color="textPrimary" align="left">
                            Dishes: 
                    </Typography>
                     {order.positions.map((position) => (
                        <Typography variant="subtitle1" color="textPrimary" align="left">
                            {position.name}
                        </Typography> 
                    ))}
                        {order.state === "Unrealized" ? (
                        <div>
                        <Button variant="contained" color="primary" onClick = {() => acceptOrder(order.id)}>
                            Accept
                        </Button>
                        &nbsp;
                        &nbsp;
                        <Button variant="contained" color="secondary" onClick = {() => refuseOrder(order.id)}>
                            Refuse
                        </Button>
                        </div>) : (<div></div>)}
                        {order.state === "Pending" ? (
                        <Button variant="contained" color="primary" onClick = {() => realizeOrder(order.id)}>
                            Realize
                        </Button>) : (<div></div>)}
                        
                        
                </CardContent>
                
                        
              </Card>
            </Grid>
          </Grid>
        </Container>
      </div>
    </React.Fragment>
  );
}
