import { useState, useContext, useEffect } from "react";
import SnackbarContext from '../contexts/SnackbarContext';
import LoadingContext from '../contexts/LoadingContext';
import axios from 'axios';
import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import AppBar from '@material-ui/core/AppBar';
import { CardMedia, Container, CssBaseline } from "@material-ui/core";
import ArrowBackIcon from '@material-ui/icons/ArrowBack';
import Toolbar from '@material-ui/core/Toolbar';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import Rating from '@material-ui/lab/Rating';
import Box from '@material-ui/core/Box';

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
      display: 'flex',
      flexDirection: 'column',
      color: "default"
    },
    cardMedia: {
    
    },
    cardContent: {
      flexGrow: 1,
      color: "default"
    },
    footer: {
      backgroundColor: theme.palette.background.paper,
      padding: theme.spacing(6),
    },
  }));


const AdminRestaurantView = (props) => {
    const classes = useStyles();
    var restaurantId = props.restaurantId;
    const { setLoading } = useContext(LoadingContext);
    const [restaurant,setRestaurant] = useState([]);
    const { setSnackbar } = useContext(SnackbarContext);

    async function fetchData() {
        setLoading(true);
        var config = {
          method: 'get',
          url: "https://localhost:44384/restaurant?id="+restaurantId,
          headers: { 
            //'security-header': token
          }
        };
        
        try
        {
          const response = await axios(config);
          setRestaurant(response.data);
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

        console.log(restaurant)
        console.log(restaurant.length == 0)
        setLoading(false);
        }

    const deleteRestaurant = async (id) => {
            setLoading(true);
            var config = {
              method: 'delete',
              url: "https://localhost:44384/complaint?id=" + id
          }
          
          try
          {
              const response = await axios(config);
              console.log(response)
          }
          catch(e)
          {
              console.error(e);
              setSnackbar({
                  open: true,
                  message: 'Error occured',
                  type: 'error'
              })
          }
          fetchData();
          }

    useEffect(() => {
        fetchData();
    });

    return (
        <React.Fragment>
            <CssBaseline />
            <AppBar>
                <Toolbar>
                    <Button>
                        <ArrowBackIcon fontSize="large" />
                    </Button>
                    <Typography variant="h6" color="inherit" noWrap>
                        Restaurant
              </Typography>
                </Toolbar>
            </AppBar>
            { restaurant.length != 0 ?
            <div className={classes.heroContent}>
                <Container className={classes.cardGrid} maxWidth="md">
                    <Grid container spacing={4} alignItems="center">
                        <Grid xs={12} >
                            <Card className={classes.card}>
                                <CardMedia className={classes.cardMedia} />
                                <CardContent className={classes.cardContent}>
                                    <Typography variant="h5" align="left" color="textPrimary">
                                        {restaurant.name}
                                    </Typography>
                                    <Typography variant="subtitle1" gutterBottom>
                                        Contact Information: {restaurant.contactInformation}
                                    </Typography>
                                    <Typography variant="subtitle1" gutterBottom>
                                        Rating: {restaurant.rating}
                                    </Typography>
                                    <Typography variant="subtitle1" gutterBottom>
                                        Address {restaurant.address}
                                    </Typography>
                                    <Button variant="contained" color="primary" onClick={() => deleteRestaurant(restaurantId)}>
                                        <Typography variant="button" color="inherit">
                                            Delete Restaurant
                                        </Typography>
                                    </Button>

                                </CardContent>
                            </Card>
                        </Grid>
                    </Grid>
                </Container>
            </div>
            : 
            <div className={classes.heroContent}>
            <Container className={classes.cardGrid} maxWidth="md">
                <Grid container spacing={4} alignItems="center">
                    <Grid xs={12} >
                        <Card className={classes.card}>
                            <CardMedia className={classes.cardMedia} />
                            <CardContent className={classes.cardContent}>
                                <Typography variant="h5" align="left" color="textPrimary">
                                   NO RESTAURANT
                                </Typography>
                            </CardContent>
                        </Card>
                    </Grid>
                </Grid>
            </Container>
        </div>
            }
        </React.Fragment>
    )
    
}

export default AdminRestaurantView;