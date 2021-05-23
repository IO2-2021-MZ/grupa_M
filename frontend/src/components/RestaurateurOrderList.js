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

function RestaurateurOrderList(props) {
    
    const classes = useStyles();
    const { restId } = props;
    const { setLoading } = useContext(LoadingContext);
    const { setSnackbar } = useContext(SnackbarContext);
    const [orders, setOrders] = useState([]);
    const {user, setUser} = useContext(UserContext);

  
    async function fetchData() {
      setLoading(true);
    console.log("id:"+{restId});
      var config = {
        method: 'get',
        url: apiUrl + "restaurant/order/all?id="+restId,
        headers: { 
          'Authorization': 'Bearer ' + user.token
        }
      };
      
      try
      {
        const response = await axios(config);
        setOrders(response.data);
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
              to={"/RestaurateurRestaurant/" + restId}
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
                    <TableCell align="right">Date</TableCell>
                    <TableCell align="right">Address</TableCell>
                    <TableCell align="right"></TableCell>
                    <TableCell align="right"></TableCell>
                </TableRow>
                </TableHead>
                <TableBody>
                {orders.map((row) => (
                    <TableRow key={row.id}>
                    <TableCell component="th" scope="row">
                        {row.id}
                    </TableCell>
                    <TableCell align="right">{row.date.replace('T',' ')}</TableCell>
                    <TableCell align="right">{row.address}</TableCell>
                    <TableCell align="right">
                        <Button variant="contained" color="primary">
                            <RouterLink to={"/RestaurateurOrder/"+row.id}  style={{ color: "#FFF" }}>
                                Details
                            </RouterLink>
                        </Button>
                        {/* <Button variant="contained" color="primary" onClick = {() => refuseOrder(row.id)}>
                            Accept
                        </Button>
                        &nbsp;
                        <Button variant="contained" color="secondary" onClick = {() => refuseOrder(row.id)}>
                            Refuse
                        </Button> */}
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

export default RestaurateurOrderList
