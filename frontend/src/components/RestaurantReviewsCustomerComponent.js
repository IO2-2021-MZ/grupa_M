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

  function RestaurantReviewsCustomerComponent(props) {
    
    const classes = useStyles();
    const { setLoading } = useContext(LoadingContext);
    const { setSnackbar } = useContext(SnackbarContext);
    const [reviews, setReviews] = useState([]);
    const {user, setUser} = useContext(UserContext);
    var restId = props.restId;
    const [rest, setRest] = useState([]);

    async function fetchRestaurant() {
        setLoading(true);
    
        var config = {
          method: 'get',
          url: apiUrl + "restaurant?id=" + restId,
          headers: headers(user)
        };
        
        try
        {
          const response = await axios(config);
          setRest(response.data);
          console.log(response.data)
          
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
            fetchRestaurant();
            fetchData();
        }, [setRest]);

  
    async function fetchData() {
      setLoading(true);
  
      var config = {
        method: 'get',
        url: apiUrl + "restaurant/review/all?id=" + restId,
        headers: headers(user)
      };
      
      try
      {
        const response = await axios(config);
        setReviews(response.data);
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
      }, [setReviews]);
  
    return (
        <div>
        <Typography color="primary" variant="h4">
            {rest.name} : reviews
        </Typography>
        <Button variant="contained" color="default">
            <RouterLink to="/RestaurantList">
            Back
            </RouterLink>
        </Button>
        <TableContainer component={Paper}>
            <Table className={classes.table} aria-label="simple table">
                <TableHead>
                <TableRow>
                    {console.log(reviews)}
                    <TableCell align="right">Content</TableCell>
                    <TableCell align="right">Rating</TableCell>
                    <TableCell align="right"></TableCell>
                </TableRow>
                </TableHead>
                <TableBody>
                {reviews.map((row) => (
                    <TableRow key={row.id}>

                    <TableCell align="right">{row.content}</TableCell>
                    <TableCell align="right">{row.rating}</TableCell>
                    </TableRow>
                ))}
                </TableBody>
            </Table>
        </TableContainer>
        </div>
    )
}

export default RestaurantReviewsCustomerComponent
