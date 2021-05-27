import React, { useState, useEffect, useContext } from 'react';
import AppBar from '@material-ui/core/AppBar';
import Button from '@material-ui/core/Button';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import CssBaseline from '@material-ui/core/CssBaseline';
import Grid from '@material-ui/core/Grid';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import HomeIcon from '@material-ui/icons/Home';
import SnackbarContext from '../contexts/SnackbarContext';
import LoadingContext from '../contexts/LoadingContext';
import axios from 'axios';
import TextField from '@material-ui/core/TextField';
import apiUrl from "../shared/apiURL"
import UserContext from "../contexts/UserContext"
import Rating from '@material-ui/lab/Rating';
import StarBorderIcon from '@material-ui/icons/StarBorder';
import Box from '@material-ui/core/Box';
import { Link as RouterLink } from 'react-router-dom';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import ArrowBackIcon from "@material-ui/icons/ArrowBack";
import headers from "../shared/authheader";

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
      height: '100%',
      display: 'flex',
      flexDirection: 'column',
    },
    cardMedia: {
      paddingTop: '56.25%', // 16:9
    },
    cardContent: {
      flexGrow: 1,
    },
    footer: {
      backgroundColor: theme.palette.background.paper,
      padding: theme.spacing(6),
    },
  }));

function CustomerOrdersHistory(props) {
    
    const classes = useStyles();
    const { restId } = props;
    const { setLoading } = useContext(LoadingContext);
    const { setSnackbar } = useContext(SnackbarContext);
    const [orders, setOrders] = useState([]);
    const {user, setUser} = useContext(UserContext);

  
    async function fetchData() {
      setLoading(true);
      var config = {
        method: 'get',
        url: apiUrl + "user/customer/order/all",
        headers: headers(user)
      };
      
      try
      {
        const response = await axios(config);
        setOrders(response.data);
        console.log(response.data);
      }
      catch(error)
      {
        console.error(error);
                  setSnackbar({
                      open: true,
                      message: "Loading data failed",
                      type: "error"
                  });
      }



      setLoading(false);
      }
  
      useEffect(() => {
          fetchData();
      }, [setOrders]);
  
      
    
    return (
        <React.Fragment>
        <AppBar position="relative">
        <Toolbar>
          <Button>
            <RouterLink
              to={"/RestaurantList"}
              style={{ color: "#FFF" }}
            >
              <ArrowBackIcon fontSize="large" />
            </RouterLink>
          </Button>
          <Typography variant="h6" color="inherit" noWrap>
            List of orders view
          </Typography>
        </Toolbar>
      </AppBar>
        <div>
        <Typography color="primary" variant="h4">
            List of all orders for restaurant
        </Typography>
        <TableContainer component={Paper}>
            <Table className={classes.table} aria-label="simple table">
                <TableHead>
                <TableRow>
                    {console.log(orders)}
                    <TableCell>Order id</TableCell>
                    <TableCell align="center">State</TableCell>
                    <TableCell align="center">Restaurant</TableCell>
                    <TableCell align="center">Price</TableCell>
                    <TableCell align="center">Date</TableCell>
                    <TableCell align="right"></TableCell>
                </TableRow>
                </TableHead>
                <TableBody>
                {orders.map((row) => (
                    <TableRow key={row.id}>
                    <TableCell component="th" scope="row">
                        {row.id}
                    </TableCell>
                    <TableCell align="center">{row.state}</TableCell>
                    <TableCell align="center">{row.restaurant.name}</TableCell>
                    <TableCell align="center">{row.finalPrice}</TableCell>
                    <TableCell align="center">{row.date.replace('T',' ')}</TableCell>
                    <TableCell align="right">
                    </TableCell>
                    </TableRow>
                ))}
                </TableBody>
            </Table>
        </TableContainer>
        </div>
        </React.Fragment>
    )
}

export default CustomerOrdersHistory
